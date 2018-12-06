using Microsoft.Azure.CosmosDB.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Cloud.Interfaces
{
    public interface ITableStorageRepository<T> where T : class
    {
        Task<T> GetItemAsync(string id);

        Task<T> CreateOrUpdateItemAsync(CloudTable table, T item);

        Task<List<T>> ExecuteSimpleQuery(string databaseName, string collectionName, Expression<Func<T, bool>> expression);

        Tuple<List<T>, int> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> filter = null, bool orderAsc = false,
            params Expression<Func<T, object>>[] orderByExpressions);

    }
}
