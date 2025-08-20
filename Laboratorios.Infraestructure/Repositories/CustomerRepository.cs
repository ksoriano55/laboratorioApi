using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Laboratorios.Infraestructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LaboratoriosContext _context;

        public CustomerRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            try
            {
                var Customers = await _context.Customer.ToListAsync();
                return Customers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Customer InsertCustomer(Customer Customer, int userId)
        {
            try
            {
                if (_context.Customer.Any(x => x.name == Customer.name && x.id != Customer.id))
                {
                    throw new InvalidOperationException("Ya existe un Cliente con este nombre");
                }
                if (Customer.id > 0)
                {
                    Customer.modifiedUser = userId;
                    Customer.modifiedDate = DateTime.UtcNow;
                    _context.Entry(Customer).State = EntityState.Modified;
                }
                else
                {
                    Customer.createdUser = userId;
                    Customer.createdDate = DateTime.Now;
                    _context.Customer.Add(Customer);
                }
                _context.SaveChanges();

                return Customer;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}