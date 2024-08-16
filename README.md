# CountriesForEveryone

REST Countries API made better!

## Table of Contents
1. [Design & Implementation Details](#design--implementation-details)
2. [Architecture](#architecture)
3. [Features Highlight](#features-highlight)
4. [Parts I Found Interesting](#parts-i-found-interesting)
5. [Proud Of](#proud-of)
6. [Going Forward](#going-forward)
7. [Setup Locally](#setup-locally)
8. [Swagger Usage](#swagger-usage)

## Design & Implementation Details
CountriesForEveryone is a .NET 8.0 Web API wrapper for the [Countries API](https://restcountries.com/). It provides the following endpoints:
- **GET /api/countries** - Retrieve a list of countries with pagination.
- **GET /api/countries/:code** - Retrieve details for a specific country by its code.
- **GET /api/regions** - Retrieve a list of regions and the countries within each region.
- **GET /api/languages** - Retrieve a list of languages spoken and the countries where they are spoken.

This API focuses on providing the most useful information about countries and includes filters to make your queries fast and efficient, ensuring a great developer experience.

## Architecture
The API design is an N-Layer with traits of clean architecture. The architecture includes:
- **Server Layer**: Hosts the controllers and API setup.
- **Service Layer**: Contains business logic, validations, and optimizations.
- **Repository Layer**: Acts as a connector to the MySQL Database, leveraging Entity Framework for smooth abstractions and setup.
- **Adapter Layer**: Connects to the underlying Countries API for detailed data retrieval and initial data fetching to populate our database. This setup allows us to make queries faster and store only the minimum necessary data, relying on the underlying API only for detailed country data.

Additionally, the architecture includes:
- **Shared Project**: Contains all the DTOs for our API, which is public information useful for interaction without compromising any internal aspects.
- **Core Project**: Houses all interfaces to decouple definitions from implementations, which is crucial for testing.
- **Service Test Project**: Contains unit tests for the service layer, mocking dependencies.
- **Integration Tests Project**: Includes all integration tests to ensure all components work seamlessly together.

## Features Highlight
Extra features of this project include:
- **Rate Limiting**: Limits requests to 30 per minute for all endpoints, configurable via `appsettings`.
- **Cache**: Utilizes InMemory cache for retrieving country details quickly after the first request with a 60-minute expiration.
- **Authentication**: Implements JWT for secure access, with potential for enhanced user validation in the future.
- **Data Sanitization and Validation**: Ensures security through `InputSanitizationMiddleware` and string input validations, protecting against XSS and SQL Injection attacks.
- **CI/CD**: Automated build and deployment through GitHub Actions to an Azure instance, streamlining updates and reducing deployment errors.

## Parts I Found Interesting
The implementation of rate limiting and the setup of CI/CD from scratch were particularly interesting to me, refreshing my knowledge and teaching me a couple new tricks. The rest of the project, while standard in many respects, allowed me to overengineer a bit to demonstrate potential and good practices.

## Proud Of
I am proud of the testing projects and its coverage, the seamless integration with the CI/CD pipeline, and the entities design that optimizes performance and user experience.

## Going Forward
Potential enhancements could include:
- Adding endpoints for Currency and Translation.
- Integrating test runs in the CI/CD pipeline to prevent deployments on failures.
- Adding more tests to the Unit test project to have a higher coverage and guarantee expected functionality.
- Creating user management and enhancing authentication.
- Implementing scaling configurations in Azure for high availability and multi-pod solution.
- Transitioning to a robust cache system like Redis to work with multi-pod cache.

## Setup Locally

**Requirements**: Docker, .NET 8.0, .NET EF tools, and a MySQL client.

### Initial Setup
- **Start Docker Environment**:
  Make sure the Docker engine is running, then navigate to the root directory of the project and execute the following command in a command prompt:
  ```bash
  docker-compose up
  ```
- **Create Database**:
  Create a new Connection with the default values, make sure you put the same user and password as in the appsettings configuration or change it later if you use different values. Then, using a MySQL client, create a new database (schema) named `CountriesForEveryone`.

- **Environment Variables**:
  You don't need to set up any environment variables unless you want to change the value of your connection string (e.g., you want to change the user or password for this DB) or JWT token. You can do this by changing the `appsettings` file or creating equivalent environment variables.
  > **Note**: These are standard and not sensitive values intended for local development only. The Production instance uses environment variables in Azure to override these configurations.

- **Run Migrations**:
  Go to the root directory of the project, open a command prompt, and run the following command:
  ```bash
  dotnet ef database update --project "CountriesForEveryone.Repository"
  ```
- **Build and Run the API**:
  In the same command prompt, execute the following commands to build and run the API:
  ```bash
  dotnet build 
  dotnet run --project "CountriesForEveryone"
  ```
Now you can navigate the Swagger URL and consume the API from your browser at the following link:
[https://localhost:7003/swagger/index.html](https://localhost:7003/swagger/index.html)

### Swagger Usage
- **Obtain JWT Token**:
  Access the `/auth/token` endpoint to get a valid JWT token. Copy the token value from the response.
  
- **Authorize**:
  Click the green 'Authorize' button at the top right corner of the Swagger UI. In the value text input, enter `Bearer <tokenCopiedFromResponse>` (replace `<tokenCopiedFromResponse>` with the token you copied). Click the 'Authorize' button to proceed.
  
- **Access Secured Endpoints**:
  With authentication set, you are now able to use all the secured methods within the API.
