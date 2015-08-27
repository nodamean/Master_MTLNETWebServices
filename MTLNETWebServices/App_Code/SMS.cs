using System;
using System.Collections.Generic;
using System.Web;

using System.Web.Configuration;
using NIGateway;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{

    public SMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Declare common variables to use internal
    protected string Username = WebConfigurationManager.AppSettings["SMSUsername"];
    protected string Password = WebConfigurationManager.AppSettings["SMSPassword"];
    protected string ServerAddress = WebConfigurationManager.AppSettings["SMSServerAddress"];
    protected string ServerPort = WebConfigurationManager.AppSettings["SMSServerPort"];
    protected string ProxyAddress = WebConfigurationManager.AppSettings["SMSProxyAddress"];
    protected string ProxyPort = WebConfigurationManager.AppSettings["SMSProxyPort"];

    //Public Method SendSMS
    public string SendSMS(string mobileno, string message)
    {
        if (CheckConnection())
        {
            try
            {
                SmsClient.ServerAddress = ServerAddress;
                SmsClient.Port = Convert.ToInt32(ServerPort);
                SmsClient.HttpProxy.ProxyMode = HttpProxyMode.AutoDetect;
                //SmsClient.HttpProxy.Host = ProxyAddress;
                //SmsClient.HttpProxy.Port = Convert.ToInt32(ProxyPort);

                SmsClient sms = new SmsClient(Username, Password);
                try
                {
                    SendMessageResult result = sms.SendMessage(mobileno, message);
                    return result.TaskId + "|" + result.MessageId;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        else
        {
            return "Error";
        }
    }

    //Private Method CheckConnection
    private bool CheckConnection()
    {
        try
        {
            SmsClient.ServerAddress = ServerAddress;
            SmsClient.Port = Convert.ToInt32(ServerPort);
            SmsClient.HttpProxy.ProxyMode = HttpProxyMode.AutoDetect;
            //SmsClient.HttpProxy.Host = ProxyAddress;
            //SmsClient.HttpProxy.Port = Convert.ToInt32(ProxyPort);

            SmsClient sms = new SmsClient(Username, Password);

            if (sms.TestConnection())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
