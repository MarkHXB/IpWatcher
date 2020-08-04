using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.Mail;

namespace IpWatcher
{
    class Program
    {
        static void Main(string[] args)
        { 
            mailSend();
        }
        private static string testLocation()
        {  
            string ip = new WebClient().DownloadString("xxxxxx");
            return ip;       
        }
        private static void mailSend()
        {
            string ip = testLocation();

            var toAddress = new MailAddress("xxxxx@gmail.com", "xxxxx");
            var fromAddress = toAddress;

            const string fromPassword = "xxxxxx";
            const string subject = "IpWatcher";
            string body = $"Your ip has changed!\nTo this:\n {ip}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
