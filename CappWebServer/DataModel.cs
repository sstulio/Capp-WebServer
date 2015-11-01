namespace CappWebServer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Prova> Prova { get; set; }
        public virtual DbSet<Resposta> Resposta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
                .Property(e => e.CodigoAluno)
                .IsUnicode(false);

            modelBuilder.Entity<Aluno>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Professor>()
                .HasMany(e => e.Prova)
                .WithRequired(e => e.Professor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prova>()
                .Property(e => e.CodigoProva)
                .IsUnicode(false);

            modelBuilder.Entity<Prova>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.CodigoProva)
                .IsUnicode(false);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.CodigoAluno)
                .IsUnicode(false);

            modelBuilder.Entity<Resposta>()
                .Property(e => e.Alternativa)
                .IsUnicode(false);
        }
    }
}
