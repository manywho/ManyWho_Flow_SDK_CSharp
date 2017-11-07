﻿using System;
using System.Runtime.Serialization;
using ManyWho.Flow.SDK.Security;

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

namespace ManyWho.Flow.SDK.Draw.Flow
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class FlowResponseAPI : FlowRequestAPI
    {
        /// <summary>
        /// The date the Flow was first created.
        /// </summary>
        [DataMember]
        public DateTime dateCreated
        {
            get;
            set;
        }

        /// <summary>
        /// The date the last modificiation was made to the Flow.
        /// </summary>
        [DataMember]
        public DateTime dateModified
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who created the Flow.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoCreated
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who last modified the Flow.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoModified
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who owns this Flow.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoOwner
        {
            get;
            set;
        }

        [DataMember]
        public String alertEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if this Flow version is the active version. In the case of run operations, this will always be true.
        /// </summary>
        [DataMember]
        public Boolean isActive
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if this Flow version is the default version. In the case of run operations, this will always be true.
        /// </summary>
        [DataMember]
        public Boolean isDefault
        {
            get;
            set;
        }

        [DataMember]
        public string comment
        {
            get;
            set;
        }
    }
}