﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using ManyWho.Flow.SDK.Run.Elements.Map;
using ManyWho.Flow.SDK.Run.Elements.UI;
using ManyWho.Flow.SDK.Run.State;
using ManyWho.Flow.SDK.Translate;


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

namespace ManyWho.Flow.SDK.Run
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class EngineInvokeResponseAPI
    {
        [DataMember]
        public CultureAPI culture
        {
            get;
            set;
        }

        /// <summary>
        /// The unique identifier for the initialized flow state. The state identifier is needed throughout the execution of the flow as it is the pointer that points the engine to the correct running instance of a flow.
        /// </summary>
        [DataMember]
        public string stateId
        {
            get;
            set;
        }

        [DataMember]
        public string parentStateId
        {
            get;
            set;
        }

        /// <summary>
        /// The unique identifier for the token response from the state. The state token identifier changes with every cycle of request/response. The token is needed as it tells the engine how in-sync your request is with the current service side state.
        /// </summary>
        [DataMember]
        public string stateToken
        {
            get;
            set;
        }

        /// <summary>
        /// The email address that should be used to notify of any Flow or player related faults. This email address is typically the email of the author of the Flow.
        /// </summary>
        [DataMember]
        public string alertEmail
        {
            get;
            set;
        }

        /// <summary>
        /// The message that should be shown to the user if the engine returns an Invoke Type of 'WAIT'.
        /// </summary>
        [DataMember]
        public string waitMessage
        {
            get;
            set;
        }

        /// <summary>
        /// The message that should be shown to the user if the engine returns an Invoke Type of 'STATUS'. The engine will automatically handle UI permissions - so this user will not recieve Page metadata.
        /// </summary>
        [DataMember]
        public string notAuthorizedMessage
        {
            get;
            set;
        }

        /// <summary>
        /// The unique identifier for the map element the user is currently executing against.
        /// </summary>
        [DataMember]
        public string currentMapElementId
        {
            get;
            set;
        }

        /// <summary>
        /// The unique identifier for the collaboration stream the user is currently using to collaborate with colleagues. If the Flow is not leveraging collaboration streams, this property will be null or blank.
        /// </summary>
        [DataMember]
        public string currentStreamId
        {
            get;
            set;
        }

        /// <summary>
        /// The way the engine responded to your initial invoke request.
        /// </summary>
        [DataMember]
        public string invokeType
        {
            get;
            set;
        }
        
        /// <summary>
        /// Key value pairs you wish to annotate to the flow. Annotations take the form of {"mykey":"myvalue"}. Any annotations added to the state will be persisted for the duration of the flow. Annotations are passed to the executing player and also through to plugin services. Annotations can be changed at any time through the execution of the flow.
        /// </summary>
        [DataMember]
        public Dictionary<string, string> annotations
        {
            get;
            set;
        }

        /// <summary>
        /// The list of map element invoke responses generated by the executed element or elements in the flow. Currently the array contains only the one object, which is the object that is the current element being executed in the flow.
        /// </summary>
        [DataMember]
        public List<MapElementInvokeResponseAPI> mapElementInvokeResponses
        {
            get;
            set;
        } = new List<MapElementInvokeResponseAPI>();

        [DataMember]
        public VoteResponseAPI voteResponse
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the execution history of the flow.
        /// </summary>
        [DataMember]
        public StateLogAPI stateLog
        {
            get;
            set;
        }

        /// <summary>
        /// The list of engine values that are currently in a pre-commit state. This is only populated when the engine is being run in "DEBUG" or "DEBUG_STEPTHROUGH" mode. Values are only committed to the state after the element has completed execution successfully (usually as a result of the user invoking the engine "FORWARD" and the page validating correctly).
        /// </summary>
        [DataMember]
        public List<EngineValueAPI> preCommitStateValues
        {
            get;
            set;
        }

        /// <summary>
        /// The list of engine values that are currently committed in the state. This is only populated when the engine is being run in "DEBUG" or "DEBUG_STEPTHROUGH" mode.
        /// </summary>
        [DataMember]
        public List<EngineValueAPI> stateValues
        {
            get;
            set;
        }

        /// <summary>
        /// The list of engine values for all values in the flow that are set as OUTPUT or INPUT/OUTPUT. This property is only populated when the engine responds with an invokeType of "DONE".
        /// </summary>
        [DataMember]
        public List<EngineValueAPI> outputs
        {
            get;
            set;
        }

        /// <summary>
        /// This property is populated is only in debug mode. It contains the list of current stack frames.
        /// </summary>
        [DataMember]
        public List<EngineFrameAPI> frames
        {
            get; 
            set;
        }    

        /// <summary>
        /// The values mimic standard REST codes, but as a String. A "200" indicates the user is authenticated to execute the flow. A "401" indicates the user needs to login based on the authorization context information provided in the response.
        /// </summary>
        [DataMember]
        public string statusCode
        {
            get;
            set;
        }

        /// <summary>
        /// The Url that should be used to run a new instance of this flow with this player. For cross-platform situations, the run Url should redirect to the appropriate player for the calling device.
        /// </summary>
        [DataMember]
        public string runFlowUri
        {
            get;
            set;
        }

        /// <summary>
        /// The Url that should be used to join this instance of this this flow with this player. For cross-platform situations, the join Url should redirect to the appropriate player for the calling device.
        /// </summary>
        [DataMember]
        public string joinFlowUri
        {
            get;
            set;
        }

        /// <summary>
        /// The authorization context for the initialization. This object will tell you how to login to the correct system to begin executing the flow.
        /// </summary>
        [DataMember]
        public EngineAuthorizationContextAPI authorizationContext
        {
            get;
            set;
        }

        [DataMember]
        public List<NavigationElementReferenceAPI> navigationElementReferences
        {
            get;
            set;
        } = new List<NavigationElementReferenceAPI>();

        /// <summary>
        /// Indicates whether both the UI and api for historical navigation has been enabled/disabled.
        /// </summary>
        [DataMember]
        public bool isHistoricalNavigationEnabled
        {
            get;
            set;
        }
    }
}
