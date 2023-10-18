using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class PharmacyContextSeed
{
    private readonly UserManager<User> _userManager;
    public PharmacyContextSeed(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public static async Task SeedAsync(VeterinaryDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>
                {
                    new Rol { Name = "Administrator" },
                    new Rol { Name = "Veterinarian" },
                    new Rol { Name = "WithoutRol" }
                };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();
                using var readerUsers = new StreamReader("../Persistence/Data/Csvs/users.csv");
                {
                    using (var csv = new CsvReader(readerUsers, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var usersList = csv.GetRecords<User>();
                        List<User> users = new List<User>();
                        foreach (var user in usersList)
                        {
                            var hashedPassword = passwordHasher.HashPassword(null, user.Password);
                            var newUser = new User
                            {
                                Id = user.Id,
                                UserName = user.UserName,
                                IdenNumber = user.IdenNumber,
                                Email = user.Email,
                                Password = hashedPassword
                            };
                            users.Add(newUser);
                        }
                        context.Users.AddRange(users);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if (!context.UserRoles.Any())
            {
                using (var readerUserRols = new StreamReader("../Persistence/Data/Csvs/userRols.csv"))
                {
                    using (var csv = new CsvReader(readerUserRols, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var userRolList = csv.GetRecords<UserRol>();
                        List<UserRol> userRols = new List<UserRol>();
                        foreach (var userRol in userRolList)
                        {
                            userRols.Add(new UserRol
                            {
                                Id = userRol.Id,
                                UserId = userRol.UserId,
                                RolId = userRol.RolId
                            });
                        }
                        context.UserRoles.AddRange(userRols);
                        await context.SaveChangesAsync();
                    }
                }
            }

           
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<VeterinaryDbContext>();
            logger.LogError(ex.Message);
        }
    }
}
