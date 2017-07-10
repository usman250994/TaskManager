using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace Task_Manager
{
    public class SMSandEmail
    {
        public bool sendEmail(string uname, string email, string ticketid, string customercode, string customername, string ProjectName, string contactnumber, string Ticketdescription)
        {
            var credential = new NetworkCredential
            {
                UserName = "RescoTask@outlook.com",  // User Email Address example@outlook.com
                Password = "Resco@1234"  // User Password
            };
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));  // replace with valid value 
                message.From = new MailAddress("RescoTask@outlook.com");  // replace with valid value
                message.Subject = "Ticket ID : " + ticketid + "";
                message.Body = "Dear " + uname + " </br> Thank You ! " + uname + " for connecting with us for your query <br/><br/> Ticket ID : " + ticketid + " <br /> Customer Code: " + ProjectName + " <br /> Customer Name: " + customername + " <br />Contact Number:" + "+92" + contactnumber + " <br /> Project Name" + ProjectName + "<br /> Ticket Description: " + Ticketdescription + " <br /><br/> To Check your Ticket Status please Click On \n <a href=http://192.168.1.177:92>Login</a> With Your Customer Code / Ticket ID ";
                //message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                var smtp = new SmtpClient();
                {

                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool sendSms(string Number, string Taskname, string Description, string StartDate, string EndDate, string ProjectName)
        {
            try
            {
                SerialPort serialPort = new SerialPort("COM5", 115200);
                Thread.Sleep(1000);
                serialPort.Open();
                Thread.Sleep(1000);
                serialPort.Write("AT+CMGF=1\r");
                Thread.Sleep(1000);
                serialPort.Write("AT+CMGS=\"" + Number + "\"\r\n");
                Thread.Sleep(1000);
                serialPort.Write("Project: " + ProjectName + ". Task :" + Taskname + ". Desc :" + Description + ". Start :" + StartDate + ". End :" + EndDate + "  " + "\x1A");
                Thread.Sleep(1000);
                serialPort.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please Insert Mobile ");
            }

            return false;
        }

        internal void sendEmail(string p1, string p2, string p3, int p4, int p5)
        {
            throw new NotImplementedException();
        }
    }
}