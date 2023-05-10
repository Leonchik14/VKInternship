using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Класс, описывающий состояние пользователя.
    /// </summary>
    [Index(nameof(Id))]
    public class UserState
    {
        public static long Count { get; set; }
        public long Id { get; set; }
        public State Code { get; set; }
        public string Description { get; set; }

        UserState() { }

        public UserState(string description) 
        {
            Id = Count;
            Description = description;
            Code = State.Active;
            Count++;
        }

    }
    public enum State : int
    {
        Active = 0,
        Blocked = 1
    }
}
