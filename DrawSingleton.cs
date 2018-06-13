﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ManyWho.Flow.SDK.Describe;
using ManyWho.Flow.SDK.Draw.Elements.Config;
using ManyWho.Flow.SDK.Draw.Elements.Group;
using ManyWho.Flow.SDK.Draw.Elements.Map;
using ManyWho.Flow.SDK.Draw.Elements.Type;
using ManyWho.Flow.SDK.Draw.Elements.UI;
using ManyWho.Flow.SDK.Draw.Elements.Value;
using ManyWho.Flow.SDK.Draw.Flow;
using ManyWho.Flow.SDK.Errors;
using ManyWho.Flow.SDK.Security;
using ManyWho.Flow.SDK.Utils;
using Newtonsoft.Json;
using Polly;

namespace ManyWho.Flow.SDK
{
    public class DrawSingleton
    {
        public const string MANYWHO_BASE_URL = "https://flow.manywho.com";

        public const string MANYWHO_DRAW_URI_PART_TYPE_ELEMENT = "/api/draw/1/element/type";
        public const string MANYWHO_DRAW_URI_PART_VALUE_ELEMENT = "/api/draw/1/element/value";
        public const string MANYWHO_DRAW_URI_PART_SERVICE_ELEMENT = "/api/draw/1/element/service";
        public const string MANYWHO_DRAW_URI_PART_PAGE_ELEMENT = "/api/draw/1/element/page";
        public const string MANYWHO_DRAW_URI_PART_MAP_ELEMENT = "/api/draw/1/flow/{0}/{1}/element/map";
        public const string MANYWHO_DRAW_URI_PART_GROUP_ELEMENT = "/api/draw/1/flow/{0}/{1}/element/group";
        public const string MANYWHO_DRAW_URI_PART_FLOW = "/api/draw/1/flow";
        public const string MANYWHO_DRAW_URI_PART_LOGIN = "/api/draw/1/authentication";
        public const string MANYWHO_DRAW_URI_PART_ADD_ELEMENT_TO_FLOW = "/api/draw/1/element/flow/{0}/{1}/{2}";
        public const string MANYWHO_DRAW_URI_PART_LOAD_FLOWS = "/api/draw/1/flow?filter=";
        public const string MANYWHO_DRAW_URI_PART_SNAPSHOT_FLOW = "/api/draw/1/flow/snap/{0}";
        public const string MANYWHO_DRAW_URI_PART_ACTIVATION = "/api/draw/1/flow/activation/{0}/{1}/{2}/{3}";

        public const string MANYWHO_DRAW_URI_PART_ADMIN_PLUGIN_AUTHENTICATION = "/plugins/manywho/api/admin/1/authentication";

        private static DrawSingleton drawSingleton;

        private DrawSingleton()
        {

        }

        public static DrawSingleton GetInstance()
        {
            if (drawSingleton == null)
            {
                drawSingleton = new DrawSingleton();
            }

            return drawSingleton;
        }

        /// <summary>
        /// This method allows you to login as an author of flows.
        /// </summary>
        public IAuthenticatedWho Login(string tenantId, string manywhoBaseUrl, AuthenticationCredentialsAPI authenticationCredentials)
        {
            string authorizationToken = null;
            IAuthenticatedWho authenticatedWho = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(null, tenantId, null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(authenticationCredentials));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the describe request
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_LOGIN;

                    // Send the describe request over to the remote service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the describe response object from the response message
                        authorizationToken = httpResponseMessage.Content.ReadAsStringAsync().Result;

                        // Trim the quotes from the JSON response token
                        authorizationToken = authorizationToken.Substring(1, authorizationToken.Length - 2);

                        // Deserialize the token back to an authenticated who object
                        authenticatedWho = AuthenticationUtils.Deserialize(Uri.UnescapeDataString(authorizationToken));
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return authenticatedWho;
        }

        /// <summary>
        /// This method should be used to get descriptions of supported plugins.
        /// </summary>
        public async Task<DescribeServiceResponseAPI> DescribeAsync(IAuthenticatedWho authenticatedWho, DescribeServiceRequestAPI describeServiceRequest)
        {
            using (var httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null, HttpUtils.SYSTEM_TIMEOUT_SECONDS))
            {
                // Use the JSON formatter to create the content of the request body.
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(describeServiceRequest));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Construct the URL for the describe request
                string endpointUrl = describeServiceRequest.uri + "/metadata";

                // Send the describe request over to the remote service
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(endpointUrl, httpContent);

