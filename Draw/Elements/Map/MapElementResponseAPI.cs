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

namespace ManyWho.Flow.SDK.Draw.Elements.Map
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class MapElementResponseAPI : MapElementRequestAPI
    {
        /// <summary>
        /// The date the Map Element was first created.
        /// </summary>
        [DataMember]
        public DateTimeOffset dateCreated
        {
            get;
            set;
        }

        /// <summary>
        /// The date the last modification was made to the Map Element.
        /// </summary>
        [DataMember]
        public DateTimeOffset dateModified
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who created the Map Element.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoCreated
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who last modified the Map Element.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoModified
        {
            get;
            set;
        }

        /// <summary>
        /// The builder user who owns this Map Element.
        /// </summary>
        [DataMember]
        public BuilderWhoAPI whoOwner
        {
            get;
            set;
        }
    }
}