using System;
using System.Collections.Generic;

namespace Mailinator_Check
{
    class Program
    {
        static int Main(string[] args)
        {
#if DEBUG
            //Some test addresses. Most are mailinator
            List<string> invalidDomains = new List<string>(new string[]
	        {
                "binkmail.com",
                "bobmail.info",
                "chammy.info",
                "devnullmail.com",
                "letthemeatspam.com",
                "mailinater.com",
                "mailinator.net",
                "mailinator2.com",
                "notmailinator.com",
                "reallymymail.com",
                "reconmail.com",
                "safetymail.info",
                "sendspamhere.com",
                "sogetthis.com",
                "spambooger.com",
                "spamherelots.com",
                "spamhereplease.com",
                "spamthisplease.com",
                "streetwisemail.com",
                "suremail.info",
                "thisisnotmyrealemail.com",
                "tradermail.info",
                "veryrealemail.com",
                "zippymail.info"
            });
            foreach (string s in invalidDomains)
            {
                Console.WriteLine("[{0}]\t{1}", Mailinator.IsMailinator(s) ? "YES" : "NO", s);
            }
            Console.WriteLine("#END");
            Console.ReadKey(true);
            return 0;
#else
            int mailinatorCount = 0;
            if (args.Length > 0)
            {
                foreach (string s in args)
                {
                    if (Mailinator.IsMailinator(s))
                    {
                        ++mailinatorCount;
                    }
                }
            }
            else
            {
                Console.WriteLine(@"Mailinator-Check <domain> [domain [...]]

Checks the given domains, if they are mailinator mailboxes or not.
Returns the number of mailinator inboxes found. 0 if none.");
            }
            return mailinatorCount;
#endif
        }
    }
}
