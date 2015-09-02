using System;
using System.Collections.Generic;
using WinAPI.DNS;

namespace Mailinator_Check
{
    /// <summary>
    /// Provides checks against mailinator DNS names
    /// </summary>
    public static class Mailinator
    {
        /// <summary>
        /// Name you consider the main mailinator mailbox
        /// </summary>
        private const string MAILINATOR = "mail.mailinator.com";

        private static List<uint> Mailinator_IPs;

        /// <summary>
        /// Gets the mailinator IPs
        /// </summary>
        static Mailinator()
        {
            Mailinator_IPs = new List<uint>(GetRecords(MAILINATOR));
        }

        /// <summary>
        /// Checks, if the given domain redirects to mailinator
        /// </summary>
        /// <param name="Host">host name</param>
        /// <returns>true, if mailinator inbox</returns>
        public static bool IsMailinator(string Host)
        {
            List<uint> Questionable_IPs = new List<uint>();
            RecordCollection RC = Dns.Lookup(Host, Dns.QueryTypes.DNS_TYPE_MX);
            if (RC == null)
            {
                return false;
            }
            for (int i = 0; i < RC.RecordTypes.Length; i++)
            {
                if (RC.RecordTypes[i] == Dns.QueryTypes.DNS_TYPE_MX)
                {
                    Questionable_IPs.AddRange(GetRecords(RecordCollection.ToString(((Dns.DNS_MX_DATA)RC.FoundRecords[i]).pNameExchange)));
                }
            }

            foreach (uint Mailinator_IP in Mailinator_IPs)
            {
                if (Questionable_IPs.Contains(Mailinator_IP))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets A records from DNS
        /// </summary>
        /// <param name="Hostname">hostname to get A records from</param>
        /// <returns>List of IP addresses (as 4 byte integers)</returns>
        static uint[] GetRecords(string Hostname)
        {
            List<uint> L = new List<uint>();
            RecordCollection RC = Dns.Lookup(Hostname, Dns.QueryTypes.DNS_TYPE_A);
            for (int i = 0; i < RC.RecordTypes.Length; i++)
            {
                if (RC.RecordTypes[i] == Dns.QueryTypes.DNS_TYPE_A)
                {
                    L.Add(((Dns.DNS_A_DATA)RC.FoundRecords[i]).IpAddress);
                }
            }
            return L.ToArray();
        }
    }
}
