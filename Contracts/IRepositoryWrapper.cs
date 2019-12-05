namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ITaskRepository Task { get; }
        void Save();
    }
}
