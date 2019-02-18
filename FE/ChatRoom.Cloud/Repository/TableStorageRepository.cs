using ChatRoom.Cloud.Interfaces;
using ChatRoom.Domain.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Cloud.Repository
{
    public class TableStorageRepository : ITableStorageRepository
    {
        /// <summary>
        /// Validate the connection string information in app.config and throws an exception if it looks like 
        /// the user hasn't updated this to valid values. 
        /// </summary>
        /// <param name="storageConnectionString">Connection string for the storage service or the emulator</param>
        /// <returns>CloudStorageAccount object</returns>
        public  CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
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


        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="table">The sample table name</param>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public  async Task<UserEntity> UpdateEntityAsync(CloudTable table, UserEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.Replace(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                UserEntity insertedCustomer = result.Result as UserEntity;

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }


        private  async Task BatchInsertOfCustomerEntitiesAsync(CloudTable table , List<UserEntity>listUserEntities)
        {
            try
            {
                // Create the batch operation. 
                TableBatchOperation batchOperation = new TableBatchOperation();

                // The following code  generates test data for use during the query samples.  
               foreach(var item in listUserEntities)
                {
                    batchOperation.Replace(item);                    
                }

                // Execute the batch operation.
                IList<TableResult> results = await table.ExecuteBatchAsync(batchOperation);

                foreach (var res in results)
                {
                    var customerInserted = res.Result as UserEntity;
                    Console.WriteLine("Inserted entity with\t Etag = {0} and PartitionKey = {1}, RowKey = {2}", customerInserted.ETag, customerInserted.PartitionKey, customerInserted.RowKey);
                }
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }


        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="rowKey">Row key - i.e., first name</param>
        /// <returns>A Task object</returns>
        public  List<UserEntity> RetrieveEntitiesUsingPointQueryAsync(CloudTable table, string alias)
        {
            try
            {
                // Create the range query using the fluid API 
                TableQuery<UserEntity> rangeQuery = new TableQuery<UserEntity>().Where(
                    //Row key defined the user name in table storage created
                    TableQuery.CombineFilters(      
                            TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.NotEqual, alias),
                            TableOperators.And,        
                            //We want get the messages not read
                                TableQuery.GenerateFilterConditionForBool("MessageChecked", QueryComparisons.Equal, false)
                            ));

                TableContinuationToken token = null;
                var entities = new List<UserEntity>();
                do
                {
                    var queryResult = table.ExecuteQuerySegmentedAsync(rangeQuery, token).Result;
                    entities.AddRange(queryResult.Results);
                    token = queryResult.ContinuationToken;
                } while (token != null);
                
              
                return entities;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public  TableEntity Merge(CloudTable table , TableEntity entity)   
        {

            // Create the InsertOrReplace table operation
            TableOperation insertOrMergeOperation = TableOperation.Replace(entity);

            // Execute the operation.
            TableResult result =  table.ExecuteAsync(insertOrMergeOperation).Result;
            UserEntity insertedCustomer = result.Result as UserEntity;

            return insertedCustomer; 
        }


        private CloudTable MakeTableReference()
        {
            string tableName = "UsersChat";

            // Retrieve storage account information from connection string.

            var tableClient = CreateStorageAccountFromConnectionString(CloudConfigurationManager.GetSetting("StorageConnectionString")).CreateCloudTableClient();

            // Create a table client for interacting with the table service
            var tableReference = tableClient.GetTableReference(tableName);

            return tableReference;
        }


        public async Task<List<UserEntity>> GetNewsAsync(string alias)
        {  
            // Create a table client for interacting with the table service 
            CloudTable table = MakeTableReference();
            
            List<UserEntity> listUsers = new List<UserEntity>();

            listUsers = this.RetrieveEntitiesUsingPointQueryAsync(table, alias);
                       
            if (listUsers.Count > 0)
            { 
                 foreach (var item in listUsers)
                {                    
                    item.MessageChecked = true;
                     Merge(table, item);
                    item.UserName = item.RowKey;
                }
            }
            return listUsers;
        }

    
    }
}
