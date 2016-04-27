# Hands On Lab #5 - Adding Resources to a new Azure Subscription

The Partner Center SDK provides abilities to order licenses for Office 365 and other license-based services offered by Microsoft. Partners can also use the SDK to create Azure subscriptions, but creating and management of resources in the Azure subscription must be done using the Azure REST APIs.

This lab shows how you would create Azure resources using the Azure Resource Manager (ARM) to an Azure subscription. It shows how to ensure that the Azure subscription is properly provisioned prior to resource provisioning and how to set appropriate access permissions on the Azure subscription for programmatic resource creation.

> You can find a completed version of the solution that you would have at the conclusion of the lab in the [completed](completed) folder. Any settings, such as IDs, passwords, keys and other partner-specific values have been removed from the completed solution, but if you run into trouble you can use the final solution as a template to compare your work against if you have trouble.

## Exercise 1: Create ASP.NET Application and Configure Authentication with Azure Active Directory

In this exercise you will create a new ASP.NET MVC application. You will then configure an Azure AD application with the necessary permissions to call the Azure REST APIs. At the end of this exercise you will write the code to obtain an access token to call the Azure REST APIs.

## Exercise 2: Create a Resource Group with the Azure REST APIs

In this exercise you will take the ASP.NET application you configured in the previous exercise in this hands-on-lab and update it to create a new resource group within you Azure subscription. This resource group will be used to contain the resources you will provision using an ARM template in the next exercise.

## Exercise 3: Provision an Azure Virtual Machine using ARM Templates

In this exercise you will locate an ARM template for provisioning a virtual machine in the resource group you created in the previously exercise. You will then use the Azure Service Management REST API to provision the virtual machine using an ARM template.

[Get started on the lab!](lab.md)