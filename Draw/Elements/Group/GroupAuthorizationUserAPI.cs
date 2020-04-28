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

namespace ManyWho.Flow.SDK.Draw.Elements.Group
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class GroupAuthorizationUserAPI
    {
        /// <summary>
        /// The unique identifier to match the correct user. This is an external unique identifier based on the directory implementation of the Service.
        /// </summary>
        [DataMember]
        public string authenticationId
        {
            get;
            set;
        }

        /// <summary>
        /// The unique attribute to match the correct user attribute. This enumeration is based on the directory implementation of the Service.
        /// </summary>
        [DataMember]
        public string attribute
        {
            get;
            set;
        }

        [DataMember]
        public bool runningUser
        {
            get;
            set;
        }

        [DataMember]
        public string attributeLabel
        {
            get;
            set;
        }

        [DataMember]
        public string friendlyName
        {
            get;
            set;
        }

        [DataMember]
        public string developerSummary
        {
            get;
            set;
        }

        [DataMember]
        public string serviceElementId
        {
            get;
            set;
        }
    }
}
