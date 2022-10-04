using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();

        public void sendData2Queue(string token)
        {
            messageQueue.Path = @".\private$\Token";
            if(!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        public void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
           try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string Subject = "FundooNoteResetLink";
                string Body = token;
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("bhagunemade2902@gmail.com", "nnuffsdchlxhqwca"),//give dummy gmail
                    EnableSsl = true,
                };
                smtp.Send("bhagunemade2902@gmail.com", "bhagunemade2902@gmail.com", Subject, Body);
                messageQueue.BeginReceive();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
