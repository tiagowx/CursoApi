using curso.api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infra.Data.Mappings
{
  public class CursoMapping : IEntityTypeConfiguration<Curso>
  {
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("TB_CURSO");
        builder.HasKey(P => P.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p=> p.Name);
        builder.Property(p=> p.Description);
        builder.HasOne(p=> p.User)
        .WithMany().HasForeignKey(fk => fk.UserId); 
    }
  }
}