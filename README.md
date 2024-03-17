## MVC Web Application
This repository contains a simple ASP.NET MVC web application.
# Features:
- Potentially allows users to submit and save data (e.g., user information) using forms.
- Handles errors and displays user-friendly messages and Logging
- Leverages JSON for data persistence (likely stores user data in a JSON file).
- User Input Validation
# Getting Started:
- # Prerequisites:
  - .NET Core SDK (https://dotnet.microsoft.com/en-us/download)
  - Basic understanding of ASP.NET MVC concepts
- # Clone the repository:
```
   git clone https://github.com/atkpradeep/MVC_WebApp.git
```
# Navigate to the project directory:
```
  cd MVC_WebApp
```
# Restore dependencies:
```
  dotnet restore
```
# Run the application:
```
  dotnet run
```
This will start the application and you can usually access it in your web browser at http://localhost:5069 (the exact port might vary).
# Project Structure

The project is structured as follows:

  - Controllers: This folder contains the controller classes responsible for handling user requests and interacting with the model and views.
  - Models: This folder contains the model classes representing the data used by the application.
  - Views: This folder contains the view templates that define the user interface of the application.
  - Program.cs: This file is the entry point of the application.
# Dependencies
  This application depends on the following NuGet packages:

    - Microsoft.AspNetCore.App
    - Microsoft.AspNetCore.Mvc.NewtonsoftJson
    - Microsoft.Extensions.Logging.Debug

# Next Steps

This is a basic example of an MVC web application. You can explore the code to understand the concepts and modify it to implement your own functionalities.

Here are some potential areas for further development:

- Add user authentication and authorization.
- Implement data access with a database instead of relying on a local file for user data.
- Create more controllers, models, and views to implement additional features.
- Style the user interface with CSS or a CSS framework (e.g., Bootstrap).

# Below is simple screenshot for form

  ![image](https://github.com/atkpradeep/MVC_WebApp/assets/32030192/24b173b7-eaaa-4e98-b5c2-1b977ebb36a4)

