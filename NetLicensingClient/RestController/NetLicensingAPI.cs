using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NetLicensingClient.Entities;
using NetLicensingClient.Exceptions;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace NetLicensingClient.RestController
{
    class NetLicensingAPI
    {
        public enum Method { GET, POST, DELETE };

        public static netlicensing request(Context context, Method method, String path, Dictionary<String, String> parameters)
        {
            #region HTTP request preparation
            // Workaround of the mod_proxy_ajp problem.
            // mod_proxy_ajp has problem processing HTTP/1.1 POST request with delayed payload transmission (Expect: 100 Continue), causes 500 Server Error in AJP module.
            // Resources on the topic:
            // http://haacked.com/archive/2004/05/15/http-web-request-expect-100-continue.aspx
            // http://stackoverflow.com/questions/3889574/apache-and-mod-proxy-not-handling-http-100-continue-from-client-http-417
            // https://issues.apache.org/bugzilla/show_bug.cgi?id=46709
            // https://issues.apache.org/bugzilla/show_bug.cgi?id=47087
            ServicePointManager.Expect100Continue = false;

            StringBuilder requestPayload = new StringBuilder();
            if (parameters != null)
            {
                bool first = true;
                foreach (KeyValuePair<String, String> param in parameters)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        requestPayload.Append("&");
                    }

                    requestPayload.Append(HttpUtility.UrlEncode(Regex.Replace(param.Key, "discount[[0-9]+]", "discount")));
                    requestPayload.Append("=");
                    requestPayload.Append(HttpUtility.UrlEncode(param.Value));
                }
            }
            String urlParam = "";
            String requestBody = null;
            if (requestPayload.Length > 0)
            {
                switch (method)
                {
                    case Method.GET:
                    case Method.DELETE:
                        urlParam = "?" + requestPayload.ToString();
                        break;
                    case Method.POST:
                        requestBody = requestPayload.ToString();
                        break;
                }
            }

            HttpWebRequest request = WebRequest.Create(context.baseUrl + "/" + path + urlParam) as HttpWebRequest;
            request.UserAgent = "NetLicensing/C# " + Constants.NETLICENSING_VERSION + "/"+ System.Environment.Version + " (https://netlicensing.io)";

            switch (method)
            {
                case Method.GET: request.Method = "GET"; break;
                case Method.POST:
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    break;
                case Method.DELETE: request.Method = "DELETE"; break;
                default:
                    throw new RestException("Invalid request type:" + method + ", allowed requests types: GET, POST, DELETE");
            }
            switch (context.securityMode)
            {
                case SecurityMode.BASIC_AUTHENTICATION:
                    request.Credentials = new NetworkCredential(context.username, context.password);
                    break;
                case SecurityMode.APIKEY_IDENTIFICATION:
                    request.Credentials = new NetworkCredential(Constants.APIKEY_USER, context.apiKey);
                    break;
                case SecurityMode.ANONYMOUS_IDENTIFICATION:
                    break;
                default:
                    throw new RestException("Unknown security mode");
            }
            request.PreAuthenticate = true;
            request.Accept = "application/xml";
            request.SendChunked = false;
            if (requestBody != null)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            #endregion

            netlicensing responsePayload = null;
            try
            {
                #region HTTP response parsing
                using (HttpWebResponse response = (request as HttpWebRequest).GetResponse() as HttpWebResponse)
                {
                    HttpStatusCode statusCode = response.StatusCode;
                    switch (statusCode)
                    {
                        case HttpStatusCode.OK:
                            // GetReponseStream() creates a connected stream that can't be read twice due to
                            // missing Seek() method. Therefore, copy responseStream to MemoryStream which
                            // can be used for deserializing XML data as well as verification of signature.
                            var responseStream = response.GetResponseStream();
                            //responsePayload = deserialize(response.GetResponseStream());
                            var memoryStream = new MemoryStream();
                            responseStream.CopyTo(memoryStream);
                            //responsePayload = deserialize(responseStream);
                            memoryStream.Position = 0;
                            responsePayload = deserialize(memoryStream);

                            // verify signature
                            if (!string.IsNullOrEmpty(context.publicKey) && !string.IsNullOrEmpty(context.apiKey))
                            {
                                memoryStream.Position = 0;
                                using (StreamReader reader = new StreamReader(memoryStream))
                                {
                                    var responseString = reader.ReadToEnd();
                                    bool verified = VerifyXmlSignature(responseString, context.publicKey);
                                    if (!verified)
                                    {
                                        throw new NetLicensingException("XML signature could not be verified");
                                    }
                                }
                            }
                            memoryStream.Dispose();
                            responseStream.Dispose();
                            break;
                        case HttpStatusCode.NoContent:
                            break;
                        default:
                            throw new NetLicensingException(String.Format("Got unsupported response result code {0}: '{1}'", response.StatusCode, response.StatusDescription));
                    }
                    response.Close();
                }
                #endregion
            }
            catch (WebException ex)
            {
                #region HTTP and NetLicensing errors conversion to Exception
                String plainTextResponse = null;
                using (HttpWebResponse response = ex.Response as HttpWebResponse)
                {
                    if (response != null)
                    {
                        if (response.ContentLength > 0)
                        {
                            try
                            {
                                responsePayload = deserialize(response.GetResponseStream());
                            }
                            catch (Exception)
                            {
                                // Ignore deserialization errors - response is not necessarily formatted as NetLicensing
                                response.GetResponseStream().Seek(0, SeekOrigin.Begin);
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    plainTextResponse = reader.ReadToEnd();
                                }
                            }
                        }
                        response.Close();
                    }
                }
                StringBuilder messages = new StringBuilder();
                messages.AppendLine("Bad request to the NetLicensingAPI:");
                if (responsePayload != null)
                {
                    foreach (info i in responsePayload.infos)
                    {
                        messages.AppendLine(i.Value);
                    }
                }
                if (plainTextResponse != null)
                {
                    messages.AppendLine(plainTextResponse);
                }
                throw new NetLicensingException(messages.ToString(), ex);
                #endregion
            }
            return responsePayload;
        }

        private static netlicensing deserialize(Stream responseStream)
        {
            XmlSerializer NetLicensingSerializer = new XmlSerializer(typeof(netlicensing));
            return NetLicensingSerializer.Deserialize(responseStream) as netlicensing;
        }

		private static bool VerifyXmlSignature(string xmlString, string publicKey)
		{
			using (var keyReader = new StringReader(publicKey))
			{
				var pemReader = new PemReader(keyReader);

				RsaKeyParameters parameters = (RsaKeyParameters)pemReader.ReadObject();
				RSAParameters rParams = new RSAParameters();
				rParams.Modulus = parameters.Modulus.ToByteArray();
				rParams.Exponent = parameters.Exponent.ToByteArray();

                RSA rsaKey = RSA.Create();
                rsaKey.ImportParameters(rParams);

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.PreserveWhitespace = true;
				xmlDoc.LoadXml(xmlString);

                // Create a new SignedXml object and pass it the XML document class
                SignedXml signedXml = new SignedXml(xmlDoc);
                // Find the "Signature" node and create a new XmlNodeList object
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

                // Throw an exception if no signature was found
                if (nodeList.Count <= 0)
				{
					throw new CryptographicException("Verification failed: No Signature was found in the document.");
				}

				// Throw an exception if more than one signature was found
				if (nodeList.Count >= 2)
				{
					throw new CryptographicException("Verification failed: More that one signature was found for the document.");
				}

                // Load the first <signature> node
                signedXml.LoadXml((XmlElement)nodeList[0]);

                // Check the signature and return the result
                return signedXml.CheckSignature(rsaKey);
			}
		}
    }

}
