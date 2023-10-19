using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class VeterinaryContextSeed
    {
        private readonly UserManager<User> _userManager;

        public VeterinaryContextSeed(UserManager<User> userManager)
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
                        new() { Name = "Administrator" },
                        new() { Name = "Veterinarian" },
                        new() { Name = "WithoutRol" }
                    };
                    context.Roles.AddRange(roles);
                    await context.SaveChangesAsync();
                }
                if (!context.Users.Any())
                {
                    var passwordHasher = new PasswordHasher<User>();
                    using var readerUsers = new StreamReader("../Persistence/Data/Csvs/users.csv");
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
                if (!context.TypeMovements.Any())
                {
                    var typeMovements = new List<TypeMovement>
                    {
                        new() { Name = "Purchase" },
                        new() { Name = "Sale" }
                    };
                    context.TypeMovements.AddRange(typeMovements);
                    await context.SaveChangesAsync();
                }
                if (!context.Veterinarians.Any())
                {
                    using (var readerVeterinarian = new StreamReader("../Persistence/Data/Csvs/veterinarian.csv"))
                    {
                        using (var csv = new CsvReader(readerVeterinarian, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var veterinarianList = csv.GetRecords<Veterinarian>();
                            List<Veterinarian> veterinarians = new List<Veterinarian>();
                            foreach (var veterinarian in veterinarianList)
                            {
                                veterinarians.Add(new Veterinarian
                                {
                                    Name = veterinarian.Name,
                                    PhoneNumber = veterinarian.PhoneNumber,
                                    Specialty = veterinarian.Specialty,
                                    IdUser = veterinarian.IdUser

                                });
                            }
                            context.Veterinarians.AddRange(veterinarians);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Laboratories.Any())
                {
                    var laboratories = new List<Laboratory>
                    {
                        new() { Name = "Genfar", Address = "calle 1", PhoneNumber = "3214565678" },
                        new() { Name = "Procaps", Address = "calle 2", PhoneNumber = "3214565679" },
                        new() { Name = "MK", Address = "calle 3", PhoneNumber = "3214565610" },
                        new() { Name = "Bayer", Address = "calle 4", PhoneNumber = "3214565611" }
                    };
                    context.Laboratories.AddRange(laboratories);
                    await context.SaveChangesAsync();
                }
                if (!context.Owners.Any())
                {
                    using (var readerOwner = new StreamReader("../Persistence/Data/Csvs/owner.csv"))
                    {
                        using (var csv = new CsvReader(readerOwner, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var ownerList = csv.GetRecords<Owner>();
                            List<Owner> owners = new List<Owner>();
                            foreach (var owner in ownerList)
                            {
                                owners.Add(new Owner
                                {
                                    Name = owner.Name,
                                    Email = owner.Email,
                                    PhoneNumber = owner.PhoneNumber
                                });
                            }
                            context.Owners.AddRange(owners);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Providers.Any())
                {
                    using (var readerProvider = new StreamReader("../Persistence/Data/Csvs/provider.csv"))
                    {
                        using (var csv = new CsvReader(readerProvider, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var providerList = csv.GetRecords<Provider>();
                            List<Provider> providers = new List<Provider>();
                            foreach (var provider in providerList)
                            {
                                providers.Add(new Provider
                                {
                                    Name = provider.Name,
                                    Address = provider.Address,
                                    PhoneNumber = provider.PhoneNumber,
                                });
                            }
                            context.Providers.AddRange(providers);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Species.Any())
                {
                    using (var readerSpecie = new StreamReader("../Persistence/Data/Csvs/specie.csv"))
                    {
                        using (var csv = new CsvReader(readerSpecie, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var specieList = csv.GetRecords<Specie>();
                            List<Specie> species = new List<Specie>();
                            foreach (var specie in specieList)
                            {
                                species.Add(new Specie
                                {
                                    Name = specie.Name,
                                });
                            }
                            context.Species.AddRange(species);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Races.Any())
                {
                    using (var readerRace = new StreamReader("../Persistence/Data/Csvs/race.csv"))
                    {
                        using (var csv = new CsvReader(readerRace, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var raceList = csv.GetRecords<Race>();
                            List<Race> races = new List<Race>();
                            foreach (var race in raceList)
                            {
                                races.Add(new Race
                                {
                                    Name = race.Name,
                                    IdSpecie = race.IdSpecie
                                });
                            }
                            context.Races.AddRange(races);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Pets.Any())
                {
                    using (var readerPet = new StreamReader("../Persistence/Data/Csvs/pet.csv"))
                    {
                        using (var csv = new CsvReader(readerPet, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var petList = csv.GetRecords<Pet>();
                            List<Pet> pets = new List<Pet>();
                            foreach (var pet in petList)
                            {
                                pets.Add(new Pet
                                {
                                    Name = pet.Name,
                                    BirthDate = pet.BirthDate,
                                    IdOwner = pet.IdOwner,
                                    IdSpecie = pet.IdSpecie,
                                    IdRace = pet.IdRace
                                });
                            }
                            context.Pets.AddRange(pets);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Medicines.Any())
                {
                    using (var readerMedicine = new StreamReader("../Persistence/Data/Csvs/medicine.csv"))
                    {
                        using (var csv = new CsvReader(readerMedicine, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var medicineList = csv.GetRecords<Medicine>();
                            List<Medicine> medicines = new List<Medicine>();
                            foreach (var medicine in medicineList)
                            {
                                medicines.Add(new Medicine
                                {
                                    Name = medicine.Name,
                                    QuantityDisp = medicine.QuantityDisp,
                                    Price = medicine.Price,
                                    IdLaboratory = medicine.IdLaboratory,
                                    IdProvider = medicine.IdProvider
                                });
                            }
                            context.Medicines.AddRange(medicines);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.MedicineMovements.Any())
                {
                    using (var readerMedicineMovement = new StreamReader("../Persistence/Data/Csvs/medicineMovement.csv"))
                    {
                        using (var csv = new CsvReader(readerMedicineMovement, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var medicineMovementList = csv.GetRecords<MedicineMovement>();
                            List<MedicineMovement> medicineMovements = new List<MedicineMovement>();
                            foreach (var medicineMovement in medicineMovementList)
                            {
                                medicineMovements.Add(new MedicineMovement
                                {

                                    Quantity = medicineMovement.Quantity,
                                    DateMovement = medicineMovement.DateMovement,
                                    PriceUnit = medicineMovement.PriceUnit,
                                    IdMedicine = medicineMovement.IdMedicine,
                                    IdTypeMovement = medicineMovement.IdTypeMovement
                                });
                            }
                            context.MedicineMovements.AddRange(medicineMovements);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Appointments.Any())
                {
                    using (var readerAppointment = new StreamReader("../Persistence/Data/Csvs/appointment.csv"))
                    {
                        using (var csv = new CsvReader(readerAppointment, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var appointmentList = csv.GetRecords<Appointment>();
                            List<Appointment> appointments = new List<Appointment>();
                            foreach (var appointment in appointmentList)
                            {
                                appointments.Add(new Appointment
                                {
                                    DateAppointment = appointment.DateAppointment,
                                    Hour = appointment.Hour,
                                    Cause = appointment.Cause,
                                    IdPet = appointment.IdPet,
                                    IdVeterinarian = appointment.IdVeterinarian
                                });
                            }
                            context.Appointments.AddRange(appointments);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Treatments.Any())
                {
                    using (var readerTreatment = new StreamReader("../Persistence/Data/Csvs/treatment.csv"))
                    {
                        using (var csv = new CsvReader(readerTreatment, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HeaderValidated = null, // Esto deshabilita la validación de encabezados
                            MissingFieldFound = null
                        }))
                        {
                            var treatmentList = csv.GetRecords<Treatment>();
                            List<Treatment> treatments = new List<Treatment>();
                            foreach (var treatment in treatmentList)
                            {
                                treatments.Add(new Treatment
                                {
                                    Dosage = treatment.Dosage,
                                    DateAdministration = treatment.DateAdministration,
                                    Observations = treatment.Observations,
                                    IdAppointment = treatment.IdAppointment,
                                    IdMedicine = treatment.IdMedicine
                                });
                            }
                            context.Treatments.AddRange(treatments);
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
}
