using ChatRoom.Domain;
using ChatRoom.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Services.Interfaces
{

    /// <summary>
    /// Order service interface
    /// </summary>
    public interface IChatRoomService
    {

        // /// <summary>
        // ///  Gets the list of Orders.
        // /// </summary>
        // /// <param name="criteria"></param>
        // /// <returns></returns>
        //Task<PagedListDTO<OrderDTO>> GetOrders(BaseCriteriaDTO criteria);


        // /// <summary>
        // /// Edit orders
        // /// </summary>
        // /// <param name="orderEntity"></param>
        // Task EditOrderDetail(OrderDTO orderEntity);
        Task DeleteMessageAsync(string message);

        Task AddMessageAsync(string message);

    }
}
