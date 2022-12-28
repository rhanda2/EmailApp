using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.ComponentModel;

namespace SimpleEmailLab1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Welcome to the command line email client!");
            var loop = true;
            while (loop)
            {
                Console.WriteLine("\nNew Message");
                Console.Write("To: ");
                string to = Console.ReadLine();

                Console.Write("Subject: ");
                string subject = Console.ReadLine();

                Console.Write("Body: ");
                string body = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(body))
                {
                    Console.WriteLine($"Body Text: \n{body}");
                    Console.Write("Are you sure you want to send? (Y/N): ");
                    var key = Console.ReadKey().Key;
                    if (key != ConsoleKey.Y)
                    {
                        continue;
                    }

                  
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                    
                    /*
                     * Added email address and
                     * Added try catch block to check and catch exceptions
                     */

                    mail.From = new MailAddress("ritvikhanda2@gmail.com");
                    try
                    {
                        mail.To.Add(to);
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine(e);
                    }
                    
                    mail.Subject = subject;
                    mail.Body = body;
                  
                    smtpServer.Port = 25;

                    //Added new email credentials

                     smtpServer.Credentials = new System.Net.NetworkCredential("ritvikhanda2", "bekaar12@");
                    smtpServer.EnableSsl = true;

                    // Added try catch block to check for SmtpException
                    
                    try
                    {
                        smtpServer.Send(mail);
                    }
                    catch (SmtpException e)
                    {
                        Console.WriteLine(e);
                    }
                    
                    Console.WriteLine("done!");
                    // Link event handler
                    smtpServer.SendCompleted += new SendCompletedEventHandler(SmtpServer_SendCompleted);

                    Console.Write("\nWould you like to send another email? (Y/N): ");
                    Console.ReadKey();
                    key = Console.ReadKey().Key;
                    if (key != ConsoleKey.Y)
                    {
                        loop = false;
                    }
                }

            }
            
        }

        static void SmtpServer_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Encourage quick failing
            if (e.Cancelled)
            {
                Console.Error.WriteLine("Message was cancelled");
            }

            if (e.Error != null)
            {
                Console.Error.WriteLine($"An error occurred: {e.Error.Message}");
            }
            Console.WriteLine("Email sent!");

        }
    }
}