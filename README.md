The repository contains both projects for both the Frontend and Backend. 

The Backend is a C# Web API Project and the Frontend is an Angular CLI project.

To run the project, clone the repo or download the files. Open the backend in Visual Studio and in the appsettings.json, change the connection string to match yours. Navigate to the package manager console in the Tools tab and run the command 'Add-migration' <YourMigrationName> to create the database. Build and run the project in either Http or Https. For the frontend, open the file in Visual Studio and navigate to the console and run the command 'npm install' or 'npm i'. Once the packages have been installed, go to the services pages/classes and change the XXXX in the localhost URL to match the port number found in swaggerUI, when the the Backend is running. Once changed, run the command 'ng serve --o' to build and run the application.
