using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Net.WebSockets;
using System.Text.RegularExpressions;

namespace InternshipProject.RepositoryPattern
{

    /// <summary>
    /// Класс, описывающий репозиторий пользователя.
    /// </summary>
    public class SQLUserRepository
    {
        // База данных.
        private DataBaseContext _db;
        public static bool adminExists = false;

        public SQLUserRepository(DataBaseContext db)
        {
            _db = db;
        }
        // Метод добавления пользователя в базу данных.
        public async Task<bool> CreateAsync(User item, Role role)
        {
            if (await IsExistAsync(item.Login))
            {
                return false;
            }

            if (role == Role.Admin && adminExists == false)
            {
                adminExists = true;   
            }
            else if (role == Role.Admin && adminExists == true)
            {
                return false;
            }

            _db.Users.Add(item);
            await Task.Delay(5000);
            return true;
        }
        // Метод получения всех пользователей из базы данных.
        public async Task<IEnumerable<Object>> GetAllAsync()
        {
            return await (from u in _db.Users
                    join g in _db.Groups on u.UserGroupId equals g.Id
                    join s in _db.States on u.UserStateId equals s.Id
                    select new {u.Id, u.Login, u.Password, GroupStatus = g.Code.ToString(), UserState = s.Code.ToString()}).ToListAsync();
        }
        public async Task<User> GetUserByLogin(string login)
        {
            return await (from u in _db.Users
                          where u.Login == login
                          select u).SingleOrDefaultAsync();
        }
        // Метод получения информации о пользователе по логину.
        public async Task<Object> GetElementAsync(string login)
        {
            return await (from u in _db.Users
                    where u.Login == login
                    join g in _db.Groups on u.UserGroupId equals g.Id
                    join s in _db.States on u.UserStateId equals s.Id
                    select new { u.Id, u.Login, u.Password, GroupStatus = g.Code.ToString(), UserState = s.Code.ToString() }).SingleOrDefaultAsync();

        }

        // Метод, осуществляющий проверку существования пользователя с данным логином в базе данных.
        public async Task<bool> IsExistAsync(string login) 
        {
            return await _db.Users.AnyAsync(x => x.Login == login);
        }

    }
}
