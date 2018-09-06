﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpaqueMail;

namespace MantisTests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager appmanager) : base(appmanager) { }

        public String GetLastMail(AccountData account)
        {
            for (int i = 0; i < 20; i++)
            {
                Pop3Client pop3Client = new Pop3Client("localhost", 110, account.Username, account.Password, false);
                pop3Client.Connect();
                pop3Client.Authenticate();

                if (pop3Client.GetMessageCount() > 0)
                {
                    MailMessage msg = pop3Client.GetMessage(1);
                    string body = msg.Body;
                    pop3Client.DeleteMessage(1);

                    return body;
                }
                else
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }

            return null;
        }
    }
}
