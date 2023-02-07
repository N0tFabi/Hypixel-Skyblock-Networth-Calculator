<h1 align="center"> Hypixel-Skyblock-Networth-Calculator</h1>
A simple Program in c sharp that prints the networth of a user in Hypixel Skyblock using the skycrypt API

# Features
```cs
You can get the purse from a Hypixel Skyblock Profile from a certain Account.
```

# How the code works
This is a C# console application that retrieves information about a player's coin purse from the skycrypt API from Hypixel Skyblock.

The code starts by importing the necessary namespaces, System, System.IO, and System.Net. Then it declares a class Program with a property called "PlayerName" and a method called "FormatNumber".

The "FormatNumber" method takes in a string parameter called "number" and formats it to be displayed with thousands separators. For example, if the input is 1234567, the output will be 1 234 567.

In the Main method, the application prompts the user to enter the player name and retrieves the data from the API by sending a GET request. If there's an error while retrieving the data, the error message will be displayed and the program will stop executing.

Otherwise, the response from the API is read and parsed to extract the required information, which includes the player's profile name and purse. The parsed data is then displayed on the console. Finally, the program waits for the user to press any key to close the console.
