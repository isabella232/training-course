# Azure PowerShell

This demo shows how to do simple operations with the Azure PowerShell interface.

1. Open a command prompt and show how to get the Azure PowerShell
  - You need the Azure PowerShell Module installed: https://azure.microsoft.com/en-us/documentation/articles/powershell-install-configure/
  - Execute it and get basic help:

    ```shell
    Get-Help Azure
    ```

1. Get detailed help for a specific command

    ```shell
    Get-Help Login-AzureRmAccount
    ```

1. Get list of all commands related to ARM

    ```shell
    Get-Command -Module AzureRM.Resources | Get-Help | Format-Table
    ```

1. Login

    ````shell
    Login-AzureRmAccount
    ````

1. See a list of all your subscriptions & switch to a specific one

    ````shell
    Get-AzureRmSubscription
    Get-AzureRmSubscription -SubscriptionName <name of subscription> | Select-AzureRmSubscription
    ````

1. List all resource groups

    ````shell
    Get-AzureRmResourceGroup
    ````

1. Create a new resource group

    ````shell
    azure group create --name "test group" -l "eastus"
    ````