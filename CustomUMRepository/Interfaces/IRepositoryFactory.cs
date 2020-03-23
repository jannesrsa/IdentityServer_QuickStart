namespace CustomUserManagerRepository.Interfaces
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository();
    }
}