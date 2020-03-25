// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentityServer
{
    internal class DynamicClientStore : IClientStore
    {
        public static Client Client { get; set; }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return await Task.FromResult(Client);
        }
    }
}