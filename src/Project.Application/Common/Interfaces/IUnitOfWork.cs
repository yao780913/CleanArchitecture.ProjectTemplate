namespace Project.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}