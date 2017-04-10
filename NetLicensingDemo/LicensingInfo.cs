using System;
using System.Web;
using NetLicensingClient;
using NetLicensingClient.Entities;
using System.Xml;
using System.Globalization;

namespace NetLicensingDemo
{
    public class LicensingInfo
    {
        public class ModuleTryAndBuy
        {
            public string name { private set; get; }
            public bool isValid { private set; get; }
            public bool isEvaluation { private set; get; }
            public string evaluationExpires { private set; get; }

            public ModuleTryAndBuy(string _name) {
                name = _name;
                reset();
            }

            public void update(Composition validation)
            {
                isValid = validation.properties["valid"].value == "true";
                isEvaluation = validation.properties["evaluation"].value == "true";
                evaluationExpires = (isEvaluation) ? convertDate(validation.properties["evaluationExpires"].value) : "";
            }

            public void reset()
            {
                isValid = false;
                isEvaluation = false;
                evaluationExpires = "";
            }
        }

        public class ModuleSubscription
        {
            public string name { private set; get; }
            public bool isValid { private set; get; }
            public string expires { private set; get; }

            public ModuleSubscription(string _name) {
                name = _name;
                reset();
            }

            public void update(Composition validation)
            {
                isValid = validation.properties["valid"].value == "true";
                expires = (isValid) ? convertDate(validation.properties["expires"].value) : "";
            }

            public void reset()
            {
                isValid = false;
                expires = "";
            }
        }

        public string licenseeNumber { set; get; }
        public string sourceLicenseeNumber { set; get; }
        public ModuleTryAndBuy module1 { private set; get; }
        public ModuleSubscription module2 { private set; get; }
        public string errorInfo { private set; get; }

        public void updateLicensingInfo()
        {
            errorInfo = "";
            try
            {
                ValidationParameters validationParameters = new ValidationParameters();
                ValidationResult validationResult = LicenseeService.validate(netLicensingContext, licenseeNumber, productNumber, "", validationParameters);

                if (validationResult.getValidations().ContainsKey(module1.name))
                {
                    module1.update(validationResult.getValidations()[module1.name]);
                }
                if (validationResult.getValidations().ContainsKey(module2.name))
                { 
                    module2.update(validationResult.getValidations()[module2.name]);   
                }
            }
            catch (NetLicensingException e)
            {
                module1.reset();
                module2.reset();
                errorInfo = "NetLicensing error during validation: " + e.Message;
            }
            catch (Exception e)
            {
                module1.reset();
                module2.reset();
                errorInfo = "Error during validation: " + e.ToString();
            }
            catch
            {
                module1.reset();
                module2.reset();
                errorInfo = "Unknown error during validation";
            }
        }

        public void transferLicenses ()
        {
            errorInfo = "";
            try {
                LicenseeService.transfer(netLicensingContext, licenseeNumber, sourceLicenseeNumber);
            } 
            catch (NetLicensingException e) 
            {
            	module1.reset ();
            	module2.reset ();
            	errorInfo = "NetLicensing error during transfer: " + e.Message;
            } 
            catch (Exception e) 
            {
            	module1.reset ();
            	module2.reset ();
            	errorInfo = "Error during transfer: " + e.ToString ();
            } 
            catch 
            {
            	module1.reset ();
            	module2.reset ();
                errorInfo = "Unknown error during transfer";
            }
        }

        private String productNumber = "P-DEMO";
        private Context netLicensingContext;

        private LicensingInfo()
        {
            netLicensingContext = new Context();
            netLicensingContext.baseUrl = "https://go.netlicensing.io";
            netLicensingContext.apiKey = "18d49975-7956-47f8-aed9-f5e989722406";
            netLicensingContext.securityMode = SecutiryMode.APIKEY_IDENTIFICATION;
            module1 = new ModuleTryAndBuy("demo-try-and-buy");
            module2 = new ModuleSubscription("demo-subscription");
        }

        private static string convertDate(string xmlUTC)
        {
            try
            {
                return XmlConvert.ToDateTime(xmlUTC, XmlDateTimeSerializationMode.Utc)
                    .ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
            } catch {
                return xmlUTC;
            }
        }

        // LicensingInfo is global for the application -> singleton
        public static LicensingInfo getLicensingInfo()
        {
            if (HttpContext.Current.Items["LicensingInfo"] == null)
            {
                HttpContext.Current.Items["LicensingInfo"] = new LicensingInfo();
            }

            return HttpContext.Current.Items["LicensingInfo"] as LicensingInfo;
        }
    }
}
