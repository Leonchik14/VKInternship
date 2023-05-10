using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// Класс, описывающий пользователя.
    /// </summary>
    [Index(nameof(Login))]    
    public class User
    {
        public static long Сount { get; set; }
        [Key]
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private DateTime CreatedDate { get; set; }
        public long UserGroupId { get; set; }
        public long UserStateId { get; set; }

        public User() { }
        public User(string login, string password, DateTime createdDate, long userGroupId, long userStateId)
        {
            Id = Сount;
            Login = login;
            Password = password;
            CreatedDate = createdDate;
            UserGroupId = userGroupId;
            UserStateId = userStateId;
            Сount++;
        }
    }
}