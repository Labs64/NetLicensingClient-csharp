C## wrapper for Labs64 NetLicensing [RESTful API](https://netlicensing.io/wiki/restful-api)

## 📖 Documentation

Visit NetLicensing [Wiki](https://netlicensing.io/wiki/) for setup and configuration tips, as well as examples on how to use licensing models.

## How to Use

This minimal example shows how to trigger validation request using APIKey identification.

```csharp
ValidationParameters validationParameters = new ValidationParameters();
validationParameters.setProductNumber("yourProductNumber");
validationParameters.setLicenseeName("yourLicenseeName");
validationParameters.put(demoProductModuleNumber, "paramKey", "paramValue");

Context context = new Context();
context.securityMode = SecurityMode.APIKEY_IDENTIFICATION;
context.apiKey = "apiKeyNumber";
ValidationResult validationResult = LicenseeService.validate(context, licenseeNumber, validationParameters);
```

## How to Contribute

Everyone is welcome to contribute to this project!
Feel free to contribute with pull requests, bug reports or enhancement suggestions.

## Bugs and Feedback

For bugs, questions and discussions please use the [GitHub Issues](https://github.com/Labs64/NetLicensingClient-csharp/issues).

## Links

- NetLicensing Website: [netlicensing.io](https://netlicensing.io)
- RESTful API: [https://netlicensing.io/wiki/restful-api](https://netlicensing.io/wiki/restful-api)
- GitHub Repo: [Labs64/NetLicensingClient-csharp](https://github.com/Labs64/NetLicensingClient-csharp)
