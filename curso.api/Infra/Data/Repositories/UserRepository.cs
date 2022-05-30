using System.Linq;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;

namespace curso.api.Infra.Data.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly CursoDbContext _context;

        public UserRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.User.Add (user);
        }

        public void Commit()
        { 
            _context.SaveChanges();
        }

        public User GetUser(string email)
        {
            return _context.User.FirstOrDefault(x => x.Email == email);
        }
    }
}
