﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ManyWho.Flow.SDK.Translate.Elements.Map;
using ManyWho.Flow.SDK.Translate.Elements.Type;
using ManyWho.Flow.SDK.Translate.Elements.UI;
using ManyWho.Flow.SDK.Translate.Elements.Value;

namespace ManyWho.Flow.SDK.Translate.Flow
{
    [DataContract(Namespace = "http://www.manywho.com/api")]
    public class FlowImageTranslationResponseAPI
    {
        [DataMember]
        public string editingToken
        {
            get;
            set;
        }

        [DataMember]
        public string id
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
        public string developerSummary
        {
            get;
            set;
        }

        [DataMember]
        public string startMapElementId
        {
            get;
            set;
        }

        [DataMember]
        public List<NavigationElementTranslationResponseAPI> navigationElements
        {
            get;
            set;
        }

        [DataMember]
        public List<MapElementTranslationResponseAPI> mapElements
        {
            get;
            set;
        }

        [DataMember]
        public List<PageElementTranslationResponseAPI> pageElements
        {
            get;
            set;
        }

        [DataMember]
        public List<TypeElementTranslationResponseAPI> typeElements
        {
            get;
            set;
        }

        [DataMember]
        public List<ValueElementTranslationResponseAPI> valueElements
        {
            get;
            set;
        }
    }
}
