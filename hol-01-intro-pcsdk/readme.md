# Hands On Lab #1 - Introduction to the Partner Center SDK

The Partner Center SDK provides a superset of the earlier CREST API functionality and enables partners to connect their sales and billing systems to Microsoft for the purposes of selling Microsoft Commercial Cloud software. This Visual Studio based lab takes you through a simple application that the partner would write which creates a customer and sells that customer a new subscription using the Partner Center SDK. Some experience with Visual Studio and C# is required.

> You can find a completed version of the solution that you would have at the conclusion of the lab in the [completed](completed) folder. Any settings, such as IDs, passwords, keys and other partner-specific values have been removed from the completed solution, but if you run into trouble you can use the final solution as a template to compare your work against if you have trouble.

This Hands on Lab is hosted online [here](http://aka.ms/pcsdkhol) 

## Exercise 1: Create an Azure AD Application

In this exercise you will create a new Azure AD application, granting it access to the Partner Center.

## Exercise 2: Create Customers with a C# Console App

In this exercise you will create a C# console application that will use the Partner Center SDK to create a customer. Then you will inspect the raw HTTP requests made by the SDK using Fiddler.

## Exercise 3: Browse & Select Office 365 Offers

In this exercise you will take the console application created in the previous exercise and extend it to list the available Office 365 Offers. After displaying the offers, the console app will be extended to let the user add an offer to their Office 365 subscription.

## Exercise 4: Place an Order for an Office 365 Subscription and Offer Add-on

In this exercise you will take the console application you have been working with in this hands-on-lab and extend it to place an order for a selected Office 365 subscription & Office 365 add-on offer.

[Get started on the lab!](lab.md)
