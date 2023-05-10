using Models;

namespace InternshipProject.RepositoryPattern
{
    /// <summary>
    /// Класс, описывающий репозиторий пользовательских групп.
    /// </summary>
    public class SQLUserGroupRepository
    {
        // База данных.
        private DataBaseContext _db;


        public SQLUserGroupRepository(DataBaseContext db)
        {
            _db = db;
        }
        // Метод добавления группы пользователя в базу данных.
        public async Task<bool> CreateAsync(UserGroup item)
        {
            await _db.Groups.AddAsync(item);
            return true;
        }

    }
}
