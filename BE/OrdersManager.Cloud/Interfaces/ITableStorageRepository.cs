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

        List<Domain.Entities.Order> ExecuteSimpleQuery(string tableName, string filters);

        Tuple<List<T>, int> GetAllAsync(int pageNumber, int pageSize, string filter, bool orderAsc = false,
            params Expression<Func<T, object>>[] orderByExpressions);

    }
}
