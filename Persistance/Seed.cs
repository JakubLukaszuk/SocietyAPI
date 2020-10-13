using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistance
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Name = "Kuba",
                        Surname = "Bons",
                        UserName = "KubaBons",
                        Email = "kub@test.pl"
                    },
                    new AppUser
                    {
                        Name = "Michal",
                        Surname = "Bons",
                        UserName = "MichalBons",
                        Email = "miachal@test.pl"
                    },
                    new AppUser
                    {
                        Name = "Pienti",
                        Surname = "Bons",
                        UserName = "PientiBons",
                        Email = "pieni@test.pl"
                    }
                };
                foreach (var user in users)
                {
                     await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
            if(!context.Activities.Any())
            {
                List <Activity> activities = new List<Activity>
                {
                    new Activity
                    {
                        Title = "Past Activity 1",
                        Date = DateTime.Now.AddMonths(-2),
                        Description = "Activity 2 months ago",
                        Category = "drinks",
                        City = "London",
                        PlaceOfEvent = "Pub",
                    },
                    new Activity
                    {
                        Title = "Past Activity 2",
                        Date = DateTime.Now.AddMonths(-1),
                        Description = "Activity 1 month ago",
                        Category = "culture",
                        City = "Paris",
                        PlaceOfEvent = "Louvre",
                    },
                    new Activity
                    {
                        Title = "Future Activity 1",
                        Date = DateTime.Now.AddMonths(1),
                        Description = "Activity 1 month in future",
                        Category = "culture",
                        City = "London",
                        PlaceOfEvent = "Natural History Museum",
                    },
                    new Activity
                    {
                        Title = "Future Activity 2",
                        Date = DateTime.Now.AddMonths(2),
                        Description = "Activity 2 months in future",
                        Category = "music",
                        City = "London",
                        PlaceOfEvent = "O2 Arena",
                    },
                    new Activity
                    {
                        Title = "Future Activity 3",
                        Date = DateTime.Now.AddMonths(3),
                        Description = "Activity 3 months in future",
                        Category = "drinks",
                        City = "London",
                        PlaceOfEvent = "Another pub",
                    },
                    new Activity
                    {
                        Title = "Future Activity 4",
                        Date = DateTime.Now.AddMonths(4),
                        Description = "Activity 4 months in future",
                        Category = "drinks",
                        City = "London",
                        PlaceOfEvent = "Yet another pub",
                    },
                    new Activity
                    {
                        Title = "Future Activity 5",
                        Date = DateTime.Now.AddMonths(5),
                        Description = "Activity 5 months in future",
                        Category = "drinks",
                        City = "London",
                        PlaceOfEvent = "Just another pub",
                    },
                    new Activity
                    {
                        Title = "Future Activity 6",
                        Date = DateTime.Now.AddMonths(6),
                        Description = "Activity 6 months in future",
                        Category = "music",
                        City = "London",
                        PlaceOfEvent = "Roundhouse Camden",
                    },
                    new Activity
                    {
                        Title = "Future Activity 7",
                        Date = DateTime.Now.AddMonths(7),
                        Description = "Activity 2 months ago",
                        Category = "travel",
                        City = "London",
                        PlaceOfEvent = "Somewhere on the Thames",
                    },
                    new Activity
                    {
                        Title = "Future Activity 8",
                        Date = DateTime.Now.AddMonths(8),
                        Description = "Activity 8 months in future",
                        Category = "film",
                        City = "London",
                        PlaceOfEvent = "Cinema",
                    }
};
context.Activities.AddRange(activities);
context.SaveChanges();
            }
        }
    }
}