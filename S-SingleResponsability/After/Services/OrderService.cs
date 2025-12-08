using SOLID_By_Example.S_SingleResponsability.After.Entities;
using SOLID_By_Example.S_SingleResponsability.After.Interfaces;
using System;

namespace SOLID_By_Example.S_SingleResponsability.After.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IInvoiceService _invoiceService;

        public OrderService(IOrderRepository orderRepository, IEmailSender emailSender, ILogger logger, IInvoiceService invoiceService)
        {
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            _logger = logger;
            _invoiceService = invoiceService;
        }

        public void Process(Order order)
        {
            try
            {
                _logger.Log($"Processing order {order.IdOrder} | Customer: {order.Customer}");
                _orderRepository.Save(order);
                _logger.Log("Order registration successful.");

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error("Process not completed correctly.");
                return;
            }

            try
            {
                _invoiceService.GenerateInvoice(order);
                _logger.Log("Invoice successfully issued.");

                _emailSender.Send("customer@exemple.com",
                                  $"Order confirmation {order.IdOrder}",
                                  $"Your order has been processed. Total {order.Total}");

                _logger.Log("Email sent successfully.");

            }
            catch (Exception ex)
            {
                _logger.Warn(ex.Message);
                _logger.Warn("The submission will need to be redone.");
                return;
            }

            _logger.Log("Process completed.");
        }
    }
}