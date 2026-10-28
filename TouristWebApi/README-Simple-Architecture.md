# Tourist Web API: simple steps

This guide follows the existing TouristWebApi project and adapts the attached architecture picture. The example data is Tourist Places and Users, which are the tables in this project.

Open `TouristWebApi-Architecture.svg` in a browser to view the project diagram.

## How a request works

1. The client sends a request to the API.
2. JWT checks a token when the endpoint is protected.
3. The controller receives the request and checks the input DTO.
4. The service performs the tourist-place work.
5. Entity Framework Core reads or saves data through `Tourist_DbContext`.
6. SQL Server stores the Places and Users tables.

## Tourist endpoints

- `GET /api/places` returns all places.
- `GET /api/places/above-altitude/1000` returns places higher than 1,000 altitude units. Send a JWT bearer token.
- `POST /api/places` adds a place. Send a JWT bearer token and a JSON body such as:

```json
{
  "name": "Ooty",
  "location": "Tamil Nadu",
  "alitude": 2240,
  "description": "A hill station in the Nilgiri Hills."
}
```

## Main files

- `Program.cs`: connects SQL Server and registers JWT, controllers, and services.
- `Models/place.cs` and `Models/User.cs`: describe database records.
- `Dbcontext/Tourist_DbContext.cs`: connects the models to EF Core.
- `Tourist_DTO/`: request and response data, including validation.
- `Services/`: reads and saves tourist place data.
- `Controllers/`: receives API calls and returns responses.

## Create or update the database

Run from the API project directory:

```powershell
dotnet ef migrations add TouristChanges
dotnet ef database update
dotnet run
```

The API uses the SQL Server connection string and JWT settings in `appsettings.json`. Change the database server name to match your machine. Keep real secrets outside source control.

The reference image shows Employees, Attendance, and LeaveRequests. Those tables are not present in this Tourist API, so this adapted diagram correctly shows Places and Users.
