using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using ChatRoom.Cloud.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Cloud.Repository
{
    public class QueueStorageRepository
        //<T> 
        : IQueueStorageRepository
    //<T>
    //where T :   TableEntity
    {

        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string</param>
        /// <returns>CloudStorageAccount object</returns>
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public CloudQueue GetClientQueue()
        {
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = cloudQueueClient.GetQueueReference("chatroom");

            return queue;
        }

        public async Task AddMessageAsync(string message)
        {
              await GetClientQueue().AddMessageAsync(new CloudQueueMessage(message));
        }

        public async Task DeleteMessageAsync(string message)
        {
            await GetClientQueue().DeleteMessageAsync(new CloudQueueMessage(message));
        }
    }
}
