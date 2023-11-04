using System.Linq.Expressions;
using BookStore.DAL.Models;

namespace BookStore.DAL.Specifications.Users;

public class UserNameSpecification: ISpecification<User>
{
    private string _name;

    public UserNameSpecification(string name)
    {
        _name = name;
    }

    public Expression<Func<User, bool>> SpecificationExpression => user => user.Name == _name;
}