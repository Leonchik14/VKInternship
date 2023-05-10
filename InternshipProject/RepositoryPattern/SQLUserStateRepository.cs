using Models;

namespace InternshipProject.RepositoryPattern
{
    /// <summary>
    /// Класс, описывающий репозиторий состояний пользователя.
    /// </summary>
    public class SQLUserStateRepository
    {
        // База данных.
        private DataBaseContext _db;

        public SQLUserStateRepository(DataBaseContext db)
        {
            _db = db;
        }
        // Метод, реализующий добавление состояния в базу данных.
        public async Task<bool> CreateAsync(UserState item)
        {
            await _db.States.AddAsync(item);
            return true;
        }

        // Метод, реализующий получения группы из базы данных.
        public async Task<UserState?> GetElementAsync(long id)
        {
            return await _db.States.FindAsync(id);
        }
        // Метод, реализующий обновление состояния пользователя в базе данных.
        public async Task UpdateAsync(long id)
        {
            (await _db.States.FindAsync(id)).Code = State.Blocked;
        }

    }
}
