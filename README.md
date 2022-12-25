## Description

Short URLs API (.NET version);

By providing a long url (f.e. search results) will generate uuid f.e (E780PHm).
There are no speciific settings needs to be done from a box. Only option to adjust can be found in launchSettings.json
can\need to adjust PORTs

```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:3310",
      "sslPort": 44303
    }
  },
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "api",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "ShortUrl.API": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "api",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    },
    "Docker": {
      "commandName": "Docker",
      "launchBrowser": true,
      "launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}/api",
      "publishAllPorts": true,
      "useSSL": true
    }
  }
}
```

## Running the API locally

1) Check-out the repo
2) Build the solution
3) Run "IIS Express" <img width="208" alt="image" src="https://user-images.githubusercontent.com/12031453/161424941-9c3993cf-5eb6-4749-a0f0-3d4229b82b26.png">

## Available end-points
1. GET http://localhost:3310/api/
RESPONSE 200
```
"Ping-Pong"
```
2. POST http://localhost:3310/api
BODY
```json
{
    "link": "https://github.com/mbdavid/LiteDB"
}
```
RESPONSE 200 OK
```json
{
    "uuid": "E780PHm"
}
```
3. GET https://localhost:44303/api/{uuid}
RESPONSE 200 OK
```json
{
    "uUid": "E780PHm",
    "created_on": "2022-04-03T12:56:29.373+02:00",
    "requested": 1,
    "originalUrl": "https://github.com/mbdavid/LiteDB4235"
}
```
4. GET https://localhost:44303/api/analytic?host=github.com
RESPONSE 200 OK
```json
{
    "host": "github.com",
    "generatedShortenLinksCount": 2,
    "generatedLinks": [{
            "link": "https://github.com/mbdavid/LiteDB",
            "accessed": 0,
            "createdOn": "2022-04-03T12:46:24.791+02:00"
        }, {
            "link": "https://github.com/mbdavid/LiteDB4235",
            "accessed": 0,
            "createdOn": "2022-04-03T12:56:29.373+02:00"
        }
    ]
}
```


## Author
Vitalii Vepretskyi
