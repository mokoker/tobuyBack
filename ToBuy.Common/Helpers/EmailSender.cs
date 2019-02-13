using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using ToBuy.Common.DTOs;
using ToBuy.Common.Exceptions;

namespace ToBuy.Common.Helpers
{
    public class EmailSender
    {
        public void SendMessage(MailDto dto)
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = dto.SenderAddress,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { dto.ReceiverAddress }
                    },
                    Message = new Message
                    {
                        Subject = new Content(dto.Subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = dto.HtmlMessage
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = dto.Textmessage
                            }
                        }
                    },
                     };
                try
                {
                    Task<SendEmailResponse> response = client.SendEmailAsync(sendRequest);
                   response.Wait();
                }
                catch (Exception ex)
                {
                    throw new EmailSendException(ex.Message);
                }
            }
        }
    }
}
