using System.Collections.Generic;

namespace trialpro.Services
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddress = new EmailAddress();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
