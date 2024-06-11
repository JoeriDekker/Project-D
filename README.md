# Water Allocation Manager (WAM)
This is the sourcecode for the Water Allocation Manager (WAM) project. The project is a web application for managing water allocation beneath houses in the Netherlands to prevent the rotting of wooden foundations. The project is developed by a group of students from the University of Applied Sciences Rotterdam.

If you are currently working on this project, please also look at the [Github repository](https://github.com/JoeriDekker/Project-D).


## What is where?
- The source code for the frontend is located in the `client` directory.
- The source code for the backend is located in the `server` directory.
- Unit tests and integration tests are located in the `server.tests` directory.
- Documentation is either found in `server/docs` or at [the online documentation](https://joeridekker.github.io/Project-D).

## How to run the project
### Prerequisites
- Node.js
- npm
- C# (for running the backend)
- Docker (for running the database)
- Docker Compose (for running the database)

### Running the project
1. Clone the repository.
2. Navigate to the `server` directory.
3. Run `dotnet restore` to restore the dependencies.
4. Delete the migrations folder in the `server` directory.
5. Run `docker-compose up -d` inside of the `server` directory to start the database.
6. Edit the `appsettings.json` file in the `server` directory to match the connection string of the database.
7. Run `dotnet ef migrations add InitialCreate` to create the initial migration.
8. Run `dotnet ef database update` to apply the migration to the database.
9. Run `dotnet run` to start the backend.
10. Navigate to the `client` directory.
11. Run `npm install` to install the dependencies.
12. Edit `.env` file in the `client` directory to match the backend URL. It should be `REACT_APP_API_URL=http://localhost:5000`.
13. Run `npm start` to start the frontend.
14. Navigate to `http://localhost:3000` in your browser to view the application.

## How to deploy the project
### Prequisites
- Docker
- Docker Compose
- (Maybe Nginx with a reverse proxy pointing to the frontend)

### Deploying the project
1. Clone the repository.
2. Change the `.env` file in the `client` directory to match the backend URL. It should be `REACT_APP_API_URL=http://URL_OF_YOUR_SITE:5000`.
3. Change the `appsettings.json` file in the `server` directory to match the connection string of the database. The host url should be `db`.
4. Run `sh deploy.sh` to build the Docker images and start the containers.


## Development progress branching rules
## Branches
- main: The main branch, production. Code has to be stable and tested before merging to main. Only gets merged to whenever a new release is ready.
- dev: The development branch. All feature branches should be branched off from this branch. This branch is merged to main when a new release is ready.
- feature branches: Branches for developing new features. Should be branched off from dev. When the feature is ready, it should be merged back to dev.

## Commit messages
- Use present tense, imperative mood. E.g. "Add feature" instead of "Added feature".
- Keep it short and concise. If more information is needed, use the description field.
- If using the description field, use bullet points for each point.

## Endpoints
### Anonymous
These endpoints do not require any authentication.
- [/api/login](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.LoginController.html)
    - POST: Logs in a user. Returns a bearer token.
- [/api/register](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.RegisterController.html)
    - POST: Registers a new user.
    - GET /confirm?userid={userid}&token={token}: Confirms the email address of a user.
### Authenticated
All these endpoints requirer a bearer token in the Authorization header. This token is linked to a user, this relation may be used to determine the user that is making the request.
- [/api/actionlog](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.ActionLogController.html):
    - GET: Get all action logs of the authenticated user.
- [/api/address](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.AddressController.html):
    - PUT: Update the address of the authenticated user.
- [/api/controlpc](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.ControlPCController.html)
    - POST: Creates a new controlpc.  
    - DELETE {id}: Deletes a controlpc.
    - GET: Gets all controlpcs.
    - PUT {id}: Updates a controlpc.
- [/api/groundwaterlog](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.GroundWaterLogController.html)
    - GET: Gets all groundwaterlogs.
- [/api/users](https://joeridekker.github.io/Project-D/api/WAMServer.Controllers.UsersController.html)
    - GET: Gets the authenticated user.