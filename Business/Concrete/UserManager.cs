using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(User user)
        {
            var existingUser = _userDal.Get(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new Exception("Bu mail adresi kullaniliyor");
            }
            _userDal.Add(user);

        }
        

        

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        User IUserService.GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
