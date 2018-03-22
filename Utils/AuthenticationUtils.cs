﻿using System;
using System.Linq;
using ManyWho.Flow.SDK.Security;

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

namespace ManyWho.Flow.SDK.Utils
{
    public class AuthenticationUtils
    {
        public static IAuthenticatedWho Deserialize(String token)
        {
            IAuthenticatedWho authenticatedWho = null;
            String[] parameters = null;
            String directoryIdParameter = null;
            String directoryNameParameter = null;
            String roleIdParameter = null;
            String roleNameParameter = null;
            String primaryGroupIdParameter = null;
            String primaryGroupNameParameter = null;
            String identityProviderParameter = null;
            String manywhoTenantIdParameter = null;
            String manywhoUserIdParameter = null;
            String manywhoTokenParameter = null;
            String tenantNameParameter = null;
            String tokenParameter = null;
            String userIdParameter = null;
            String usernameParameter = null;
            String emailParameter = null;
            String firstNameParameter = null;
            String lastNameParameter = null;

            // Start by splitting the string so we have a complete key/value pairing
            parameters = token.Split('&');

            // Grab the parameters for each of the properties from the array
            manywhoTenantIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TENANT_ID, StringComparison.OrdinalIgnoreCase));
            manywhoUserIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_USER_ID, StringComparison.OrdinalIgnoreCase));
            manywhoTokenParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TOKEN, StringComparison.OrdinalIgnoreCase));
            directoryIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_ID, StringComparison.OrdinalIgnoreCase));
            directoryNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_NAME, StringComparison.OrdinalIgnoreCase));
            roleIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_ROLE_ID, StringComparison.OrdinalIgnoreCase));
            roleNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_ROLE_NAME, StringComparison.OrdinalIgnoreCase));
            primaryGroupIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_PRIMARY_GROUP_ID, StringComparison.OrdinalIgnoreCase));
            primaryGroupNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_PRIMARY_GROUP_NAME, StringComparison.OrdinalIgnoreCase));
            emailParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_EMAIL, StringComparison.OrdinalIgnoreCase));
            identityProviderParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_IDENTITY_PROVIDER, StringComparison.OrdinalIgnoreCase));
            tenantNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TENANT_NAME, StringComparison.OrdinalIgnoreCase));
            tokenParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TOKEN, StringComparison.OrdinalIgnoreCase));
            userIdParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_USER_ID, StringComparison.OrdinalIgnoreCase));
            usernameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_USERNAME, StringComparison.OrdinalIgnoreCase));
            firstNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_FIRST_NAME, StringComparison.OrdinalIgnoreCase));
            lastNameParameter = parameters.Single(value => value.StartsWith(ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_LAST_NAME, StringComparison.OrdinalIgnoreCase));

            // Check to make sure we have all of the parameters
            Validation.Instance.IsNotNullOrWhiteSpace(manywhoTenantIdParameter, "ManyWhoTenantId", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TENANT_ID)
                                .IsNotNullOrWhiteSpace(manywhoUserIdParameter, "ManyWhoUserId", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_USER_ID)
                                .IsNotNullOrWhiteSpace(manywhoTokenParameter, "ManyWhoToken", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TOKEN)
                                .IsNotNullOrWhiteSpace(directoryIdParameter, "DirectoryId", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_ID)
                                .IsNotNullOrWhiteSpace(directoryNameParameter, "DirectoryName", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_NAME)
                                .IsNotNullOrWhiteSpace(emailParameter, "Email", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_EMAIL)
                                .IsNotNullOrWhiteSpace(identityProviderParameter, "IdentityProvider", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_IDENTITY_PROVIDER)
                                .IsNotNullOrWhiteSpace(tenantNameParameter, "TenantName", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TENANT_NAME)
                                .IsNotNullOrWhiteSpace(tokenParameter, "Token", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TOKEN)
                                .IsNotNullOrWhiteSpace(userIdParameter, "UserId", "Missing parameter: " + ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_USER_ID);           

            // Create our new authenticated who object
            authenticatedWho = new AuthenticatedWho();
            authenticatedWho.ManyWhoTenantId = Guid.Parse(manywhoTenantIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1]);
            authenticatedWho.ManyWhoUserId = Guid.Parse(manywhoUserIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1]);
            authenticatedWho.ManyWhoToken = manywhoTokenParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.DirectoryId = directoryIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.DirectoryName = directoryNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.Email = emailParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.IdentityProvider = identityProviderParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.TenantName = tenantNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.Token = tokenParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            authenticatedWho.UserId = userIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];

            if (firstNameParameter != null &&
                firstNameParameter.Trim().Length > 0)
            {
                authenticatedWho.FirstName = firstNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (lastNameParameter != null &&
                lastNameParameter.Trim().Length > 0)
            {
                authenticatedWho.LastName = lastNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (usernameParameter != null &&
                usernameParameter.Trim().Length > 0)
            {
                authenticatedWho.Username = usernameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (!string.IsNullOrWhiteSpace(roleIdParameter))
            {
                authenticatedWho.RoleId = roleIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (!string.IsNullOrWhiteSpace(roleNameParameter))
            {
                authenticatedWho.RoleName = roleNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (!string.IsNullOrWhiteSpace(primaryGroupIdParameter))
            {
                authenticatedWho.PrimaryGroupId = primaryGroupIdParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            if (!string.IsNullOrWhiteSpace(primaryGroupNameParameter))
            {
                authenticatedWho.PrimaryGroupName = primaryGroupNameParameter.Split(ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER)[1];
            }

            // Finally, validate the object is OK
            ValidateAuthenticatedWho(authenticatedWho);

            return authenticatedWho;
        }

        public static String Serialize(IAuthenticatedWho authenticatedWho)
        {
            String token = "";

            // Validate the object to make sure we have everything that's needed
            ValidateAuthenticatedWho(authenticatedWho);

            // Construct the token string
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TENANT_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.ManyWhoTenantId.ToString() + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_USER_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.ManyWhoUserId.ToString() + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_MANYWHO_TOKEN + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.ManyWhoToken + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.DirectoryId + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_DIRECTORY_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.DirectoryName + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_ROLE_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.RoleId + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_ROLE_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.RoleName + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_PRIMARY_GROUP_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.PrimaryGroupId + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_PRIMARY_GROUP_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.PrimaryGroupName + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_EMAIL + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.Email + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_IDENTITY_PROVIDER + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.IdentityProvider + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TENANT_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.TenantName + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_TOKEN + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.Token + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_USERNAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.Username + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_USER_ID + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.UserId + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_FIRST_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.FirstName + "&";
            token += ManyWhoConstants.AUTHENTICATED_WHO_TOKEN_LAST_NAME + ManyWhoConstants.SERIALIZATION_DELIMITER_DELIMITER + authenticatedWho.LastName;

            // Encode the token ready for http
            return token;
        }

        public static IAuthenticatedWho CreatePublicUser(String tenantId)
        {
            IAuthenticatedWho authenticatedWho = null;
            Guid tenantGuid = Guid.Empty;

            if (String.IsNullOrWhiteSpace(tenantId) == true)
            {
                throw new ArgumentNullException("TenantId", "The TenantId cannot be null or blank.");
            }

            if (Guid.TryParse(tenantId, out tenantGuid) == false)
            {
                throw new ArgumentNullException("TenantId", "The TenantId is not a valid identifier.");
            }

            authenticatedWho = new AuthenticatedWho();
            authenticatedWho.DirectoryId = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_DIRECTORY_ID;
            authenticatedWho.DirectoryName = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_DIRECTORY_NAME;
            authenticatedWho.RoleId = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_ROLE_ID;
            authenticatedWho.RoleName = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_ROLE_NAME;
            authenticatedWho.PrimaryGroupId = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_PRIMARY_GROUP_ID;
            authenticatedWho.PrimaryGroupName = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_PRIMARY_GROUP_NAME;
            authenticatedWho.IdentityProvider = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_IDENTITY_PROVIDER;
            authenticatedWho.Email = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_EMAIL;
            authenticatedWho.TenantName = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_TENANT_NAME;
            authenticatedWho.Token = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_TOKEN;
            authenticatedWho.UserId = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_USER_ID;
            authenticatedWho.ManyWhoTenantId = tenantGuid;
            authenticatedWho.ManyWhoToken = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_TOKEN;
            authenticatedWho.ManyWhoUserId = ManyWhoConstants.AUTHENTICATED_USER_PUBLIC_MANYWHO_USER_ID;

            return authenticatedWho;
        }

        public static void ValidateAuthenticatedWho(IAuthenticatedWho authenticatedWho)
        {
            Validation.Instance.AuthenticatedWho(authenticatedWho)
                                .IsNotEmpty(authenticatedWho.ManyWhoUserId, "ManyWhoUserId")
                                .IsNotNullOrWhiteSpace(authenticatedWho.ManyWhoToken, "ManyWhoToken")
                                .IsNotNullOrWhiteSpace(authenticatedWho.Email, "Email")
                                .IsNotNullOrWhiteSpace(authenticatedWho.ManyWhoToken, "ManyWhoToken")
                                .IsNotNullOrWhiteSpace(authenticatedWho.Token, "Token")
                                .IsNotNullOrWhiteSpace(authenticatedWho.UserId, "UserId");
        }
    }
}
