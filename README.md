# InterviewConsoleBackup

# EmployeeService
A simple WCF service for managing employee data.

## Technologies
- .NET Framework 4.8
- WCF (Windows Communication Foundation)
- SQL Server / LocalDB
- ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
- Newtonsoft.Json (for serialization, if needed)

## Main Methods

### `GetEmployeeById(int id)`
- Returns `true` if the employee with the given `id` exists.
- Sets HTTP status codes:
  - `200 OK` — employee found
  - `404 Not Found` — employee not found
  - `500 Internal Server Error` — server error

### `EnableEmployee(int id, int enable)`
- Sets the active status of an employee (`enable = 1` — active, `0` — inactive).
- Sets HTTP status codes:
  - `200 OK` — success
  - `500 Internal Server Error` — server error

### `GetQueryResult(string query)`
- Executes a SQL query and returns the result as a `DataTable`.
- Can be used to get all employees:
```csharp
DataTable employees = GetQueryResult("SELECT * FROM Employee");
