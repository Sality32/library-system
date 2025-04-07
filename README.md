# Library System
This is backend service to provide library system that included Authentication, Role base authorization, Borrowing lifecycle, and Request new collection books data.

## Structure Projects
This projects using Clean Architecture

```
Solution
- Application Project
-- Dependencies (Domain)
-- DTOs
-- Services
- Domain Project
-- Dependencies ()
-- Entities
-- Interfaces
- Infrastructure Project
-- Dependencies (Application, Domain)
-- Migrations
-- Persistence
-- Repositories
- Application Project
-- Dependencies (Application, Infrastructure, Domain)
-- appsetting.json
-- program.cs
-- Controller
``` 


## Features

- Authentication and Authorization
- Get List book (user)
- Get List book (admin, officer)
- Create Request Borrow Book (user)
- Get Request Borrow Book (admin, officer)
- Get Request Borrow Book (user)
- Approval Request Borrow (admin, officer)
- Create Request new Collection book (admin, officer)
- Approval new collection book (admin)
## Tech

- .Net Core 8.0
- ASP Net Core
- MsSQL
- Postman
- Visual Studio 2022
- Redis

## Environment Variables

To run this project, you will need to add the following environment variables to your appsetting.json file (or can copy from appsetting.json.example)

`ConnectionStrings > DefaultConnection` 
fill the string connection with your credential MsSQL
`ConnectionStrings > RedisConnection` 
fill the string connection with your credential Redis

## Progres Development
Pushed

    Authentication and Authorization
    Get List book (user)
    Get List book (admin, officer)
    Create Request Borrow Book (user)
    Get Request Borrow Book (admin, officer)
    Get Request Borrow Book (user)
    Approval Request Borrow (admin, officer)
    
Debt

    Create Request new Collection book (admin, officer)
    Approval new collection book (admin)
    Implementation Redis on request borrow book


## Related

Here are some related projects

[CleanCode by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

