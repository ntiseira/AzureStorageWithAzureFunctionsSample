using ChatRoom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Cloud.Interfaces
{
   public interface ITableStorageRepository
   {
        Task<List<UserEntity>> GetNewsAsync();

    }
}
