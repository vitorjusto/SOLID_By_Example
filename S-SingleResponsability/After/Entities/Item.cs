using System;

namespace SOLID_By_Example.S_SingleResponsability.After.Entities
{
    public class Item
    {
        public Item(int amount, decimal price)
        {
            if (amount <= 0)
                throw new Exception("The item quantity cannot be equal to or less than zero.");

            if (price <= 0)
                throw new Exception("The item price cannot be less than or equal to zero.");

            Amount = amount;
            Price = price;
        }

        public int Amount { get; private set; }
        public decimal Price { get; private set; }
    }
}