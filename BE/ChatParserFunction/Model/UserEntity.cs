using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatParserFunction.Model
{
    public class UserEntity : TableEntity
    {
        public UserEntity()
        {
        }


        public UserEntity(string typeChat, string userName, string message)
        {
            PartitionKey = Guid.NewGuid().ToString();
            TypeChat = typeChat;
            RowKey = userName;
            Message = message;
        }

        public string TypeChat { get; set; }
        public string Message { get; set; }


    }
}
