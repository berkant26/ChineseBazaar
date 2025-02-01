using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
namespace DataAccess.Concrete
{
    public class EfUserDal :EfEntityRepositoryBase<User,ChineseBazaarContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ChineseBazaarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();

            }
        }
    }
}

