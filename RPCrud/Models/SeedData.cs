using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPCrud.Data;
using System;
using System.Linq;

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
                        Login = "John",
                        Password = "AA!@#5gf",
                        Gender = "male",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1989-2-12"),
                        CheckedAgreement = true
                    },

                    new User
                    {
                        Login = "Sam",
                        Password = "GH&&*4hgjyz%",
                        Gender = "female",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1999-1-19"),
                        CheckedAgreement = false
                    },

                    new User
                    {
                        Login = "Molly",
                        Password = "5(&%$KGfyR",
                        Gender = "female",
                        Country = "us",
                        DateOfBirth = DateTime.Parse("2000-11-17"),
                        CheckedAgreement = true
                    },

                    new User
                    {
                        Login = "Jorge",
                        Password = "P4$$ghYD&",
                        Gender = "male",
                        Country = "mx",
                        DateOfBirth = DateTime.Parse("1977-6-15"),
                        CheckedAgreement = false
                    },
                    new User
                    {
                        Login = "Sarah",
                        Password = "73HF3$*F%Ja",
                        Gender = "female",
                        Country = "ca",
                        DateOfBirth = DateTime.Parse("1987-2-23"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Antony",
                        Password = "*$K3%GF!fdb",
                        Gender = "male",
                        Country = "us",
                        DateOfBirth = DateTime.Parse("1999-10-11"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Michaela",
                        Password = "P4$$ghYD&",
                        Gender = "female",
                        Country = "mx",
                        DateOfBirth = DateTime.Parse("1974-8-2"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Clayton",
                        Password = "67HF4gfdR%%#&",
                        Gender = "male",
                        Country = "us",
                        DateOfBirth = DateTime.Parse("1975-6-26"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Andre",
                        Password = "H*^$kdfGG4%g/",
                        Gender = "male",
                        Country = "mx",
                        DateOfBirth = DateTime.Parse("2001-3-5"),
                        CheckedAgreement = true
                    },
                    new User
                    {
                        Login = "Margaret",
                        Password = "QWN44F#@%jf",
                        Gender = "female",
                        Country = "mx",
                        DateOfBirth = DateTime.Parse("2001-3-5"),
                        CheckedAgreement = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
