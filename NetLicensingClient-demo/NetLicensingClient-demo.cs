using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using NetLicensingClient.Entities;
using NetLicensingClient.Utils;

namespace NetLicensingClient
{
    class NetLicensingClient_demo
    {
        static int Main(string[] args)
        {
            Context context = new Context();
            context.baseUrl = "https://go.netlicensing.io/core/v2/rest";

            context.username = "demo";
            context.password = "demo";
            context.securityMode = SecurityMode.BASIC_AUTHENTICATION;

			String randomNumber = randomString(8);

            String demoProductNumber = numberWithPrefix("P", randomNumber);
            String demoProductModuleNumber = numberWithPrefix("PM", randomNumber);
            String demoLicensingModel = Constants.LicensingModel.TryAndBuy.NAME;
            String demoLicenseTemplate1_Number = numberWithPrefix("LT", randomNumber);
            String demoLicenseTemplate1_Name = "Demo Evaluation Period";
            String demoLicenseTemplate1_Type = "FEATURE";
            Decimal demoLicenseTemplate1_Price = 12.50M;
            String demoLicenseTemplate1_Currency = "EUR";
            Boolean demoLicenseTemplate1_Automatic = false;
            Boolean demoLicenseTemplate1_Hidden = false;
            String demoLicenseeNumber = numberWithPrefix("L", randomNumber);
            String demoLicenseNumber = numberWithPrefix("LC", randomNumber);
            String demoLicenseeName = "Demo Licensee";

            try
            {

                #region ****************** Lists

                List<String> licenseTypes = UtilityService.listLicenseTypes(context);
                ConsoleWriter.WriteList("License Types:", licenseTypes);

                List<String> licensingModels = UtilityService.listLicensingModels(context);
                ConsoleWriter.WriteList("Licensing Models:", licensingModels);

                #endregion

                #region ****************** Product

                Product newProduct = new Product();
                newProduct.number = demoProductNumber;
                newProduct.name = "Demo product";
                Product product = ProductService.create(context, newProduct);
                ConsoleWriter.WriteEntity("Added product:", product);

                product = ProductService.get(context, demoProductNumber);
                ConsoleWriter.WriteEntity("Got product:", product);

                List<Product> products = ProductService.list(context, null);
                ConsoleWriter.WriteList("Got the following products:", products);

                Product updateProduct = new Product();
                updateProduct.productProperties.Add("Updated property name", "Updated value");
                product = ProductService.update(context, demoProductNumber, updateProduct);
                ConsoleWriter.WriteEntity("Updated product:", product);

                ProductService.delete(context, demoProductNumber, true);
                ConsoleWriter.WriteMsg("Deleted Product!");

                products = ProductService.list(context, null);
                ConsoleWriter.WriteList("Got the following Products:", products);

                product = ProductService.create(context, newProduct);
                ConsoleWriter.WriteEntity("Added product again:", product);

                products = ProductService.list(context, null);
                ConsoleWriter.WriteList("Got the following Products:", products);

                #endregion

                #region ****************** ProductModule

                ProductModule newProductModule = new ProductModule();
                newProductModule.number = demoProductModuleNumber;
                newProductModule.name = "Demo product module";
                newProductModule.licensingModel = demoLicensingModel;
                ProductModule productModule = ProductModuleService.create(context, demoProductNumber, newProductModule);
                ConsoleWriter.WriteEntity("Added product module:", productModule);

                productModule = ProductModuleService.get(context, demoProductModuleNumber);
                ConsoleWriter.WriteEntity("Got product module:", productModule);

                List<ProductModule> productModules = ProductModuleService.list(context, null);
                ConsoleWriter.WriteList("Got the following ProductModules:", productModules);

                ProductModule updateProductModule = new ProductModule();
                updateProductModule.productModuleProperties.Add("Updated property name", "Updated property value");
                productModule = ProductModuleService.update(context, demoProductModuleNumber, updateProductModule);
                ConsoleWriter.WriteEntity("Updated product module:", productModule);

                ProductModuleService.delete(context, demoProductModuleNumber, true);
                ConsoleWriter.WriteMsg("Deleted ProductModule!");

                productModules = ProductModuleService.list(context, null);
                ConsoleWriter.WriteList("Got the following ProductModules:", productModules);

                productModule = ProductModuleService.create(context, demoProductNumber, newProductModule);
                ConsoleWriter.WriteEntity("Added product module again:", productModule);

                productModules = ProductModuleService.list(context, null);
                ConsoleWriter.WriteList("Got the following ProductModules:", productModules);

                #endregion

                #region ****************** LicenseTemplate

                LicenseTemplate newLicenseTemplate = new LicenseTemplate();
                newLicenseTemplate.number = demoLicenseTemplate1_Number;
                newLicenseTemplate.name = demoLicenseTemplate1_Name;
                newLicenseTemplate.licenseType = demoLicenseTemplate1_Type;
                newLicenseTemplate.price = demoLicenseTemplate1_Price;
                newLicenseTemplate.currency = demoLicenseTemplate1_Currency;
                newLicenseTemplate.automatic = demoLicenseTemplate1_Automatic;
                newLicenseTemplate.hidden = demoLicenseTemplate1_Hidden;
                ConsoleWriter.WriteEntity("Adding license template:", newLicenseTemplate);
                LicenseTemplate licenseTemplate = LicenseTemplateService.create(context, demoProductModuleNumber, newLicenseTemplate);
                ConsoleWriter.WriteEntity("Added license template:", licenseTemplate);

                licenseTemplate = LicenseTemplateService.get(context, demoLicenseTemplate1_Number);
                ConsoleWriter.WriteEntity("Got licenseTemplate:", licenseTemplate);

                List<LicenseTemplate> licenseTemplates = LicenseTemplateService.list(context, null);
                ConsoleWriter.WriteList("Got the following license templates:", licenseTemplates);

                LicenseTemplate updateLicenseTemplate = new LicenseTemplate();
                updateLicenseTemplate.active = true;
                updateLicenseTemplate.automatic = demoLicenseTemplate1_Automatic; // workaround: at the moment not specified booleans treated as "false"
                updateLicenseTemplate.hidden = demoLicenseTemplate1_Hidden; // workaround: at the moment not specified booleans treated as "false"
                licenseTemplate = LicenseTemplateService.update(context, demoLicenseTemplate1_Number, updateLicenseTemplate);
                ConsoleWriter.WriteEntity("Updated license template:", licenseTemplate);

                LicenseTemplateService.delete(context, demoLicenseTemplate1_Number, true);
                ConsoleWriter.WriteMsg("Deleted LicenseTemplate!");

                licenseTemplates = LicenseTemplateService.list(context, null);
                ConsoleWriter.WriteList("Got the following license templates:", licenseTemplates);

                licenseTemplate = LicenseTemplateService.create(context, demoProductModuleNumber, newLicenseTemplate);
                ConsoleWriter.WriteEntity("Added license template again:", licenseTemplate);

                licenseTemplates = LicenseTemplateService.list(context, null);
                ConsoleWriter.WriteList("Got the following license templates:", licenseTemplates);


                #endregion

                #region ****************** Licensee

                Licensee newLicensee = new Licensee();
                newLicensee.number = demoLicenseeNumber;
                Licensee licensee = LicenseeService.create(context, demoProductNumber, newLicensee);
                ConsoleWriter.WriteEntity("Added licensee:", licensee);

                List<Licensee> licensees = LicenseeService.list(context, null);
                ConsoleWriter.WriteList("Got the following licensees:", licensees);

                LicenseeService.delete(context, demoLicenseeNumber, true);
                ConsoleWriter.WriteMsg("Deleted licensee!");

                licensees = LicenseeService.list(context, null);
                ConsoleWriter.WriteList("Got the following licensees after delete:", licensees);

                licensee = LicenseeService.create(context, demoProductNumber, newLicensee);
                ConsoleWriter.WriteEntity("Added licensee again:", licensee);

                licensee = LicenseeService.get(context, demoLicenseeNumber);
                ConsoleWriter.WriteEntity("Got licensee:", licensee);

                Licensee updateLicensee = new Licensee();
                updateLicensee.licenseeProperties.Add("Updated property name", "Updated value");
                licensee = LicenseeService.update(context, demoLicenseeNumber, updateLicensee);
                ConsoleWriter.WriteEntity("Updated licensee:", licensee);

                licensees = LicenseeService.list(context, null);
                ConsoleWriter.WriteList("Got the following licensees:", licensees);

                #endregion

                #region ****************** License

                License newLicense = new License();
                newLicense.number = demoLicenseNumber;
                License license = LicenseService.create(context, demoLicenseeNumber, demoLicenseTemplate1_Number, null, newLicense);
                ConsoleWriter.WriteEntity("Added license:", license);

                List<License> licenses = LicenseService.list(context, null);
                ConsoleWriter.WriteList("Got the following licenses:", licenses);

                LicenseService.delete(context, demoLicenseNumber);
                Console.WriteLine("Deleted license");

                licenses = LicenseService.list(context, null);
                ConsoleWriter.WriteList("Got the following licenses:", licenses);

                license = LicenseService.create(context, demoLicenseeNumber, demoLicenseTemplate1_Number, null, newLicense);
                ConsoleWriter.WriteEntity("Added license again:", license);

                license = LicenseService.get(context, demoLicenseNumber);
                ConsoleWriter.WriteEntity("Got license:", license);

                License updateLicense = new License();
                updateLicense.licenseProperties.Add("Updated property name", "Updated value");
                license = LicenseService.update(context, demoLicenseNumber, null, updateLicense);
                ConsoleWriter.WriteEntity("Updated license:", license);

                #endregion

                #region ****************** PaymentMethod

                List<PaymentMethod> paymentMethods = PaymentMethodService.list(context);
                ConsoleWriter.WriteList("Got the following payment methods:", paymentMethods);

                #endregion

                #region ****************** Token

                string privateKey = File.ReadAllText(@"./Data/rsa_private.pem");
                string publicKey = File.ReadAllText(@"./Data/rsa_public.pem");
                string publicKeyWrong = @File.ReadAllText(@"./Data/rsa_public_wrong.pem");
                Console.WriteLine("loaded privateKey: {0}", privateKey);
                Console.WriteLine("loaded publicKey: {0}", publicKey);
                Console.WriteLine("loaded publicKey_wrong: {0}", publicKeyWrong);

                //NetLicensing supports API Key Identification to allow limited API access on vendor's behalf.
                //See: https://netlicensing.io/wiki/security for details.
                Token newToken = new Token();
                newToken.tokenType = Constants.Token.TYPE_APIKEY;
                newToken.tokenProperties.Add(Constants.Token.TOKEN_PROP_PRIVATE_KEY, privateKey);
                Token apiKey = TokenService.create(context, newToken);
                ConsoleWriter.WriteEntity("Created APIKey:", apiKey);
                context.apiKey = apiKey.number;

                newToken.tokenType = Constants.Token.TYPE_SHOP;
                newToken.tokenProperties.Add(Constants.Licensee.LICENSEE_NUMBER, demoLicenseeNumber);
                context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
                Token shopToken = TokenService.create(context, newToken);
                context.securityMode = SecurityMode.BASIC_AUTHENTICATION;
                ConsoleWriter.WriteEntity("Got the following shop token:", shopToken);

                String filter = Constants.Token.TOKEN_TYPE + "=" + Constants.Token.TYPE_SHOP;
                List<Token> tokens = TokenService.list(context, filter);
                ConsoleWriter.WriteList("Got the following shop tokens:", tokens);

                TokenService.delete(context, shopToken.number);
                ConsoleWriter.WriteMsg("Deactivated shop token!");

                tokens = TokenService.list(context, filter);
                ConsoleWriter.WriteList("Got the following shop tokens after deactivate:", tokens);

                #endregion

                #region ****************** Validate

                ValidationParameters validationParameters = new ValidationParameters();
                validationParameters.setLicenseeName(demoLicenseeName);
                validationParameters.setProductNumber(demoProductNumber);
                validationParameters.put(demoProductModuleNumber, "paramKey", "paramValue");

                ValidationResult validationResult = null;
                
                // Validate using Basic Auth
                context.securityMode = SecurityMode.BASIC_AUTHENTICATION;
                validationResult = LicenseeService.validate(context, demoLicenseeNumber, validationParameters);
                ConsoleWriter.WriteEntity("Validation result (Basic Auth):", validationResult);

                OSPlatform operatingSystem = GetOperatingSystem();
                Console.WriteLine("operatingSystem: {0}", operatingSystem);

                // Verify signature on Linux or OSX only
                // TODO: https://github.com/Labs64/NetLicensingClient-csharp/issues/25
                if (operatingSystem.Equals(OSPlatform.Linux) || operatingSystem.Equals(OSPlatform.OSX))
                {
                    // Validate using APIKey
                    context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
                    validationResult = LicenseeService.validate(context, demoLicenseeNumber, validationParameters);
                    ConsoleWriter.WriteEntity("Validation result (APIKey):", validationResult);

                    // Validate using APIKey signed
                    context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
                    context.publicKey = publicKey;
                    validationResult = LicenseeService.validate(context, demoLicenseeNumber, validationParameters);
                    ConsoleWriter.WriteEntity("Validation result (APIKey / signed):", validationResult);

                    // Validate using APIKey wrongly signed
                    context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
                    context.publicKey = publicKeyWrong;
                    try
                    {
                        validationResult = LicenseeService.validate(context, demoLicenseeNumber, validationParameters);
                    }
                    catch (NetLicensingException e)
                    {
                        Console.WriteLine("Validation result exception (APIKey / wrongly signed): {0}", e);
                    }
                }

                // Reset context for futher use
                context.securityMode = SecurityMode.BASIC_AUTHENTICATION;
                context.publicKey = null;

                #endregion

                #region ****************** Offline validation
                Context offlineContext = new Context();
                offlineContext.publicKey = publicKey;

                string validationFile = File.ReadAllText(@"./Data/Isb-DEMO.xml");

                ValidationResult validationOfflineResult = ValidationService.validateOffline(offlineContext, validationFile);
                ConsoleWriter.WriteEntity("Offline validation result (Basic Auth):", validationOfflineResult);

                offlineContext.publicKey = publicKeyWrong;

                try
                {
                    validationOfflineResult = ValidationService.validateOffline(offlineContext, validationFile);
                }
                catch (NetLicensingException e)
                {
                    Console.WriteLine("Offline validation result exception (APIKey / wrongly signed): {0}", e);
                }

                #endregion

                #region ****************** Transfer

                Licensee transferLicensee = new Licensee();
                transferLicensee.number = "TR" + demoLicenseeNumber;
                transferLicensee.licenseeProperties.Add(Constants.Licensee.PROP_MARKED_FOR_TRANSFER, Boolean.TrueString.ToLower());
                transferLicensee = LicenseeService.create(context, demoProductNumber, transferLicensee);
                ConsoleWriter.WriteEntity("Added transfer licensee:", transferLicensee);

                License transferLicense = new License();
                transferLicense.number = "LTR" + demoLicenseNumber;
                License newTransferLicense = LicenseService.create(context, transferLicensee.number, demoLicenseTemplate1_Number, null, transferLicense);
                ConsoleWriter.WriteEntity("Added license for transfer:", newTransferLicense);

                LicenseeService.transfer(context, licensee.number, transferLicensee.number);

                licenses = LicenseService.list(context, Constants.Licensee.LICENSEE_NUMBER + "=" + licensee.number);
                ConsoleWriter.WriteList("Got the following licenses after transfer:", licenses);

                Licensee transferLicenseeWithApiKey = new Licensee();
                transferLicenseeWithApiKey.number = "Key" + demoLicenseeNumber;
                transferLicenseeWithApiKey.licenseeProperties.Add(Constants.Licensee.PROP_MARKED_FOR_TRANSFER, Boolean.TrueString.ToLower());
                transferLicenseeWithApiKey = LicenseeService.create(context, demoProductNumber, transferLicenseeWithApiKey);

                License transferLicenseWithApiKey = new License();
                transferLicenseWithApiKey.number = "Key" + demoLicenseNumber;
                LicenseService.create(context, transferLicenseeWithApiKey.number, demoLicenseTemplate1_Number, null,
                        transferLicenseWithApiKey);

                context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
                LicenseeService.transfer(context, licensee.number, transferLicenseeWithApiKey.number);
                context.securityMode = SecurityMode.BASIC_AUTHENTICATION;

                licenses = LicenseService.list(context, Constants.Licensee.LICENSEE_NUMBER + "=" + licensee.number);
                ConsoleWriter.WriteList("Got the following licenses after transfer:", licenses);

                #endregion

                #region ****************** Transactions

                List<Transaction> transactions = TransactionService.list(context, Constants.Transaction.SOURCE_SHOP_ONLY + "=" + Boolean.TrueString.ToLower());
                ConsoleWriter.WriteList("Got the following transactions shop only:", transactions);

                transactions = TransactionService.list(context, null);
                ConsoleWriter.WriteList("Got the following transactions after transfer:", transactions);

                #endregion

                Console.WriteLine("All done.");

                return 0;
            }
            catch (NetLicensingException e)
            {
                Console.WriteLine("Got NetLicensing exception:");
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception:");
                Console.WriteLine(e);
            }
            finally
            {
                try
                {
                    // Cleanup
                    context.securityMode = SecurityMode.BASIC_AUTHENTICATION;

                    // Deactivate APIKey in case this was used (exists)
                    if (!String.IsNullOrEmpty(context.apiKey))
                    {
                        TokenService.delete(context, context.apiKey);
                    }
                    // Delete test product with all its related items
                    ProductService.delete(context, demoProductNumber, true);
                }
                catch (NetLicensingException e)
                {
                    Console.WriteLine("Got NetLicensing exception during cleanup:");
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Got exception during cleanup:");
                    Console.WriteLine(e);
                }
            }

            return 1;
        }

		public static string randomString(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private static String numberWithPrefix(String prefix, String number)
		{
			return "DEMO-"+prefix+number;
		}


        private static OSPlatform GetOperatingSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            throw new Exception("Cannot determine operating system!");
        }

    }
}
