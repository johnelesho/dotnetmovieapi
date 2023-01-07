using System.Diagnostics.CodeAnalysis;
using DotNetMoviesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetMoviesApi;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext([NotNull]DbContextOptions<ApplicationDbContext> dbContextOptions) : base
    (dbContextOptions)
    {
    
    }
    public DbSet<Genre> Genres { get; private set; }
}