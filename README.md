> <h1 style="font-size:50px;"><b>Azure Function App - Task Management API Documentation</b></h1>



> **Overview**

The Task Management API is an Azure Function App designed to provide a serverless solution for managing tasks efficiently. It offers CRUD operations for tasks, ensuring scalability, cost-effectiveness, and high availability using Azure Functions, Azure Cosmos DB, and Azure Storage.

> **Features**

Serverless execution model with Azure Functions

HTTP-triggered functions for API endpoints

Data storage using Azure Cosmos DB

Secure authentication using Azure Active Directory (Azure AD)

Logging and monitoring using Azure Application Insights

> **Architecture**

**Azure Function App:** Hosts and runs HTTP-triggered functions

**Azure Cosmos DB:** Stores task data

**Azure Storage:** Manages function-related files/logs

**Azure AD:** Handles authentication and authorization

**Azure Application Insights:** Provides logging and monitoring

**API Management (optional):** For enhanced security and throttling

> **API Endpoints**

**1. Get All Tasks**

Method: GET

Endpoint: /api/tasks

Description: Retrieves a list of all tasks

Response: JSON list of tasks

**2. Get Task by ID**

Method: GET

Endpoint: /api/tasks/{id}

Description: Retrieves a specific task by ID

Response: JSON object containing task details

**3. Create a New Task**

Method: POST

Endpoint: /api/tasks

Description: Creates a new task

Request Body:

{
  "Title": "Sample Task 1",
  "Description": "This is a sample task 1 description.",
  "IsCompleted": false
}

Response: Created task object with ID

**4. Update an Existing Task**

Method: PUT

Endpoint: /api/tasks/{id}

Description: Updates an existing task

Request Body:

{
  "Title": "Sample Task 1",
  "Description": "This is a sample task 1 description.",
  "IsCompleted": false
}

Response: Updated task object

**5. Delete a Task**

Method: DELETE

Endpoint: /api/tasks/{id}

Description: Deletes a specific task
