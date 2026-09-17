using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGED.Infrastructure.Persistence.Configurations;

public sealed class EtapaAnoEscolarConfiguration : IEntityTypeConfiguration<EtapaAnoEscolar>
{
    public void Configure(EntityTypeBuilder<EtapaAnoEscolar> builder)
    {
        builder.ToTable("EtapaAnoEscolar");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.DeletedAt);
    }
}