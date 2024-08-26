using ASM.Entities;

namespace ASM.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomer();
        Task<Customer> GetCustomerById(int Id);
        Task<bool> DeleteCustomer(int Id);
        Task<bool> UpdateCustomer(Customer customer);
        Task<int> CreateCustomer(Customer customer);
    }
}
