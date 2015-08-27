using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Configuration;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
    private string _SMTPHost = WebConfigurationManager.AppSettings["_SMTPHost"];

    private string emailSender;
    private string emailRecipient;
    private string emailRecipientBCC;
    private string subject;
    private string content;

    public string EmailSender
    {
        get { return emailSender; }
        set { emailSender = value; }
    }
    public string EmailRecipient
    {
        get { return emailRecipient; }
        set { emailRecipient = value; }
    }
    public string EmailRecipientBCC
    {
        get { return emailRecipientBCC; }
        set { emailRecipientBCC = value; }
    }
    public string Subject
    {
        get { return subject; }
        set { subject = value; }
    }
    public string Content
    {
        get { return content; }
        set { content = value; }
    }

    public SendEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SendEmail(string emailSender, string emailRecipient, string subject, string content, string emailRecipientBCC)
    {
        this.emailSender = emailSender;
        this.emailRecipient = emailRecipient;
        this.subject = subject;
        this.content = content;
        this.emailRecipientBCC = emailRecipientBCC;
    }

    public bool Send()
    {
        try
        {
            MailMessage mailobj = new MailMessage(this.emailSender, this.emailRecipient, this.subject, this.content);
            mailobj.IsBodyHtml = true;
            if (emailRecipientBCC != "" && emailRecipientBCC != null)
            {
                if (emailRecipientBCC.Contains(";"))
                {
                    string[] delimeterString = new string[] { ";" };
                    string[] listBcc = emailRecipientBCC.Split(delimeterString, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in listBcc)
                    {
                        mailobj.Bcc.Add(item.ToString());
                    }
                }
                else
                {
                    mailobj.Bcc.Add(emailRecipientBCC);
                }
            }

            SmtpClient smtpobj = new SmtpClient(_SMTPHost);
            smtpobj.Send(mailobj);
            smtpobj = null;

            return true;
        }
        catch
        {
            return false;
        }
    }

    // ใช้สำหรับเช็ค E-mail ว่าถูกต้องหรือไม่
    public bool CheckIsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
    }
}
