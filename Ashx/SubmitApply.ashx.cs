using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace HuiYaWeb.Ashx
{
    /// <summary>
    /// SubmitApply 的摘要说明
    /// </summary>
    public class SubmitApply : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder sbEmailBody = new StringBuilder();
            //string desc=context.Request["desc"].Replace("\n","</br>");
            sbEmailBody.AppendFormat("<div>姓名：{0}</div>", context.Request["uname"]);
            sbEmailBody.AppendFormat("<div>邮箱：{0}</div>", context.Request["email"]);
            sbEmailBody.AppendFormat("<div>电话：{0}</div>", context.Request["phone"]);
            sbEmailBody.AppendFormat("<div>公司名称：{0}</div>", context.Request["ComName"]);
            sbEmailBody.AppendFormat("<div>需求描述：</br>{0}</div>", context.Request["desc"].Replace("\n","</br>"));

            string emailTitleManager = ConfigurationManager.AppSettings["Email_FromName"].ToString();
            string emailjsManager = ConfigurationManager.AppSettings["Email_js"].ToString();

            SendMail(emailjsManager, "", emailTitleManager, sbEmailBody.ToString());

            context.Response.Write("true");
        } 


        public static void SendMail(string toEmailAddress, string ccEmailAddress, string emailTitle, string emailBody)
        {
            try
            {

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["Email_Host"]);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email_Account"], ConfigurationManager.AppSettings["Email_Pwd"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["Email_Account"], ConfigurationManager.AppSettings["Email_FromName"], Encoding.UTF8);

                //群发对象
                string[] emails = toEmailAddress.Split(';');
                for (int i = 0; i < emails.Length; i++)
                {
                    message.To.Add(emails[i]);
                }

                if (!string.IsNullOrEmpty(ccEmailAddress))
                {
                    string[] ccemails = ccEmailAddress.Split(';');
                    for (int i = 0; i < ccemails.Length; i++)
                    {
                        message.CC.Add(ccemails[i]);
                    }
                }

                //邮件标题
                message.Subject = emailTitle;
                //邮件内容
                message.Body = emailBody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}