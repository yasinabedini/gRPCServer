using gRPCServer.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPCServer.DAL.Context;
public class GrpcContext : DbContext
{
    public GrpcContext(DbContextOptions options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=YasiAbdn\\ABDN;initial catalog=Db_Grpc;integrated security=true;TrustServerCertificate=True");
    }
}
