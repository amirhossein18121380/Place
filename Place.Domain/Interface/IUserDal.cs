using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Domain.Models;

namespace Place.Domain.Interface;
public interface IUserDal
{
    User GetById(long id);
    Task<long> Insert(Domain.Models.User entity);

    Task<User?> GetByUserName(string UserName);
}