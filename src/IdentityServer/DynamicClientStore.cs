// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using IdentityServer.Helpers;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer
{
    internal class DynamicClientStore : IClientStore
    {
        private static Client _configClient;

        public static Client Client { get; private set; }

        public static void ResetConfigClient()
        {
            Client = _configClient.CloneJson();
        }

        public static void SetConfigClient(Client client)
        {
            _configClient = client;
            ResetConfigClient();
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return await Task.FromResult(Client);
        }
    }
}