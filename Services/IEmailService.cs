using System.Net.Mail;

namespace ExamPortal.Services
{
    //Email based Services
    public interface IEmailService
    {
        public void SendMailForPaper(string papercode, string link, string title, string starttime, string endtime, string user);
    }
    public class EmailService : IEmailService
    {
        public void SendMailForPaper(string papercode, string link, string title, string starttime, string endtime, string user)
        {
            using (MailMessage emailMessage = new MailMessage())
            {
                emailMessage.From = new MailAddress("examportalcoremvc@gmail.com", "Exam Portal");
                emailMessage.To.Add(new MailAddress(user, user));
                emailMessage.Subject = "New Paper on Exam Portal";
                emailMessage.Body = $"Title : { title } \t\n Link : {link}\t\n Paper-Code : { papercode } \t\n Start Time : { starttime} \t\n Deadline : { endtime} \t\n"; ;
                emailMessage.Priority = MailPriority.Normal;
                using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    MailClient.EnableSsl = true;
                    MailClient.Credentials = new System.Net.NetworkCredential("examportalcoremvc@gmail.com", "examportal@123");
                    MailClient.Send(emailMessage);
                }
            }

        }
    }
}
