﻿using System.Runtime.Serialization;
using ManyWho.Flow.SDK.Draw.Elements.Value;

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

namespace ManyWho.Flow.SDK.Draw.Elements.Type
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class ListFilterWhereConfigAPI
    {
        /// <summary>
        /// The unique identifier for the column in the bound component data to filter by.
        /// </summary>
        [DataMember]
        public string columnTypeElementPropertyId
        {
            get;
            set;
        }

        /// <summary>
        /// The criteria to filter the list data by.
        /// </summary>
        [DataMember]
        public string criteriaType
        {
            get;
            set;
        }

        [DataMember]
        public string criteriaTypeFriendly
        {
            get;
            set;
        }

        /// <summary>
        /// The reference to the Value that contains the value to filter the list data by.
        /// </summary>
        [DataMember]
        public ValueElementIdAPI valueElementToReferenceId
        {
            get;
            set;
        }

        [DataMember]
        public string columnTypeElementPropertyDeveloperName
        {
            get;
            set;
        }

        [DataMember]
        public string valueElementToReferenceDeveloperName
        {
            get;
            set;
        }
    }
}
