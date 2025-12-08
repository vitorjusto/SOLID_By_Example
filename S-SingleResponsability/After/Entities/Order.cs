using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID_By_Example.S_SingleResponsability.After.Entities
{
    public class Order 
    {
        public Order(int id, string customer, List<Item> items)
        {
            if (string.IsNullOrWhiteSpace(customer))
                throw new Exception("Invalid customer.");

            if (items == null || items.Count == 0)
                throw new Exception("Order without items.");

            IdOrder = id;
            Customer = customer;
            Items = items;
            Total = CalculateTotal();
        }

        public int IdOrder { get; private set; }
        public string Customer { get; private set; }
        public List<Item> Items { get; private set; }
        public decimal Total { get; private set; }

        private decimal CalculateTotal()
        {
            return Items.Sum(item => item.Price * item.Amount);
        }
    }
}
