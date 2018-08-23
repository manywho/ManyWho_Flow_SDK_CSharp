﻿using System.Runtime.Serialization;
using ManyWho.Flow.SDK.Draw.Elements.Group;
using ManyWho.Flow.SDK.Restrictions;


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
    public class FlowRequestAPI
    {
        /// <summary>
        /// A unique token for this particular editing session
        /// </summary>
        /// <remarks>
        /// An editing token is required so the modelling engine can track multiple user changes.
        /// </remarks>
        [DataMember]
        public string editingToken
        {
            get;
            set;
        }

        /// <summary>
        /// The complete unique identifier for the currently edited version of the flow.
        /// </summary>
        /// <remarks>
        /// This value should not be included when creating new flows.
        /// </remarks>
        [DataMember]
        public FlowIdAPI id
        {
            get;
            set;
        }

        /// <summary>
        /// The developer name for the flow. When referencing flows by name, this is the name you should use in your referencing.
        /// </summary>
        /// <remarks>
        /// This is typically a helpful name to remind builders of the purpose of the flow.
        /// </remarks>
        [DataMember]
        public string developerName
        {
            get;
            set;
        }

        /// <summary>
        /// The developer summary the author provided to give more information about the Flow.
        /// </summary>
        [DataMember]
        public string developerSummary
        {
            get;
            set;
        }

        /// <summary>
        /// The unique identifier for the first element in the flow. This element is always of the `START` map element type.
        /// </summary>
        [DataMember]
        public string startMapElementId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Indicates that the builder of the flow will allow users to jump to any position in the Flow regardless of outcomes and/or navigation.
        /// </summary>
        [DataMember]
        public bool allowJumping
        {
            get;
            set;
        }

        [DataMember]
        public int stateExpirationLength
        {
            get;
            set;
        }

        /// <summary>
        /// The configuration of the authorization context for this flow.
        /// </summary>
        [DataMember]
        public GroupAuthorizationAPI authorization
        {
            get;
            set;
        }
        
        /// <summary>
        /// Specifies geographical restrictions for this flow
        /// </summary>
        [DataMember]
        public FlowRestrictionsAPI restrictions
        {
            get;
            set;
        }
    }
}