using System;
using System.Collections.Generic;
using System.Net;
using ManyWho.Flow.SDK.Converters;
using Newtonsoft.Json;

namespace ManyWho.Flow.SDK.Admin
{
    public class RuntimeNode
    {
        /// <summary>
        /// A unique identifier for the node
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// An identifying name for the node
        /// </summary>
        public string DeveloperName
        {
            get;
            set;
        }

        /// <summary>
        /// A list of the IP addresses currently associated with the node
        /// </summary>
        [JsonProperty(ItemConverterType = typeof(IPAddressConverter))]
        public IEnumerable<IPAddress> IpAddresses
        {
            get;
            set;
        }

        /// <summary>
        /// Timestamp of the last report received from this node
        /// </summary>
        public DateTimeOffset ReportedAt
        {
            get;
            set;
        }

        /// <summary>
        /// The current status of the node, determined by the last time a report was received from it
        /// </summary>
        public RuntimeStatus Status
        {
            get;
            set;
        }
    }
}