namespace Code.Library
{
    public static class EmailHelper
    {
        //public static void SendgridEmail(string destination, string subject, string body)
        //{
        //    var myMessage = new SendGridMessage();
        //    myMessage.AddTo(destination);
        //    myMessage.From = new System.Net.Mail.MailAddress("Email-From", "Email-Name");
        //    myMessage.Subject = subject;
        //    myMessage.Text = body;
        //    myMessage.Html = body;

        //    //Template
        //    //switch (subject)
        //    //{
        //    //    case "\":
        //    //        myMessage.EnableTemplateEngine(WebConfigurationManager.AppSettings["templateId"]);
        //    //        break;    
        //    //}

        //    var credentials = new NetworkCredential("Sendgrid Username","Sendgrid Password");

        //    // Create a Web transport for sending email. 
        //    var transportWeb = new Web(credentials);

        //    // Send the email. 
        //    transportWeb.DeliverAsync(myMessage);
        //}
    }
}
