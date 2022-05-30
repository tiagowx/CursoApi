using System.Collections.Generic;
using curso.api.Business.Entities;

namespace curso.api.Business.Repositories
{
    public interface ICursoRepository
    {
        public void Add(Curso curso);

        public void Commit();
        public IList <Curso> GetByUserId(int userId);
    }
}