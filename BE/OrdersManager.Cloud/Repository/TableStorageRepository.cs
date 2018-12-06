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

        public Task<List<T>> ExecuteSimpleQuery(string databaseName, string collectionName, Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Tuple<List<T>, int> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null, bool orderAsc = false, params Expression<Func<T, object>>[] orderByExpressions)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
