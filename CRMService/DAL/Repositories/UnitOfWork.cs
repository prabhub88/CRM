using DAL.Models;

namespace DAL.Repositories
{
    public class UnitOfWork:IDisposable
    {
        private CRMDbContext Context;
        private Repository<Customer> customerRepository;
        private Repository<Gender> genderRepository;
        private Repository<User> userRepository;
        private Repository<UserType> userTypesRepository;

        public UnitOfWork(CRMDbContext context)
        {
            Context = context;
        }

        public Repository<Customer> CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new Repository<Customer>(Context);
                return customerRepository;
            }
        }

        public Repository<Gender> GenderRepository
        {
            get
            {
                if (genderRepository == null)
                    genderRepository = new Repository<Gender>(Context);

                return genderRepository;
            }
        }

        public Repository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new Repository<User>(Context);

                return UserRepository;
            }
        }

        public Repository<UserType> UserTypesRepository
        {
            get
            {
                if (userTypesRepository == null)
                    userTypesRepository = new Repository<UserType>(Context);

                return userTypesRepository;
            }
        }

        public void Save()
        {
            Context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
