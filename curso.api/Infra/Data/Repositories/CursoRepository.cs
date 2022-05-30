using System.Collections.Generic;
using System.Linq;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infra.Data.Repositories
{
  public class CursoRepository : ICursoRepository
  {
      private readonly CursoDbContext _context;
    public CursoRepository(CursoDbContext context)
    {
        _context = context;
    }

    public void Add(Curso curso)
    {
        _context.Curso.Add(curso);
    }

    public void Commit()
    {
         _context.SaveChanges();
    }

    public IList<Curso> GetByUserId(int userId)
    {
        return _context.Curso.Include(i => i.User).Where(w => w.UserId == userId).ToList();
    }
  }
}