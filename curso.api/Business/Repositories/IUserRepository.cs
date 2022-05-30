using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Infra.Data;

namespace curso.api.Business.Repositories
{
  public interface IUserRepository
  {
    public void Add(User user);
    public void Commit();
    public User GetUser( string email);
  }
}