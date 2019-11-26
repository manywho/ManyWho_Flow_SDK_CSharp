﻿using System.Runtime.Serialization;

/*!

Copyright 2013 Manywho, Inc.

Licensed under the Manywho License, Version 1.0 (the "License"); you may not use this
file except in compliance with the License.

You may obtain a copy of the License at: http://manywho.com/sharedsource

Unless required by applicable law or agreed to in writing, software distributed under
the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.

*/

namespace ManyWho.Flow.SDK.Security
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class RemoteSiteAPI
    {
        /// <summary>
        /// The name for this remote site restriction, typically a helpful one to remind builders of the purpose
        /// </summary>
        [DataMember]
        public string developerName
        {
            get;
            set;
        }

        /// <summary>
        /// Any additional summary information about this remote site restriction that will help explain the purpose
        /// </summary>
        [DataMember]
        public string developerSummary
        {
            get;
            set;
        }

        /// <summary>
        /// The base URI of the remote site (e.g. https://flow.manywho.com)
        /// </summary>
        [DataMember]
        public string uri
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if the remote site can be accessed without encryption (SSL)
        /// </summary>
        [DataMember]
        public bool disableProtocolSecurity
        {
            get;
            set;
        }
    }
}