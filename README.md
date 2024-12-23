# Candidate Hub
## Overview
This project demonstrates the implementation of 'Candidate Hub API' where information of candidates are stored and viewed.

## Technical Overview
- Dot Net 8
- Entity Framework Core
- SQL Lite
- XUnit

## Usage
Swagger Documentation `/swagger/index.html`
API Endpoints
- GET `/api/candidates`
    - Retrieves paged data
- POST `/api/candidates`
    - Upsert (Insert or Update record).
    - JSON Body:
    ```json
      {
        "id": 0,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "phoneNumber": "string",
        "callTimeInterval": "string",
        "linkedInUrl": "string",
        "gitHubUrl": "string",
        "comments": "string"
      }

## Installation
1. Clone the repository:
    - `git clone https://github.com/chaukate/candidate-hub.git`
2. Navigate to the project directory:
    - `cd candidate-hub`
3. Install dependencies:
    - `dotnet restore`
5. Create a appsettings.Development.json:
    - Copy or create appsettings.Development.json file in CandidateHub.Api project.
    - Copy content in appsettings.Example.json to appsettings.Developmentjson file.
    - Replace the value of CHub in ConnectionStrings.
    - Since this application uses SQL Lite by default, give the connection string value as `Data Source=app.db;`.
6. Apply migration and update the database:
    - `dotnet ef database update`
7. Test the project:
    - `dotnet test`
8. Run the project:
    - `dotnet run`
