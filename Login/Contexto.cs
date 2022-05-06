using Login.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> opt) : base(opt) { }
        public DbSet<Usuario> USUARIOS { get; set; }
        public DbSet<Filme> FILMES { get; set; }
    }
}
