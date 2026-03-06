using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UserDAO: PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            try
            {
                UserDTO dto = new UserDTO();
                T_User user = db.T_User.First(x => x.Username == model.Username && x.Password == model.Password);
                if (user != null)
                {
                    if (user.ID != 0)
                    {
                        dto.ID = user.ID;
                        dto.Username = user.Username;
                        dto.Name = user.NameSurname;
                        dto.ImagePath = user.ImagePath;
                        dto.isAdmin = user.isAdmin;
                    }
                }
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
