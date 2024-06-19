Test Case - .NET Tech Lead
Overview
Please build a service which would provide REST API and WebSocket endpoints for live financial instrument prices sourced from a public data provider and will efficiently handle over 1,000 subscribers.
Requirements
1. REST API:
● Endpoint to get a list of available financial instruments. Take just a few, for example:
EURUSD, USDJPY, BTCUSD - will be sufficient
● Endpoint to get the current price of a specific financial instrument.
2. WebSocket Service:
● Subscribe to live price updates for a specific financial instrument(s) from the list above.
● Broadcast price updates to all subscribed clients.
3. Data Source:
● Use a public API like Alpha Vantage or CEX.io to fetch live price data.
4. Performance:
● Efficiently manage 1,000+ WebSocket subscribers with a single connection to the data
provider. There is no need to simulate such a workload, just make comments in the code where you will handle that
5. Logging and error reporting:
● Please implement event and error logging capabilities. Level of details and message
structure is up to you
● No need to setup any logging platform, it’s ok to stream events to the console stdout
Further details
● Time for implementation: up to 4 days. If you need more time, please let us know.
● The result should look like:
● Github repository with the code and instruction on how to run it on local computer
● Any additional documents should be provided either as a shared Google Doc or
in the email or in PDF attached to the email.
● Any secrets (for example API key) should be provided in reply email

Prerequisites to run:
1. .NET SDK 8.0
2. Visual Studio or VS Code witth "C# Dev Kit" and ".NET Install tool" and "C#" extensions

In order to test the WebSocket one can use one of the following options:
1. Run the code in "Debug" mode
2. Use the following code ona browser console:

    var socket = new WebSocket("wss://localhost:7263/ws?instrument=EURUSD");
    
    socket.onopen = function(event) {
        console.log("Connection established.");
    };
    
    socket.onmessage = function(event) {
        var priceUpdate = JSON.parse(event.data);
        console.log("Price update: ", priceUpdate);
    };
    
    socket.onclose = function(event) {
        console.log("Connection closed: ", event);
    };

API Key: EVIIKA1W3PSN5UEE Key also exists in the appsettings.json file. There is no need to add it.

Note: There is definitely space for improvements in the existing code such as centralized error handling for Websokets. However, for the sake of not missing deadline, those implementations are omitted.
Note 2:
● The AlphaVantage api request may not produce a response as the free key allows only 25 requests per day only. 
● In order to get proper response one can generate a free API key using the following url and paste it to the app.settings.json file: 
    https://www.alphavantage.co/support/#api-key
