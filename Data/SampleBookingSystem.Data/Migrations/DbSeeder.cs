using System;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SampleBookingSystem.Common;
using SampleBookingSystem.Data.Models;
using System.Drawing;
using System.Threading;

namespace SampleBookingSystem.Data.Migrations
{
    public static class DbSeeder
    {
        const string AdministratorEmail = "admin@admin.com";
        const string AdministratorUserName = "admin";
        const string UserEmailSuffix = "@user.com";
        const string UserNamePrefix = "user";
        const string TestPassword = "123456";
        const string UserCreationError = "Error creating user: {0}";

        public static void SeedUsers(SampleBookingSystemDbContext context)
        {
            if (!context.Users.Any())
            {
                // Create roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var adminRole = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(adminRole);

                var clientUserRole = new IdentityRole { Name = GlobalConstants.ClientRoleName };
                roleManager.Create(clientUserRole);

                // Create user store and user manager
                var userStore = new UserStore<Models.User>(context);
                var userManager = new UserManager<User>(userStore);

                // Create admin
                var admin = new User { Email = AdministratorEmail, UserName = AdministratorUserName, EmailConfirmed = true };
                IdentityResult adminCreation = userManager.Create(admin, TestPassword);
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);

                if (!adminCreation.Succeeded)
                {
                    throw new Exception(string.Format(UserCreationError, adminCreation.Errors.First()));
                }

                // Create normal users
                for (int i = 0; i < 20; i++)
                {
                    var testUser = new User { Email = string.Format("{0}{1}{2}", UserNamePrefix, i, UserEmailSuffix), UserName = string.Format("{0}{1}", UserNamePrefix, i), EmailConfirmed = true };
                    IdentityResult testUserCreation = userManager.Create(testUser, TestPassword);

                    if (!testUserCreation.Succeeded)
                    {
                        throw new Exception(string.Format(UserCreationError, testUserCreation.Errors.First()));
                    }

                    userManager.AddToRole(testUser.Id, GlobalConstants.ClientRoleName);

                    context.SaveChanges();
                }
            }
        }

        public static void SeedRooms(SampleBookingSystemDbContext context)
        {
            if (!context.Rooms.Any())
            {
                for (int i = 1; i < 16; i++)
                {
                    var random = new Random();
                    Thread.Sleep(365);
                    var imageNumber = random.Next(1, 6);

                    Image img = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "SeedImages\\hl_" + imageNumber + ".jpg");
                    byte[] arr;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        arr = ms.ToArray();
                    }

                    var newRoom = new Room
                    {
                        RoomNumber = i,
                        Capacity = i % 3 == 0 ? 3 : 2,
                        Floor = i < 10 ? 1 : 2,
                        Description = "The best room in our hotel",
                        Picture = arr
                    };

                    context.Rooms.Add(newRoom);
                }
            }

            context.SaveChanges();
        }

        public static void SeedReservations(SampleBookingSystemDbContext context)
        {
            if (!context.Reservations.Any())
            {
                int reservationId = 1;
                int roomId = 1;
                int dateCounter = 1;

                while (context.Reservations.Count() <= 17)
                {
                    var userStore = new UserStore<User>(context);
                    var userManager = new UserManager<User>(userStore);


                    IdentityRole clientRole = context.Roles
                        .SingleOrDefault(m => m.Name == GlobalConstants.ClientRoleName);

                    var usersInClientRole = context.Users
                        .Where(m => m.Roles.All(r => r.RoleId == clientRole.Id));

                    var user = context.Users
                        .Where(u => usersInClientRole.Contains(u))
                        .OrderBy(u => Guid.NewGuid())
                        .First();

                    var userHasReservation = context.Reservations.Where(r => r.UserId == user.Id).Any();

                    if (!userHasReservation)
                    {
                        var reservation = new Reservation()
                        {
                            UserId = user.Id,
                            RoomId = roomId,
                            CheckinDate = new DateTime(2016, 12, dateCounter),
                            ChechoutDate = new DateTime(2016, 12, dateCounter + 2)
                        };

                        context.Reservations.Add(reservation);
                        context.SaveChanges();

                        reservationId++;

                        if (roomId < 15)
                        {
                            roomId++;
                        }
                        else
                        {
                            roomId = 1;
                        }

                        dateCounter++;
                    }
                }
            }
        }
    }
}
