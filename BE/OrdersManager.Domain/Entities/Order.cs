using Microsoft.Azure.CosmosDB.Table;
using Newtonsoft.Json;
using OrdersManager.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Domain.Entities
{
   public class Order : TableEntity, IEntity
    {
        public Customer OrderCustomer { get; set; }

        public OrderDetail [] OrdersDetails { get; set; }

         public int CustomerId { get; set; }

         public DateTime  Created_At { get; set; }

         public string ShipAdress { get; set; }

         public string ShipCity { get; set; }

         public string ShipPostalCode { get; set; }

         public string ShipCountry { get; set; }

         public decimal TotalAmount { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public PropertyInfo[] GetPropertiesOrder()
        {

            return this.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                           BindingFlags.Public |
                                           BindingFlags.Instance);

        }
        
    }
}
