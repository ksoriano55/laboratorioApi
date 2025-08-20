using Microsoft.EntityFrameworkCore;
using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;


namespace Laboratorios.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LaboratoriosContext _context;

        public UserRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User> InsertUser(User usuario)
        {
            var user = new User();
            try
            {  
                var existuser = await _context.User.Where(x => x.UserName == usuario.UserName).FirstOrDefaultAsync();
                if (existuser != null)
                { 
                    throw new InvalidOperationException("Ya existe Usuario");
                }

                user.UserName = usuario.UserName;
                user.Status = true;
                _context.User.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e){
                throw new InvalidOperationException("Error al crear el usuario: " + e.Message.ToString());
            }

            return user;
        }

        public async Task<User> EditUser(User usuario)
        {
            var existuser = new User();
            try
            {
                existuser = await _context.User.Where(x=>x.UserId==usuario.UserId).FirstOrDefaultAsync();
                if (existuser != null)
                {
                    var exist = _context.User.Where(x => x.UserName == usuario.UserName && x.UserId!=existuser.UserId).FirstOrDefault();
                    if (exist != null)
                    {
                        throw new InvalidOperationException("Ya existe Usuario");
                    }
                    existuser.UserName= usuario.UserName;
                    existuser.Status= usuario.Status;
                    if(usuario.userSAP!= null && usuario.passwordSAP != null)
                    {
                        existuser.userSAP = usuario.userSAP;
                        existuser.passwordSAP = usuario.passwordSAP;
                    }
                    _context.Entry(existuser);
                    _context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Error al editar el usuario");
                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error al editar el usuario");
            }

            return existuser;
        }

    }
}
