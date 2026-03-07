using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userDAO = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            return userDAO.GetUserWithUsernameAndPassword(model);
        }

        public void AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.NameSurname = model.Name;
            user.isAdmin = model.isAdmin;
            user.AddDate = DateTime.Now;
            user.isDeleted = false;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            int ID = userDAO.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
        }

        public List<UserDTO> GetUsers()
        {
            return userDAO.GetUsers();
        }
    }
}
