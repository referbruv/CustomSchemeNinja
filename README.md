# Custom Scheme Ninja - ASP.NET Core Boilerplate

CustomSchemeNinja is a boilerplate solution, built to demonstrate to demonstrate creating and using a custom Authentication Scheme in ASP.NET Core (.NET 6)

# The Need for a Custom Authentication Scheme

Whenever we implement token authentication for our APIs to enhance security, we generally go for standard token authentication schemes such as JWT Bearer. This authentication requires the input token be in a JWT (JSON Web Token) and follow certain requirements (such as a particular encryption mechanism for the token like SHA256 or RSA). 

But sometimes we might come across requirements to authenticate a custom token, may be because of the client practices or recommendations or whatever. In such cases, we can't use the default authentication schemes (such as Bearer or Cookie) in order to validate an incoming token.

We require to build our own authentication handler which contains the necessary logic to extract token from the headers, validate and decide whether the requirement has been successfully met or the token is invalid and is a failure.

# Technologies

ASP.NET Core (.NET 6)
ASP.NET Core Authentication Library

# About the Boilerplate

This boilerplate is a perfect starter for developers looking to implement custom Authentication. It offers the following:

1. Simple implementation and separation of an Authentication Scheme
2. Detailed Controller Endpoints for API
3. Seeded data to view results as the solution starts
4. Postman Collection included, to get quickly get started
5. Dockerfile included for containerization

# Getting Started

To get started, follow the below steps:

1. Install .NET 6 SDK
2. Clone the Solution into your Local Directory
3. Navigate to the Cloned directory and run the solution

# Testing the Solution

The solution contains necessary seeding code, so once the solution starts you already have the data ready.

Once the solution is running, open a browser and try the below URLs to see the API in action.

1. Test if the API is running:

```
curl --location --request GET 'https://localhost:5001/api/Ninjas/Alive'

Ninja clan is Alive
```

2. Sending an Unauthenticated Request:

```
curl --location --request GET 'https://localhost:5001/api/Ninjas/1002'

*throws empty response with header 401 UnAuthorized
```

3. Sending a Token request:

```
curl --location --request GET 'https://localhost:5001/api/Ninjas/1002' \
--header 'Authorization: Ninpo eyJ1c2VySWQiOiIxMDAwMSIsIm5hbWUiOiJIYXR0b3JpIFplbnpvIiwiZW1haWxBZGRyZXNzIjoibWFyaXNoaXR0ZW5AbmlucG8uY29tIn0='

{
    "id": 1002,
    "name": "Gou",
    "moniker": "Shinobi5",
    "techniques": [
        ""
    ]
}
```

The complete explanation of this sample is available at:

[(UPDATED to .NET 6) Implementing Custom Authentication Scheme in ASP.NET Core](https://referbruv.com/blog/posts/implementing-custom-authentication-scheme-and-handler-in-aspnet-core-3x)

Leave a Star if you find the solution useful. For more detailed articles and how-to guides, visit https://referbruv.com

<a href="https://www.buymeacoffee.com/referbruv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>