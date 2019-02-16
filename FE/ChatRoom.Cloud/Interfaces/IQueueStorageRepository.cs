 using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom.Cloud.Interfaces
{
    public interface IQueueStorageRepository        
    {
        Task AddMessageAsync(string message);

        Task DeleteMessageAsync(string message);
        

    }
}
