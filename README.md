Test Case - .NET Tech Lead
Overview
Please build a service which would provide REST API and WebSocket endpoints for live financial instrument prices sourced from a public data provider and will efficiently handle over 1,000 subscribers.
Requirements
1. REST API:
   - Endpoint to get a list of available financial instruments. Take just a few, for example: EURUSD, USDJPY, BTCUSD - will be sufficient
   - Endpoint to get the current price of a specific financial instrument.
3. WebSocket Service:
 - Subscribe to live price updates for a specific financial instrument(s) from the list above.
 - Broadcast price updates to all subscribed clients.
6. Data Source:
 - Use a public API like Alpha Vantage or CEX.io to fetch live price data.
7. Performance:
 - Efficiently manage 1,000+ WebSocket subscribers with a single connection to the data
provider. There is no need to simulate such a workload, just make comments in the code where you will handle that
8. Logging and error reporting:
 - Please implement event and error logging capabilities. Level of details and message
structure is up to you
 - No need to setup any logging platform, it’s ok to stream events to the console stdout
Further details
 - Time for implementation: up to 4 days. If you need more time, please let us know.
 - The result should look like:
 - Github repository with the code and instruction on how to run it on local computer
 - Any additional documents should be provided either as a shared Google Doc or
in the email or in PDF attached to the email.
 - Any secrets (for example API key) should be provided in reply email

Prerequisites to run:
1. .NET SDK 8.0
2. Visual Studio or VS Code with "C# Dev Kit", ".NET Install tool" and "C#" extensions

Running the project:
* Either use GUI of an IDE like Visual Studio or VS Code
* Or use terminal:
- navigate to "TestCase" directory from the project root directory
- change environment to "development" by running "export ASPNETCORE_ENVIRONMENT=Development" command
- run "dotnet build" & "dotnet run" commands consecutively
- navigate to "http://localhost:5265/swagger/index.html" url on any modern browser to test the API endpoints using Swagger UI


In order to test the WebSocket one can use one of the following options:
1. Use WebSocket Client Library
 - Step 1: Create a new Console Application
 - Step 2: Add System.Net.WebSockets.Client package
 - Step 3: Write a test client using Console App
 - Step 4: Run the Console Application

2. Use "Postman":
 - Open Postman.
 - Click on the "New" button and select "WebSocket Request".
 - Enter the WebSocket URL, for ex: (wss://localhost:5265/ws?instrument=BTCUSD).
 - Click "Connect".

3. Use Browser Developer Tools:
 - Open the browser's developer tools (F12).
 - Go to the "Console" tab.
 - Enter the following JavaScript code to connect and test the WebSocket:

    var socket = new WebSocket("wss://localhost:5265/ws?instrument=BTCUSD");

    socket.onopen = function(event) {
        console.log("Connected to WebSocket server.");
    };
    
    socket.onmessage = function(event) {
        console.log("Received: " + event.data);
    };
    
    socket.onclose = function(event) {
        console.log("Disconnected from WebSocket server.");
    };
    
    socket.onerror = function(error) {
        console.log("WebSocket error: " + error);
    };


API Key: 
 - EVIIKA1W3PSN5UEE Key also exists in the appsettings.json file. There is no need to add it.
 - The AlphaVantage api request may not produce a response as the free key allows 25 requests per day only.
 - In order to get proper response one can generate a free API key using the following url and paste it to the app.settings.json file: 
    https://www.alphavantage.co/support/#api-key

Note: There is definitely space for improvements in the existing code such as centralized error handling for Websokets. However, for the sake of not missing deadline, those implementations are omitted.


