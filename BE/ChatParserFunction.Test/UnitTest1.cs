using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

namespace ChatParserFunction.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            var message = "General:nico:prueba";

            Function1.Run(message, null);        
        }
    }
}
