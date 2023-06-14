# Runtime
Download runtime you may here https://dotnet.microsoft.com/en-us/download/dotnet/6.0

# How to Build & Run
To build applicaition:

1. Open console

2. Set solution folder as current in console

3. Publish app `dotnet publish -c Release`

4. Change current console folder `cd src/WebAPI/bin/Release/net6.0/publish`

5. Run app `dotnet WebAPI.dll`

# Database migrations
[Liquibase](https://docs.liquibase.com/home.html "liquibase") is use for versioning database schema.
1.  Open console and set `.\database` as current folder.
2. Exec `liquibase <your command> --changelog-file=changelog.xml --url="<database connection info>" --username=<value> --password=<value>`

Where `<your command>`:

- For update schema to last version:`update`

- For update schema to specific version:`update-to-tag v1.0.0`

- For rollback schema `liquibase rollback v1.0.0`

All tags you may see in `changelog.xml` file.

**Full example: **
```bash
liquibase update --changelog-file=changelog.xml --url="jdbc:postgresql://localhost:5432/test" --username=postgres --password=postgres
```

# Useful app links

- `/swagger/index.html`- api documentation

-  `/metrics` - app metrics collected by Prometheus


# Configuration

```json
{
"ConnectionStrings": {
    "DataDb": "<postgres connection string for app data>",
    "LogsDb": "<postgres connection string for app logs>"
  },
  "Authorization":{
    "Header": "<Authorization header>"
  },
  "Kestrel": {
    "EndPoints": {
      "Http": {
        "Url": "<App endpoint>"
      }
    }
  },
  "Serilog": {
    "Using": [
      "Serilog.Sinks.PostgreSQL.Configuration"
    ],
    "MinimumLevel":{
      "Default": "<Minimum logging level>"
    }
}
```