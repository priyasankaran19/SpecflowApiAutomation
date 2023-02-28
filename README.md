# SPECFLOW API AUTOMATION FRAMEWORK

### This is a SpecFlow API Automation Framework for testing RESTful APIs. It is built on top of SpecFlow and NUnit. It uses the Page Object Model design pattern and is written in C#.

### The framework is designed to be used with Visual Studio 2022 and .NET Framework 7.

## Folder Structure

### The framework is structured as follows:

Features - This folder contains the feature files. Each feature file contains the scenarios to be tested.

StepDefinitions - This folder contains the step definitions. Each step definition file contains the code that is executed when a step is run.

Support - This folder contains the support / helper files. These files help in setting up the test environment and also contain the code for the page objects.

TestData - This folder contains the test data files. These files contain the test data that is used in the tests.

## Reading Env Variables and Test Data
appsettings.json file contains the environment variables and test data. These variables and data can be accessed using the following code:

```csharp
string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
string testData = ConfigurationManager.AppSettings["testData"];
```
### Pre-requisites

- Visual Studio 2022
- .NET Framework 7
- SpecFlow for Visual Studio 2022
- NUnit 3 Test Adapter


## Reporting
We are using Specflow LivingDoc plugin to generate the test report. The report is generated in the following format:
