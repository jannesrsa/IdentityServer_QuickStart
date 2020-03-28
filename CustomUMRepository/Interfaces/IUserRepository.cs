using System.Threading;
using System.Threading.Tasks;
using CustomUserManagerRepository.Dto;

namespace CustomUserManagerRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> FindUserAsync(string username, CancellationToken cancellationToken);

        Task<bool> FindCheckUserLoginAsync(string username, string password);
    }
}