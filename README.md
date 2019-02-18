# Interact with Azure Queue and Table storage using WPF , and azure functions


About this sample

# Scenario

This sample demonstrates a wpf application calling to azure queue and then automatically trigger a azure function that save history of chat in table storage.  
  
1-Chat wpf application, add message to de queue.  
2-Azure function trigger on server, parse the message and save message to history table storage.  
3-Chat wpf application query table storage to view history.  

![alt text](https://github.com/ntiseira/AzureStorageWithAzureFunctionsSample/blob/master/ReadmeFiles/azureFunctionsDiagram.png)

**ChatRoom app**

![alt text](https://github.com/ntiseira/AzureStorageWithAzureFunctionsSample/blob/master/ReadmeFiles/chatRoomApp.jpg)


**Azure function parse chat message**

![alt text](https://github.com/ntiseira/AzureStorageWithAzureFunctionsSample/blob/master/ReadmeFiles/azureFunctionConsole.jpg)



How to run this sample To run this sample, you'll need:

-Visual Studio 2017
-An Internet connection
-Azure storage account
-App services to register azure function.


**Step 1: Clone or download this repository**

git clone https://github.com/ntiseira/AzureStorageWithAzureFunctionsSample.git

**Step 2: Create azure queue**

Add queue with name "chatroom"

![alt text](https://github.com/ntiseira/AzureStorageWithAzureFunctionsSample/blob/master/ReadmeFiles/storageQueue.jpg)


**Step 3: Configure the sample to use your Azure storage**

Open FE/ChatRoom/App.config

-Find the app key StorageConnectionString and replace the existing value with your connection string of azure storage.

**Step 4: Run the sample**

*WPF: ChatRoom app*

Clean the solution, rebuild the solution, and run it. 


*Azure functions: ChatParserFunction*

Publish app services, the steps for publish the app are in the next link https://docs.microsoft.com/en-us/visualstudio/deployment/quickstart-deploy-to-azure?view=vs-2017



**Community Help and Support**

Use Stack Overflow to get support from the community. Ask your questions on Stack Overflow first and browse existing issues to see if someone has asked your question before. Make sure that your questions or comments are tagged with [adal dotnet].

If you find a bug in the sample, please raise the issue on GitHub Issues.

**Contributing**
- If you wish to make code changes to samples, or contribute something new, please follow the [GitHub Forks / Pull requests model](https://help.github.com/articles/fork-a-repo/): Fork the sample repo, make the change and propose it back by submitting a pull request.










