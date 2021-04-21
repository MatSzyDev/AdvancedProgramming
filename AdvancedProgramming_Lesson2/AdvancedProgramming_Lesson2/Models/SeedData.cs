using AdvancedProgramming_Lesson2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AdvancedProgramming_Lesson2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());
            // Look for any movies.
            if (context.Movie.Any())
            {
                return; // DB has been seeded
            }
            context.Movie.AddRange(
            new Movie
            {
                Title = "Potwory i spółka",
                ReleaseDate = DateTime.Parse("2001-1-1"),
                Genre = "Comedy/Fantasy",
                Price = 1.99M,
                Rank = 1,
                Lang = "PL"
            },
            new Movie
            {
                Title = "Auta",
                ReleaseDate = DateTime.Parse("2006-1-1"),
                Genre = "Animation",
                Price = 2.49M,
                Rank = 2,
                Lang = "PL"
            },
            new Movie
            {
                Title = "Toy Story",
                ReleaseDate = DateTime.Parse("1995-1-1"),
                Genre = "Comedy/Animation",
                Price = 9.99M,
                Rank = 3,
                Lang = "PL"
            },
            new Movie
            {
                Title = "Kill Bill",
                ReleaseDate = DateTime.Parse("2003-1-1"),
                Genre = "Action",
                Price = 8.29M,
                Rank = 4,
                Lang = "PL"
            }
            );
            context.SongRank.AddRange(
            new SongRank
            {
                Title = "Potwory i spółka",
                ReleaseDate = DateTime.Parse("2001-1-1"),
                Rank = 1
            },
            new SongRank
            {
                Title = "Auta",
                ReleaseDate = DateTime.Parse("2006-1-1"),
                Rank = 2
            },
            new SongRank
            {
                Title = "Toy Story",
                ReleaseDate = DateTime.Parse("1995-1-1"),
                Rank = 3
            },
            new SongRank
            {
                Title = "Kill Bill",
                ReleaseDate = DateTime.Parse("2003-1-1"),
                Rank = 4
            }
            );
            context.SaveChanges();
        }
    }
}
