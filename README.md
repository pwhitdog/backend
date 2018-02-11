# Backend

This is my default backend for using dotnet core with postgres sql and identity framework. Code first works and this is cross platform.

## How to run

Running this should be a pretty straight forward and easy.

1. Get postgresql running locally and save port it is listening to.

2. Change connection string to match postgresql database.

3. Change admin information in code so everyone doesn't use the same!

4. Run the backend!

## What it does

Currently the project runs a register command that will create a customer. It also loads with a default admin. The security it is using is JWT authentication. From here you can create web api endpoints and set up either to open or locked down to logged in people and further lock them down by their role. 

Enjoy!

Paul Whitmer 
> YNWA