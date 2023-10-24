using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml;
using System.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Xml.Serialization;

namespace NetLicensingClient.Utils
{
    public class SignatureUtils
    {
        public static Boolean check(string xmlString, string publicKey)
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
