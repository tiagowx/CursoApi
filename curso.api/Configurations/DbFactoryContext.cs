using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using curso.api.Infra.Data;

namespace curso.api.Configurations
{
    public class DbFactoryContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder
                .UseSqlServer("Server=127.0.0.1,1433;Database=Curso;User Id=SA;Password=mssql1Ipw");

            CursoDbContext context = new CursoDbContext(optionsBuilder.Options);
            return context;
        }
    }
}
