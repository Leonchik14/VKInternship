using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Класс, описывающий группу пользователя.
    /// </summary>
    [Index(nameof(Id))]
    public class UserGroup
    {
        public static long Count { get; set; }
        public long Id { get; set; }
        public Role Code { get; set; }
        public string Description { get; set; }

        UserGroup() { }

        public UserGroup(Role role, string description) 
        {
            Id = Count;
            Description = description;
            Code = role;            
            Count++;
        }
    }
    public enum Role : int
    {
        Admin = 0,
        User = 1
    }
}
