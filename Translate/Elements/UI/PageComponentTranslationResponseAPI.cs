﻿using System;
using System.Collections.Generic;
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

namespace ManyWho.Flow.SDK.Translate.Elements.UI
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class PageComponentTranslationResponseAPI
    {
        [DataMember]
        public string pageContainerDeveloperName
        {
            get;
            set;
        }

        [DataMember]
        public string developerName
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

        [DataMember]
        public string contentContentValueId
        {
            get;
            set;
        }

        [DataMember]
        public string labelContentValueId
        {
            get;
            set;
        }

        [DataMember]
        public string hintValueContentValueId
        {
            get;
            set;
        }

        [DataMember]
        public string helpInfoContentValueId
        {
            get;
            set;
        }

        [DataMember]
        public List<PageComponentColumnTranslationResponseAPI> columns
        {
            get;
            set;
        }
    }
}
