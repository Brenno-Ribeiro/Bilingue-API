using Bilingue.Domain.DomainClassroom;
using Bilingue.Domain.DomainRegistraition;
using Bilingue.Domain.DomainStudent;
using Microsoft.EntityFrameworkCore;

namespace Bilingue.Infra.Context
{
    public class BilingueContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        public BilingueContext(DbContextOptions<BilingueContext> options) : base(options)
        {
      
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity => 
            {
                entity.ToTable("Students");

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Name).HasColumnType("VARCHAR(30)").IsRequired();
                entity.Property(P => P.Email).HasColumnType("VARCHAR(50)").IsRequired();
                entity.Property(P => P.Cpf).HasColumnType("CHAR(11)").IsRequired();
                entity.Property(P => P.Age).HasColumnType("INT").IsRequired();

                entity.HasIndex(P => P.Cpf);
            });



            modelBuilder.Entity<Classroom>(entity => 
            {
                entity.ToTable("Classrooms");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.Number).HasColumnType("VARCHAR(50)").IsRequired();
                entity.Property(p => p.SchoolYear).HasColumnType("INT").IsRequired();

                entity.HasIndex(p => p.Number);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registrations");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.ClassroomId).HasColumnType("UNIQUEIDENTIFIER").IsRequired().ValueGeneratedOnAdd();
                entity.Property(p => p.StudentId).HasColumnType("UNIQUEIDENTIFIER").IsRequired().ValueGeneratedOnAdd();

                entity.HasIndex(p => p.StudentId);
                entity.HasIndex(p => p.ClassroomId);


                entity.HasOne(p => p.Student)
                .WithMany(p => p.Registrations)
                .HasConstraintName("FK_Studenid")
                .HasPrincipalKey(P => P.Id)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Classroom)
                .WithMany(p => p.Registrations)
                .HasConstraintName("FK_ClassroomId")
                .HasPrincipalKey(P => P.Id)
                .HasForeignKey(p => p.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            

        }
    }
}
