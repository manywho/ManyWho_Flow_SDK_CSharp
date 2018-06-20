﻿using System;
using System.Runtime.Serialization;

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

namespace ManyWho.Flow.SDK.Run.Elements.UI
{
    /// <summary>
    /// This acts as the column descriptor for rendering the table content stored in the content property (as part of the parent
    /// field definition).
    /// </summary>
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class PageComponentColumnResponseAPI
    {
        /// <summary>
        /// This is the developer name of the type element entry.
        /// </summary>
        [DataMember]
        public string developerName
        {
            get;
            set;
        }

        [DataMember]
        public string typeElementPropertyId
        {
            get;
            set;
        }

        [DataMember]
        public string contentFormat
        {
            get;
            set;
        }

        [DataMember]
        public string contentType
        {
            get;
            set;
        }

        [DataMember]
        public string label
        {
            get;
            set;
        }

        [DataMember]
        public bool isDisplayValue
        {
            get;
            set;
        }

        [DataMember]
        public bool isEditable
        {
            get;
            set;
        }

        [DataMember]
        public int order
        {
            get;
            set;
        }

        [DataMember]
        public string typeElementPropertyToDisplayId
        {
            get;
            set;
        }

        [DataMember]
        public string componentType
        {
            get;
            set;
        }
    }
}
