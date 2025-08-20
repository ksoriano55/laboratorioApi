using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomer();

        Customer InsertCustomer(Customer Customer, int userId);
    }
}