                // Check the status of the response and respond appropriately
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Get the describe response object from the response message
                    return JsonConvert.DeserializeObject<DescribeServiceResponseAPI>(await httpResponseMessage.Content.ReadAsStringAsync());
                }

                // Otherwise, if we have an error then throw an appropriate exception
                throw await ErrorUtils.BuildProblemException(httpResponseMessage);
            }
        }

        /// <summary>
        /// This method should be used to get descriptions of supported plugins.
        /// </summary>
        public async Task<DescribeServiceResponseV2API> DescribeAsync(Guid tenant, string authorizationHeader, string uri)
        {
            using (var httpClient = HttpUtils.CreateHttpClientV2(authorizationHeader, tenant))
            {
                // Send the describe request over to the remote service
                var response = await httpClient.GetAsync(uri + "/metadata");
                if (response.IsSuccessStatusCode)
                {
                    // Get the describe response object from the response message
                    return JsonConvert.DeserializeObject<DescribeServiceResponseV2API>(await response.Content.ReadAsStringAsync());
                }

                // Otherwise, if we have an error then throw an appropriate exception
                throw await ErrorUtils.BuildProblemException(response);
            }
        }

        public async Task<DescribeInstallResponseAPI> DescribeInstallAsync(Guid tenant, string authorizationHeader, string uri, DescribeInstallRequestAPI request)
        {
            using (var httpClient = HttpUtils.CreateHttpClientV2(authorizationHeader, tenant))
            {
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                // Send the describe request over to the remote service
                var response = await httpClient.PostAsync(uri + "/metadata/install", content);
                if (response.IsSuccessStatusCode)
                {
                    // Get the describe response object from the response message
                    return JsonConvert.DeserializeObject<DescribeInstallResponseAPI>(await response.Content.ReadAsStringAsync());
                }

                // Otherwise, if we have an error then throw an appropriate exception
                throw await ErrorUtils.BuildProblemException(response);
            }
        }

        /// <summary>
        /// This method allows you to save flows back to the service.
        /// </summary>
        public FlowResponseAPI SaveFlow(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, FlowRequestAPI flowRequest)
        {
            FlowResponseAPI flowResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(flowRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_FLOW;

                    // Send the flow to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the flow response back from the save
                        flowResponse = JsonConvert.DeserializeObject<FlowResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return flowResponse;
        }

        /// <summary>
        /// This method loads all of the flows in the draw service.
        /// </summary>
        public List<FlowResponseAPI> LoadFlows(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string tenantId, string filter)
        {
            string endpointUrl = null;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            List<FlowResponseAPI> flowResponses = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the draw request
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_LOAD_FLOWS + filter;

                    // Get the flow responses from ManyWho
                    httpResponseMessage = httpClient.GetAsync(endpointUrl).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the flow responses list from the response message
                        flowResponses = JsonConvert.DeserializeObject<List<FlowResponseAPI>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return flowResponses;
        }

        /// <summary>
        /// This method snap shots the latest flow in the modelling environment.
        /// </summary>
        public FlowResponseAPI SnapShotFlow(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string flowId)
        {
            FlowResponseAPI flowResponse = null;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the snapshot
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_SNAPSHOT_FLOW, flowId);

                    // Send the flow to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, null).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the flow response back from the save
                        flowResponse = JsonConvert.DeserializeObject<FlowResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return flowResponse;
        }

        /// <summary>
        /// This method takes a flow snap shot and activates it based on the provided settings.
        /// </summary>
        public FlowResponseAPI SetFlowActivation(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, FlowIdAPI flowId, bool isDefault, bool isActivated)
        {
            FlowResponseAPI flowResponse = null;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the activation
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_ACTIVATION, flowId.id, flowId.versionId, isDefault.ToString().ToLower(), isActivated.ToString().ToLower());

                    // Send the flow to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, null).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the flow response back from the save
                        flowResponse = JsonConvert.DeserializeObject<FlowResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return flowResponse;
        }

        /// <summary>
        /// This method allows you to save shared elements back to the service.
        /// </summary>
        public void AddElementToFlow(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string flowId, string elementType, string elementId)
        {
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_ADD_ELEMENT_TO_FLOW, flowId, elementType, elementId);

                    // We construct an empty content request as we don't actually have any post values
                    httpContent = new StringContent("");

                    // Send the request over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });
        }

        /// <summary>
        /// This method allows you to save value elements back to the service.
        /// </summary>
        public ValueElementResponseAPI SaveValueElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, ValueElementRequestAPI valueElementRequest)
        {
            ValueElementResponseAPI valueElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(valueElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_VALUE_ELEMENT;

                    // Send the value element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the value element response back from the save
                        valueElementResponse = JsonConvert.DeserializeObject<ValueElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return valueElementResponse;
        }

        public TypeElementResponseAPI SaveTypeElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, TypeElementRequestAPI typeElementRequest)
        {
            TypeElementResponseAPI typeElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(typeElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_TYPE_ELEMENT;

                    // Send the type element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the type element response back from the save
                        typeElementResponse = JsonConvert.DeserializeObject<TypeElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return typeElementResponse;
        }

        /// <summary>
        /// This method allows you to save service elements back to the service.
        /// </summary>
        public ServiceElementResponseAPI SaveServiceElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, ServiceElementRequestAPI serviceElementRequest)
        {
            ServiceElementResponseAPI serviceElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(serviceElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_SERVICE_ELEMENT;

                    // Send the service element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the service element response back from the save
                        serviceElementResponse = JsonConvert.DeserializeObject<ServiceElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return serviceElementResponse;
        }

        /// <summary>
        /// This method allows you to save page elements back to the service.
        /// </summary>
        public PageElementResponseAPI SavePageElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, PageElementRequestAPI pageElementRequest)
        {
            PageElementResponseAPI pageElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(pageElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + MANYWHO_DRAW_URI_PART_PAGE_ELEMENT;

                    // Send the page element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the page element response back from the save
                        pageElementResponse = JsonConvert.DeserializeObject<PageElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return pageElementResponse;
        }

        public MapElementResponseAPI LoadMapElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string editingToken, string flowId, string mapElementId)
        {
            MapElementResponseAPI mapElementResponse = null;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_MAP_ELEMENT, flowId, editingToken) + "/" + mapElementId;

                    // Get the map element data to from the service
                    httpResponseMessage = httpClient.GetAsync(endpointUrl).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the map element response back from the load request
                        mapElementResponse = JsonConvert.DeserializeObject<MapElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return mapElementResponse;
        }

        public MapElementResponseAPI SaveMapElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string editingToken, string flowId, MapElementRequestAPI mapElementRequest)
        {
            MapElementResponseAPI mapElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(mapElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_MAP_ELEMENT, flowId, editingToken);

                    // Send the map element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the map element response back from the save
                        mapElementResponse = JsonConvert.DeserializeObject<MapElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return mapElementResponse;
        }

        public GroupElementResponseAPI LoadGroupElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string editingToken, string flowId, string groupElementId)
        {
            GroupElementResponseAPI groupElementResponse = null;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_GROUP_ELEMENT, flowId, editingToken) + "/" + groupElementId;

                    // Get the group element data to from the service
                    httpResponseMessage = httpClient.GetAsync(endpointUrl).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the group element response back from the load request
                        groupElementResponse = JsonConvert.DeserializeObject<GroupElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return groupElementResponse;
        }

        public GroupElementResponseAPI SaveGroupElement(IAuthenticatedWho authenticatedWho, string manywhoBaseUrl, string editingToken, string flowId, GroupElementRequestAPI groupElementRequest)
        {
            GroupElementResponseAPI groupElementResponse = null;
            HttpClient httpClient = null;
            HttpContent httpContent = null;
            HttpResponseMessage httpResponseMessage = null;
            string endpointUrl = null;

            Policy.Handle<ServiceProblemException>().Retry(HttpUtils.MAXIMUM_RETRIES).Execute(() =>
            {
                using (httpClient = HttpUtils.CreateHttpClient(authenticatedWho, authenticatedWho.ManyWhoTenantId.ToString(), null))
                {
                    // Use the JSON formatter to create the content of the request body.
                    httpContent = new StringContent(JsonConvert.SerializeObject(groupElementRequest));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Construct the URL for the save
                    endpointUrl = manywhoBaseUrl + string.Format(MANYWHO_DRAW_URI_PART_GROUP_ELEMENT, flowId, editingToken);

                    // Send the group element data to save over to the service
                    httpResponseMessage = httpClient.PostAsync(endpointUrl, httpContent).Result;

                    // Check the status of the response and respond appropriately
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        // Get the group element response back from the save
                        groupElementResponse = JsonConvert.DeserializeObject<GroupElementResponseAPI>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw new ServiceProblemException(new ServiceProblem(endpointUrl, httpResponseMessage, string.Empty));
                    }
                }
            });

            return groupElementResponse;
        }
    }
}
