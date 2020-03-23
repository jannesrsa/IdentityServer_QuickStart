using AutoMapper;
using CustomUserManagerRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomUserManagerRepository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IConnectionProvider _connectionProvider;

        public RepositoryFactory(IConnectionProvider connectionProvider, IMapper mapper)
        {
            _connectionProvider = connectionProvider;
            _mapper = mapper;
        }

        protected DataContext DataContext
        {
            get
            {
                if (_dataContext != null)
                {
                    return _dataContext;
                }

                var envContext = new DataContext(_connectionProvider.GetSqlConnectionString());

                return envContext;
            }
        }

        public IUserRepository GetUserRepository() => new UserRepository(DataContext, _mapper);
    }
}
