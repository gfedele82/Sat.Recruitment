using Sat.Recruitment.Models;

namespace Sat.Recruitment.DataAccess.DTOAdapter
{
    public static class UserAdapter
    {
        public static Schema.User ToDBModel(this User user)
        {
            if (user == null)
                return null;

            return new Schema.User()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Money = user.Money,
                UserType = user.UserType
            };

        }

        public static User ToModel(this Schema.User dbUser)
        {
            if (dbUser == null)
                return null;


            return new User()
            {
                Id = dbUser.Id,
                Name = dbUser.Name,
                Email = dbUser.Email,
                Address = dbUser.Address,
                Phone = dbUser.Phone,
                Money = dbUser.Money,
                UserType = dbUser.UserType
            };

        }
    }
}
