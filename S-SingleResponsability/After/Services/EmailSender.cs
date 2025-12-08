using SOLID_By_Example.S_SingleResponsability.After.Interfaces;
using System;

namespace SOLID_By_Example.S_SingleResponsability.After.Services
{
    public class EmailSender : IEmailSender
    {
        public void Send(string customer, string subject, string body)
        {
            try
            {
                Console.WriteLine($"[EMAIL] To: {customer} | Subject: {subject} | Body: {body}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email to {customer}.{Environment.NewLine} Message: {ex.Message}");
            }
        }
    }
}