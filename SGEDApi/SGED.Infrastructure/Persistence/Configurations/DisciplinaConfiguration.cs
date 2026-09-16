using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Infrastructure.Persistence.Configurations;

public sealed class DisciplinaConfiguration : IEntityTypeConfiguration<Disciplina>
{
    public void Configure(EntityTypeBuilder<Disciplina> builder)
    {
        builder.ToTable("disciplinas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);
        
        builder.Property(x => x.DeletedAt);
    }
}