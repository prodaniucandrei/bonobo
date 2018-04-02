using bonobo.Dtos;
using bonobo.ViewModel;
using bonobo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bonobo.Helpers
{
    public class Helper
    {
        private static UserProfileView user;
        private static UserDto userdto;


        //------------------------------GET-CURRENT-USER-PROFILE-VIEW-OBJ---------------------
        public static UserProfileView GetLoggedUserProfileView()
        {
            if(App.UserDatabase.GetUser() != null)
            {
                return new UserProfileView
                {
                    FirstName = App.UserDatabase.GetUser().FirstName,
                    LastName = App.UserDatabase.GetUser().LastName,
                    Gender = App.UserDatabase.GetUser().Gender,
                    Birthdate = App.UserDatabase.GetUser().BirthDate,
                    HeaderImage = "http://acephalous.typepad.com/.a/6a00d8341c2df453ef017d3c2bd399970c-500wi",
                    ProfileImage = "https://manofmany.com/wp-content/uploads/2017/07/Jon-Snow-2.jpg",
                    Tagline = "I conquer super villains and make the world a safer place.",
                    ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                    Reviews = 16,
                    Hosted = 11,
                    Joined = 24
                };
            }
            else
            {
                //default current user
                return new UserProfileView
                {
                    FirstName = "Jon",
                    LastName = "Snow",
                    Gender = "Male",
                    Birthdate = new DateTime(1989, 12, 22),
                    HeaderImage = "http://acephalous.typepad.com/.a/6a00d8341c2df453ef017d3c2bd399970c-500wi",
                    ProfileImage = "https://manofmany.com/wp-content/uploads/2017/07/Jon-Snow-2.jpg",
                    Tagline = "I conquer super villains and make the world a safer place.",
                    ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                    Reviews = 16,
                    Hosted = 11,
                    Joined = 24
                };
            }
        }


        //------------------------------GET-DEFAULT-ACTIVITY----------------------------------
        public static ActivityView GetDefaultActivity()
        {
            return new ActivityView
            {
                ActivityTitle = "What would it be like to live on Mars?",
                Category = "Astronomy",
                ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                NoPlaces = 3,
                Where = "Botanical garden",
                When = new DateTime(2018, 3, 30),
                Image = "http://cdns.yournewswire.com/wp-content/uploads/2016/06/NASA-life-on-Mars-678x381.jpg",
                JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                HostUserId = "defaulthost"
            };
        }


        //------------------------------GET-ACTIVITY-BY-DEFAULT-------------------------------
        public static async Task<ActivityView> GetActivityById(string ActivityId)
        {
            ActivityDto a;
            if (ActivityId != null)
            {
                a = await App.RestService.GetActivityById(new GetActivityByIdViewModel { Id = ActivityId });
                if(a != null)
                {
                    //TODO: construct ActivityView obj
                    return new ActivityView
                    {
                        ActivityTitle = a.ActivityName,
                        Category = a.Category,
                        ShortDescription = a.ShortDescription,
                        NoPlaces = a.NoPlaces,
                        Where = a.Where,
                        When = a.When,
                        Image = "http://cdns.yournewswire.com/wp-content/uploads/2016/06/NASA-life-on-Mars-678x381.jpg",
                        JoinedUsersListIds = a.JoinedUsersIds,
                        HostUserId = a.ActivityHostId
                    };
                }
                else
                {
                    return GetDefaultActivity();
                }
            } 
            return GetDefaultActivity();
        }


        //------------------------------GET-USER-PROFILE-VIEW-OBJ-----------------------------
        public static async Task<UserProfileView> GetUserProfileViewObj(string UserId)
        {
            if(UserId != null)
            {
                if (UserId != "defaultguest" || UserId != "defaulthost")
                {
                   userdto = await App.RestService.FindUserById(
                       new FindUserByIdViewModel
                       {
                           Id = UserId
                       });
                }
                else
                {
                    userdto = null;
                }
            }
            else
            {
                userdto = null;
            }

            if(userdto != null)
            {
                user = new UserProfileView
                {
                    FirstName = userdto.FirstName,
                    LastName = userdto.LastName,
                    Gender = userdto.Gender,
                    Birthdate = userdto.BirthDate,
                    HeaderImage =
               "http://hdqwalls.com/wallpapers/game-of-thrones-season-7-drogon-and-khaleesi-im.jpg",
                    ProfileImage =
               "https://cdn.techjuice.pk/wp-content/uploads/2017/08/daenerys-1024x576.jpg",
                    Tagline = "I was born to rule the Seven Kingdoms, and I will.",
                    ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                    Reviews = 31,
                    Hosted = 9,
                    Joined = 15
                };
            }
            else
            {
                //default guest & default host user profile
                user = new UserProfileView
                {
                    FirstName = "Daenerys",
                    LastName = "Stormborn",
                    Gender = "Female",
                    Birthdate = new DateTime(1993, 3, 21),
                    HeaderImage = "http://hdqwalls.com/wallpapers/game-of-thrones-season-7-drogon-and-khaleesi-im.jpg",
                    ProfileImage = "https://cdn.techjuice.pk/wp-content/uploads/2017/08/daenerys-1024x576.jpg",
                    Tagline = "I was born to rule the Seven Kingdoms, and I will.",
                    ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                    Reviews = 31,
                    Hosted = 9,
                    Joined = 15
                };
            }
            return user;
        }


        //TODO
        //------------------------------CHECK-NULL-INFO-FOR-CREATE-ACTIVITY-------------------
        public static bool CheckNullInformation(CreateActivityViewModel a)
        {
            if (a.ActivityName == null ||
                a.Category == null ||
                a.ShortDescription == null ||
                a.When == null ||
                a.Where == null)
                return false;
            else
                return true;
        }


        //------------------------------SEED-ACTIVITY-VIEW-LIST-------------------------------
        public static List<ActivityView> SeedActivityViewList()
        {
            List<ActivityView> activities = new List<ActivityView>
            {
                 new ActivityView {
                    ActivityTitle = "Do you think all the hype about privacy is warranted?",
                    Category = "Privacy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 8,
                    Where = "Central Park",
                    Image = "https://placeimg.com/640/480/people/8",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What’s the last book you read?",
                    Category = "Books",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 2,
                    Where = "Botanical garden",
                    Image = "https://placeimg.com/640/480/people/1",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What place do I really need to see?",
                    Category = "Travel",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/2",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                 new ActivityView{
                    ActivityTitle = "Would you buy a self-driving car if it was affordable?",
                    Category = "Automotive",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/7",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What common misconceptions do people have about your hobby?",
                    Category = "Hobbies",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 3,
                    Where = "Roses Park",
                    Image = "https://placeimg.com/640/480/people/3",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What would it be like to live on Mars?",
                    Category = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 6,
                    Where = "Astroclubul Antares",
                    Image = "https://placeimg.com/640/480/people/4",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What kinds of things are you interested in learning more about?",
                    Category = "Personal Development",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 1,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/5",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "What do direction do you think the internet is headed in?",
                    Category = "Internet",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 3,
                    Where = "Central Park",
                    Image = "https://placeimg.com/640/480/people/6",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "Do you think that VR will become mainstream in the near future?",
                    Category = "Technology",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/7",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    },
                new ActivityView{
                    ActivityTitle = "Do you think we’ll find microscopic alien life in our own solar system?",
                    Category = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Botanical garden",
                    Image = "https://placeimg.com/640/480/people/7",
                    JoinedUsersListIds = new List<string>
                        {
                            "defaultguest",
                        },
                    HostUserId = "defaulthost",
                    }
            };

            return activities;
        }
    }
}
