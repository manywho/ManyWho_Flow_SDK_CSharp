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

namespace ManyWho.Flow.SDK.Run.Elements.Map
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class OutcomeAvailableAPI : IComparable<OutcomeAvailableAPI>
    {
        /// <summary>
        /// The unique identifier for the outcome.
        /// </summary>
        [DataMember]
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// The developer name provided by the builder user to help identify the outcome.
        /// </summary>
        [DataMember]
        public string developerName
        {
            get;
            set;
        }

        /// <summary>
        /// The label provided by the builder user to help Flow users select the correct outcome.
        /// </summary>
        [DataMember]
        public string label
        {
            get;
            set;
        }

        /// <summary>
        /// The order in which this outcome should appear with respect to sibling outcomes.
        /// </summary>
        [DataMember]
        public int order
        {
            get;
            set;
        }

        public int CompareTo(OutcomeAvailableAPI other)
        {
            return order.CompareTo(other.order);
        }
    }
}
