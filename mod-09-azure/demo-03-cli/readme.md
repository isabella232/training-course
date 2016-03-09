# Azure Command Line Interface

This demo shows how to do simple operations with the Azure Command Line Interface (CLI)

1. Open a command prompt and show how to get the Azure CLI
  - You need Node.js & NPM installed
    - https://nodejs.org
    - https://azure.microsoft.com/documentation/articles/xplat-cli-install/
  - Execute it and get basic help:

    ```shell
    azure
    ```

1. Get detailed help for a specific command

    ```shell
    azure login -h
    ```

1. Take note of the current mode... it's listed in the footer of the commands
1. Now, switch modes from ASM to ARM

    ```shell
    azure config mode arm
    ```

1. Call help again and see how many more commands are available:

    ```shell
    azure help
    ```

1. Login

    ````shell
    azure login
    ````

1. See a list of all your subscriptions & switch to a specific one

    ````shell
    azure account
    azure account list
    azure account set <id of a subscription>
    azure account show
    ````

1. List all resource groups

    ````shell
    azure group -h
    azure group list
    ````

1. Get all resource groups in JSON format

    ````shell
    azure group --json
    ````

1. See what the underlying call & data returned looks like

    ````shell
    azure group list -vv
    ````

1. Create a new resource group

    ````shell
    azure group create --name "test group" -l "eastus"
    ````