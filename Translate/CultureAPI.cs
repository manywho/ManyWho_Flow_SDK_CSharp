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

namespace ManyWho.Flow.SDK.Translate
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class CultureAPI
    {
        /// <summary>
        /// The id for the culture.
        /// </summary>
        [DataMember]
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// The developer name for the culture.
        /// </summary>
        [DataMember]
        public string developerName
        {
            get;
            set;
        }

        /// <summary>
        /// The developer summary for the culture.
        /// </summary>
        [DataMember]
        public string developerSummary
        {
            get;
            set;
        }

        /// <summary>
        /// The brand for the culture.
        /// </summary>
        [DataMember]
        public string brand
        {
            get;
            set;
        }

        /// <summary>
        /// The language for the culture.
        /// </summary>
        [DataMember]
        public string language
        {
            get;
            set;
        }

        /// <summary>
        /// The country for the culture.
        /// </summary>
        [DataMember]
        public string country
        {
            get;
            set;
        }

        /// <summary>
        /// The variant for the culture.
        /// </summary>
        [DataMember]
        public string variant
        {
            get;
            set;
        }
    }
}