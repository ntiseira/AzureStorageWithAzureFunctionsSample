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

        private IQueueStorageRepository repositoryQueueStorage = new QueueStorageRepository ();
        private ITableStorageRepository repositoryTableStoragee = new TableStorageRepository();


        public ChatRoomService()
        {
        }

        /// <inheritdoc />
        public ChatRoomService(
            IQueueStorageRepository repositoryQueueStorage
            )
        {
            this.repositoryQueueStorage = repositoryQueueStorage;

        }

        public Task AddMessageAsync(string message)
        {
          return repositoryQueueStorage.AddMessageAsync(message);
            
        }

        public Task DeleteMessageAsync(string message)
        {
            return repositoryQueueStorage.DeleteMessageAsync(message);
        }


        public Task<List<UserEntity>> GetNewsAsync(string alias)
        {
            return repositoryTableStoragee.GetNewsAsync(alias);
        }

    }
}
