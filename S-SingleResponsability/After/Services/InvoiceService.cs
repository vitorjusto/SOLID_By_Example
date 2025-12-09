using SOLID_By_Example.S_SingleResponsability.After.Entities;
using SOLID_By_Example.S_SingleResponsability.After.Interfaces;
using System;
using System.IO;

namespace SOLID_By_Example.S_SingleResponsability.After.Services
{
    public class InvoiceService : IInvoiceService
    {
        public void GenerateInvoice(Order order)
        {
            try
            {
                Directory.CreateDirectory("Invoices");

                var invoicePath = Path.Combine("Invoices", $"InvoicePath_{order.IdOrder}.txt");

                File.WriteAllText(invoicePath, $"Invoice - Order {order.IdOrder}\nCustomer: {order.Customer}\nTotal: {order.Total}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to generate invoice.{Environment.NewLine} Message: {ex.Message}");
            }
        }
    }
}