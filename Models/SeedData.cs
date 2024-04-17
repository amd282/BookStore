using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookStore.Data;
using System;
using System.Linq;

namespace BookStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookStoreContext>>()))
            {
                // Look for any books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "War and Peace",
                        Author = "Leo Tolstoy",
                        Genre = "Historical",
                        Price = 7.99M
                    },

                    new Book
                    {
                        Title = "Long Walk To Freedom ",
                        Author = "Nelson Mandela",
                        Genre = "Memoir",
                        Price = 8.99M
                    },

                    new Book
                    {
                        Title = "Can’t Hurt Me",
                        Author = "David Goggins",
                        Genre = "Motivational",
                        Price = 9.99M
                    },

                    new Book
                    {
                        Title = "Midnight in Chernoby",
                        Author = "Adam Higginbotham",
                        Genre = "History",
                        Price = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}