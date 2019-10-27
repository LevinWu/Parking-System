
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FIT5032.Models
{
    public class BulkEmail
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void SendB(List<EmailAddress> Emaillist, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@easyparking.com", "Easy Parking Customer Service");

            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Emaillist, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendBA(List<EmailAddress> Emaillist, String subject, String contents, string path, string filename)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@easyparking.com", "Easy Parking Customer Service");

            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, Emaillist, subject, plainTextContent, htmlContent);
            var bytes = File.ReadAllBytes(path);
            msg.AddAttachment(filename, Convert.ToBase64String(bytes));
            var response = client.SendEmailAsync(msg);
        }

    }
}