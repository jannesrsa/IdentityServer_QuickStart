namespace CustomUserManagerRepository.Interfaces
{
    public interface IConnectionProvider
    {
        string GetSqlConnectionString();
    }
}