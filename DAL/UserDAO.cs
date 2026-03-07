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

        public UserDTO GetUsersWithID(int iD)
        {
            T_User user = db.T_User.First(x => x.ID == iD);
            UserDTO dto = new UserDTO();
            dto.ID = user.ID;
            dto.Name = user.NameSurname;
            dto.Username = user.Username;
            dto.Password = user.Password;
            dto.isAdmin = user.isAdmin;
            dto.Email = user.Email;
            dto.ImagePath = user.ImagePath;
            return dto;
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.Name;
                user.Username = model.Username;
                if (model.ImagePath != null)
                    user.ImagePath = model.ImagePath;
                user.Email = model.Email;
                user.Password = model.Password;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.isAdmin = model.isAdmin;
                return oldImagePath;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDTO> GetUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            List<T_User> users = db.T_User.Where(x => x.isDeleted == false).ToList();

            foreach(var item in users)
            {
                UserDTO dto = new UserDTO();
                dto.ID = item.ID;
                dto.Name = item.NameSurname;
                dto.Username = item.Username;
                dto.ImagePath = item.ImagePath;
                userDTOs.Add(dto);
            }

            return userDTOs;
        }
    }
}
