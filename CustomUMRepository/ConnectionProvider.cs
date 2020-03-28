using System;
using CustomUserManagerRepository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CustomUserManagerRepository
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IConfiguration _config;

        public ConnectionProvider(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public string GetSqlConnectionString()
        {
            return _config.GetConnectionString("SqlConnection");
        }
    }
}