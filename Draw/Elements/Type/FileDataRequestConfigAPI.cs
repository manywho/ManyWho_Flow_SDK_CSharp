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

namespace ManyWho.Flow.SDK.Draw.Elements.Type
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class FileDataRequestConfigAPI
    {
        /// <summary>
        /// The Id for the service element associated with this file data.  The service element will give us the connection settings
        /// and allow us to validate various bits of info.
        /// </summary>
        [DataMember]
        public string serviceElementId
        {
            get;
            set;
        }

        /// <summary>
        /// The reference for the folder - this can be an identifier or a path - whatever the service implementation needs.
        /// </summary>
        [DataMember]
        public string resourcePath
        {
            get;
            set;
        }

        /// <summary>
        /// The reference for the file - this can be an identifier or a file name - whatever the service implementation needs.
        /// </summary>
        [DataMember]
        public string resourceFile
        {
            get;
            set;
        }
    }
}
