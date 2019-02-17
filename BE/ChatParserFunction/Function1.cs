using ChatParserFunction.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Configuration;

namespace ChatParserFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("chatroom", Connection = "")]string myQueueItem, TraceWriter log )
        {
            string[] splitStrings = myQueueItem.Split(':');

            if (splitStrings.Length > 2)
            {
           

                UserEntity userEntity = new UserEntity(splitStrings[0], splitStrings[1], splitStrings[2]);
  
                //Insert entity
                var result = CommonTableStorage.InsertOrMergeEntityAsync(userEntity).Result;
            }
        }

    }
}
