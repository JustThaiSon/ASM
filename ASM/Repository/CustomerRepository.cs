using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ASM.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDbContext _context;
        public CustomerRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return _context.SaveChanges();
        }

        public async Task<bool> DeleteCustomer(int Id)
        {
            var Customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == Id);
            if (Customer == null) return false;

            _context.Customers.RemoveRange(Customer);
			await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int Id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == Id);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var CustomerUpdate = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
            if (CustomerUpdate == null) return false;
            CustomerUpdate.UserName = customer.UserName;
            CustomerUpdate.Email = customer.Email;
            CustomerUpdate.Address = customer.Address;
            CustomerUpdate.PassWord = customer.PassWord;
            CustomerUpdate.FullName = customer.FullName;
            CustomerUpdate.Gender = customer.Gender;
            CustomerUpdate.DateOfBirth = customer.DateOfBirth;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
