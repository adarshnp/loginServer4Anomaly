using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using trialpro.Models;

namespace trialpro.Services
{
    public class SentMail
    {
        //private readonly string SenderAddress ;
        //private readonly string RecieverAddress;
        //private readonly string Subject;
        //private readonly string TextBody;
        public void SentOtp(string otp)
        {
            //try
            //{
                MailMessage message = new MailMessage();
                message.To.Add("menonakhilmenon@gmail.com");
                //mailMessage.To.Add(u.mailId);
                message.From = new MailAddress("drakestardrake@gmail.com", "TenshiCorp");
                message.Subject = "NightTales account password reset OTP";
                message.Body = "your account password reset one time password :  ";
                var _smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("drakestardrake@gmail.com", "****"),
                    //Timeout = 20000
                };
                _smtpClient.Send(message);
                Console.WriteLine("E-mail sent!");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Could not send the e-mail - error: " + ex.Message + ex.StackTrace);
            //}
        }
        //public void SentOtp(User u)
        //{
        //    using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast2)
        //    {
        //        var sendRequest
        //        sendRequest = new SendEmailRequest
        //        {
        //            Source = SenderAddress,
        //            Destination = new Destination
        //            {
        //                ToAddresses = new List<string> { RecieverAddress}
        //            },
        //            Message = new Message
        //            {
        //                Subject = new Content(Subject),
        //                Body = new Body
        //                {
        //                    Text = new Content
        //                    {
        //                        Charset = "UTF-8",
        //                        Data = TextBody
        //                    }
        //                }
        //            }
        //        };
        //        try
        //        {
        //            Console.WriteLine("Sending email using Amazon SES...");
        //            var response = client.SendEmail(sendRequest);
        //            Console.WriteLine("The email was sent successfully.");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("The email was not sent.");
        //            Console.WriteLine("Error message: " + ex.Message);
        //        }
        //    }
    }
 }
