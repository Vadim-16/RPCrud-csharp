using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPCrud.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPCrud.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RPCrudContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RPCrudContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }

                context.User.AddRange(
                    new User
                    {
                        
                        Password = "123",
                        Gender = "m",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1989-2-12"),
                        CheckedAgreement = true
                    },

                    new User
                    {
                        Login = "Sam",
                        Password = "456",
                        Gender = "f",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1999-1-19"),
                        CheckedAgreement = true
                    },

                    new User
                    {
                        Login = "Molly",
                        Password = "789",
                        Gender = "f",
                        Country = "en",
                        DateOfBirth = DateTime.Parse("2000-11-17"),
                        CheckedAgreement = true
                    },

                    new User
                    {
                        Login = "Jorge",
                        Password = "111",
                        Gender = "m",
                        Country = "mx",
                        DateOfBirth = DateTime.Parse("1977-6-15"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Sarah",
                        Password = "222",
                        Gender = "f",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1995-7-27"),
                        CheckedAgreement = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
