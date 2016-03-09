# Creating a Virtual Machine using an ARM Template

This demo shows how to create a resource group & a new virtual machine using the Azure CLI. At the same time use the verbose output to view the underlying Azure ARM REST API calls.


create a new parameter file
find a template - virtual machine preferred
login to cli
create a new resource group with -vv
create a new deployment using the template 7 parameter file -vv
view it's status in the CLI -vv
view it in the portal

1. Verify you are in ARM mode...

    ```shell
    azure config mode arm
    ```

1. Call help again and see how many more commands are available:

    ```shell
    azure help
    ```

1. Login to Azure:

    ````shell
    azure login
    ````

1. Switch to the desired subscription:

    ````shell
    azure account
    azure account list
    azure account set <id of a subscription>
    azure account show
    ````

1. Create a new resource group that will hold the virtual machine:

    ````shell
    azure group create --name "new-vm-group" --location "eastus"
    ````

Now create a new simple Linux VM using an existing template. First, find the template in the gallery, explore it's source in GitHub, make some changes to the template and then use the changed version.

1. Open a browser and navigate to https://azure.microsoft.com/documentation/templates/101-vm-simple-linux/

  - This page will provide some information about the template including a description, parameters & how to run it using PowerShell or the CLI.

1. Click the link **Learn more on GitHub** to get to the source of this template.

  - Take note of two files on this page:
    - `azuredeploy.json`: this is the ARM template to create the resource
    - `azuredeploy.parameters.json`: this is the parameter file you can feed into the provisioning process

1. Select the `azuredeploy.parameters.json` file to see the options you can specify. It shows you can specify the admin username, password, DNS label prefix and the version of the OS.
1. Select the `azuredeploy.json` file to see what the template looks like.
1. Looking at an ARM template can be hard to visualize. From the homepage of template in the repo, notice the **Visualize** button. This will take you to a graphical interface where you can explore the template.

  Click the **Visualize** button and explore.

1. On the left side of the **Azure Resource Manager Template Visualizer**, select the **Edit Parameter Definitions** button. This will open a dialog for you to modify the parameters. 
  1. Set the `adminUsername` & `adminPassword` for the administrator account.
  1. Set the name of the `dnsLabelPrefix` to: **muUmbuntuVm**
  1. After making changes, save the parameters file to your local machine. A sample can be seen in the [parameters.json](parameters.json) file.

1. Next, double click on the resources within the visualizer. You can see how the dependencies are organized and the different settings. You can even make changes to them using this tool.

1. If you don't make any changes to the template you can just keep track of the raw URL for the template that you will pass into the CLI provisioning engine. The other option is to download the template.
  - To get the URL of the template, go back and select it from the repository in GitHub and then click the **Raw** button in the top-right corner. You should get a URL like this: https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/101-vm-simple-linux/azuredeploy.json

1. Now create the virtual machine using the template. You should already be in ARM mode from the above commands. Create the VM by running the following at the command line:

  > *Lines have been broken up for readability, but make sure you enter everything on one line.*

  ```shell
  azure group deployment create 
      -g "new-vm-group" 
      -n "umbuntu-vm-01" 
      -e parameters.json 
      --template-uri https://raw.githubusercontent.com/azure/azure-quickstart-templates/master/101-vm-simple-linux/azuredeploy.json
  ```

1. You may notice there was a problem... but there isn't much information here explaining what happened so let's look at the logs. The following command will tell Azure to show the logs for the last deployment in this group:

  ```shell
  azure group log show -g "new-vm-group" --last-deployment
  ```

1. The logs are presented in reverse order so the most recent log entry is at the top and the oldest is at the bottom. Start slowly scrolling up investigating the **Status** field. Eventually you will get to an error... investigating the error message you can see the following:

  ```json
  {"error":
    {
      "code":"InvalidDomainNameLabel",
      "message":"The domain name label myUmbuntuVm is invalid. 
                It must conform to the following regular expression: 
                ^[a-z][a-z0-9-]{1,61}[a-z0-9]$.",
      "details":[]
    }
  }
  ```

  The name we used has capital letters which is clearly not allowed.

1. Delete and recreate the resource group:

  ```shell
  azure group delete -n "new-vm-group"
  azure group create --name "new-vm-group" --location "eastus"
  ```

1. Update the `parameters.json` file to set the `dnsLabelPrefix` to **myumbuntuvm**, save your changes and rerun the script, except this time add two more parameters:
  - `--nowait`: tell the CLI to create the deployment, but don't wait for it to complete
  - `-vv`: tell the CLI to out put all the HTTP requests to the Azure ARM REST API

  ```shell
  azure group deployment create 
      -g "new-vm-group" 
      -n "umbuntu-vm-01" 
      -e parameters.json
      --template-uri https://raw.githubusercontent.com/azure/azure-quickstart-templates/master/101-vm-simple-linux/azuredeploy.json
      --nowait
      -vv 
  ```

1. While we're waiting for the deployment to complete, we can check the status of it:

  ```shell
  azure group deployment list -g "new-vm-group"
  ```

1. After a while you will see your all the resources within your resource group as well as your new virtual machine.