using ChatRoom.Cloud.Helper;
using ChatRoom.Cloud.Interfaces;
using ChatRoom.Cloud.Repository;
using ChatRoom.Domain.Entities;
using ChatRoom.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatRoom.Services
{
    public class ChatRoomService : IChatRoomService
    {

        private IQueueStorageRepository  repositoryOrders = new QueueStorageRepository ();

        public ChatRoomService()
        {
        }

        /// <inheritdoc />
        public ChatRoomService(
            IQueueStorageRepository repositoryOrders
            )
        {
            this.repositoryOrders = repositoryOrders;

        }

        public Task AddMessageAsync(string message)
        {
          return repositoryOrders.AddMessageAsync(message);
            
        }

        public Task DeleteMessageAsync(string message)
        {
            return repositoryOrders.DeleteMessageAsync(message);
        }

    }
}
