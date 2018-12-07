using Microsoft.Azure;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using OrdersManager.Cloud.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Cloud.Repository
{
    public class TableStorageRepository<T> : ITableStorageRepository<T>
        where T :   TableEntity
    {
        public async Task<T> CreateOrUpdateItemAsync(CloudTable table, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(item);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                T insertedCustomer = result.Result as T;

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">Connection string for the storage service or the emulator</param>
        /// <returns>CloudStorageAccount object</returns>
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
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

        public List<Domain.Entities.Order> ExecuteSimpleQuery(string tableName, string filters)
        {
            try
            {
                // Retrieve storage account information from connection string.
                CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create a table client for interacting with the table service
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create a table client for interacting with the table service 
                CloudTable table = tableClient.GetTableReference(tableName);

                TableContinuationToken token = null;
                var entities = new List<Domain.Entities.Order>();
                do
                {                    
                    var queryResult = table.ExecuteQuerySegmented(new TableQuery<Domain.Entities.Order>().Where(filters), token);
                    entities.AddRange(queryResult.Results);
                    token = queryResult.ContinuationToken;
                } while (token != null);

          
                return entities.ToList();


            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
            catch (Exception e)
            {
                throw e;
            }

        }





        public Tuple<List<T>, int> GetAllAsync(int pageNumber, int pageSize, string filter, bool orderAsc = false, params Expression<Func<T, object>>[] orderByExpressions)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
