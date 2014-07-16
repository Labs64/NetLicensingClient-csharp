using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetLicensingClient;
using NetLicensingClient.Entities;
using System.Net;

namespace NetLicensingClient
{
    class NetLicensingClient_demo
    {
        static int Main(string[] args)
        {
            // ServicePointManager.ServerCertificateValidationCallback = delegate { return true;  // Trust any (self-signed) certificate }; 

            Context context = new Context();
            context.baseUrl = "https://netlicensing.labs64.com";
            context.username = "demo";
            context.password = "demo";
            context.securityMode = SecutiryMode.BASIC_AUTHENTICATION;

            String demoProductNumber = "P001demo";
            String demoProductModuleNumber = "M001demo";
            String demoLicensingModel = "TimeLimitedEvaluation";
            String demoLicenseTemplate1_Number = "E001demo";
            String demoLicenseTemplate1_Name = "Demo Evaluation Period";
            String demoLicenseTemplate1_Type = "FEATURE";
            Decimal demoLicenseTemplate1_Price = 12.50M;
            String demoLicenseTemplate1_Currency = "EUR";
            Boolean demoLicenseTemplate1_Automatic = false;
            Boolean demoLicenseTemplate1_Hidden = false;
            String demoLicenseeNumber = "I001demo";
            String demoLicenseNumber = "L001demoTV";
            String demoLicenseeName = "demo licensee";

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

                #region ****************** Transaction

                List<Transaction> transactions = TransactionService.list(context, null);
                ConsoleWriter.WriteList("Got the following transactions:", transactions);

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
                ConsoleWriter.WriteList("Got the following license templates:", licenses);

                LicenseService.delete(context, demoLicenseNumber);
                Console.WriteLine("Deleted license");

                licenses = LicenseService.list(context, null);
                ConsoleWriter.WriteList("Got the following license templates:", licenses);

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

                Token newToken = new Token();
                newToken.tokenType = Constants.Token.TYPE_APIKEY;
                Token apiKey = TokenService.create(context, newToken);
                ConsoleWriter.WriteEntity("Created API Key:", apiKey);
                context.apiKey = apiKey.number;

                newToken.tokenType = Constants.Token.TYPE_SHOP;
                newToken.tokenProperties.Add(Constants.Licensee.LICENSEE_NUMBER, demoLicenseeNumber);
                context.securityMode = SecutiryMode.APIKEY_IDENTIFICATION;
                Token shopToken = TokenService.create(context, newToken);
                context.securityMode = SecutiryMode.BASIC_AUTHENTICATION;
                ConsoleWriter.WriteEntity("Got the following shop token:", shopToken);

                String filter = Constants.Token.TOKEN_TYPE + "=" + Constants.Token.TYPE_SHOP;
                List<Token> tokens = TokenService.list(context, filter);
                ConsoleWriter.WriteList("Got the following shop tokens:", tokens);

                TokenService.deactivate(context, shopToken.number);
                ConsoleWriter.WriteMsg("Deactivated shop token!");

                tokens = TokenService.list(context, filter);
                ConsoleWriter.WriteList("Got the following shop tokens after deactivate:", tokens);

                #endregion

                #region ****************** Validate

                ValidationResult validationResult = LicenseeService.validate(context, demoLicenseeNumber, demoProductNumber, demoLicenseeName);
                ConsoleWriter.WriteEntity("Validation result for created licensee:", validationResult);

                context.securityMode = SecutiryMode.APIKEY_IDENTIFICATION;
                validationResult = LicenseeService.validate(context, demoLicenseeNumber, demoProductNumber, demoLicenseeName);
                context.securityMode = SecutiryMode.BASIC_AUTHENTICATION;
                ConsoleWriter.WriteEntity("Validation repeated with API Key:", validationResult);

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
                    // Cleanup:
                    context.securityMode = SecutiryMode.BASIC_AUTHENTICATION;

                    // deactivate api key in case APIKey was used (exists)
                    if (!String.IsNullOrEmpty(context.apiKey))
                    {
                        TokenService.deactivate(context, context.apiKey);
                    }
                    // delete test product with all its related items
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
    }
}
