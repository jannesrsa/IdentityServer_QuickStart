using AutoMapper;
using CustomUserManagerRepository.Dto;
using CustomUserManagerRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomUserManagerRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private IMapper _mapper { get; }

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> FindUserAsync(string userName, CancellationToken cancellationToken)
        {
            var result = await _context.User
                            .Where(x => x.UserName == userName)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(cancellationToken)
                            .ConfigureAwait(false);

            return _mapper.Map<UserDto>(result);
        }

        public async Task<bool> FindCheckUserLoginAsync(string username, string password)
        {
            return await Task.FromResult(_context.GetLoginAsync(username, password));
        }
    }
}
