using Models;

namespace InternshipProject.RepositoryPattern
{
    public class UnitOfWork : IDisposable
    {
        private DataBaseContext _db;
        private SQLUserRepository sqlUserRepository;
        private SQLUserGroupRepository sqlUserGroupRepository;
        private SQLUserStateRepository sqlUserStateRepository;
        private bool _disposed;
        public UnitOfWork(DataBaseContext db)
        {
            this._db = db;
            
            Models.User.Сount = _db.Users.Count() == 0 ? 1 : db.Users.Max(x => x.Id) + 1;
            Models.UserGroup.Count = _db.Groups.Count() == 0 ? 1 : db.Groups.Max(x => x.Id) + 1;
            Models.UserState.Count = _db.States.Count() == 0 ? 1 : db.States.Max(x => x.Id) + 1;
        }

        public SQLUserRepository Users
        {
            get
            {
                if (sqlUserRepository == null)
                {
                    sqlUserRepository = new SQLUserRepository(_db);
                }
                return sqlUserRepository;
            }
        }

        public SQLUserGroupRepository Group
        {
            get
            {
                if (sqlUserGroupRepository == null)
                {
                    sqlUserGroupRepository = new SQLUserGroupRepository(_db);
                }
                return sqlUserGroupRepository;
            }
        }
        public SQLUserStateRepository State
        {
            get
            {
                if (sqlUserStateRepository == null)
                {
                    sqlUserStateRepository = new SQLUserStateRepository(_db);
                }
                return sqlUserStateRepository;
            }
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
