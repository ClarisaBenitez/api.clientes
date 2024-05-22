using Microsoft.EntityFrameworkCore;
using Repository.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<ClienteModel> ClientesEF { get; set; }
    public DbSet<FacturaModel> FacturasEF { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración 
        modelBuilder.Entity<ClienteModel>(entity =>
        {
            entity.ToTable("cliente"); 
            entity.HasKey(e => e.id);
            entity.Property(e => e.id);
            entity.Property(e => e.id_banco);
            entity.Property(e => e.nombre).HasMaxLength(255).IsRequired(); 
            entity.Property(e => e.apellido).HasMaxLength(255).IsRequired();
            entity.Property(e => e.documento).HasMaxLength(255).IsRequired();
            entity.Property(e => e.direccion).HasMaxLength(255);
            entity.Property(e => e.celular).HasMaxLength(255).IsRequired();
            entity.Property(e => e.mail).HasMaxLength(255);
            entity.Property(e => e.estado).HasMaxLength(255);
        });

        // 
        modelBuilder.Entity<FacturaModel>(entity =>
        {
            entity.ToTable("factura"); 
            entity.HasKey(e => e.id);
            entity.Property(e => e.id);
            entity.Property(e => e.id_cliente).HasMaxLength(12).IsRequired();
            entity.Property(e => e.nro_factura).HasMaxLength(12).IsRequired();
            entity.Property(e => e.fecha_hora).IsRequired();
            entity.Property(e => e.total).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.total_iva5).HasColumnType("decimal(18,2)");
            entity.Property(e => e.total_iva10).HasColumnType("decimal(18,2)");
            entity.Property(e => e.total_iva).HasColumnType("decimal(18,2)");
            entity.Property(e => e.total_letras).HasMaxLength(255);
            entity.Property(e => e.sucursal).HasMaxLength(255);
        });
    }
}
