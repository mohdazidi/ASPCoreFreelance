# ASPCoreFreelance

in this repository created for Etiqa interview. This WebApi was build using C#, ASP.Net Core 


## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)


## Introduction

A fictional company, CDN - Complete Developer Network is going to build a list of freelancers.
Such that they could have a directory of contact get people for their job.

## Features

Key features of this project.

- Support CRUD operations for Freelance User information.
- connect to MySQL database.
- Net Core RESTful API design with Swagger.
- Error handling and logging to specific log file.

## Getting Started

set up and run your project locally. Include prerequisites and step-by-step instructions.

```
# Clone the repository
git clone https://github.com/mohdazidi/ASPCoreFreelance.git

# project directory
download 2 (CDNUserMgt & CDNUserMgtWeb) folder open project solution in CDNUserMgt folder through Visual Studio 2022 and above. 

# Build and run the project
dotnet build
dotnet run

# run cdnusermgmt.sql in MySQL database.

```

## Usage

this application can be interact with API through Swagger.  API requests and responses.
```
https://localhost:7019/](https://localhost:7019/swagger/index.html
```

example http
```
GET https://localhost:7019/api/FreelanceUser/1
```

Response:

FreelanceUser json sample
```
{
  "Username": "john_doe",
  "Mail": "john.doe@example.com",
  "PhoneNumber": "1234567890",
  "Skillsets": ["C#", "ASP.NET", "Web Development"],
  "Hobby": ["Reading", "Gaming"]
}
```

## API Endpoints

Available API endpoints, methods, and expected inputs/outputs.

    GET https://localhost:7019//api/User/GetUser  input : UserName (string) --  Get a specific user.
    GET https://localhost:7019//api/User/ListAllUser no input -- Get all users.
    POST https://localhost:7019//api/User/RegisterUser input : FreelanceUser JSON -- Register a new user.
    PUT https://localhost:7019//api/User/UpdateUser input : FreelanceUser -- Update an existing user.
    DELETE https://localhost:7019//api/User/DeleteUser input : Username -- Delete a user.

## Configuration

please change configuration settings.
```
    Database Connection String: Set the DefaultConnection in appsettings.json.
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;Database=cdnusermgmt;User=root;Password='';",
      "backupConnection": "Server=localhost;Port=3306;Database=YourDatabaseName;User=root;Password='';"
    }

  

```
