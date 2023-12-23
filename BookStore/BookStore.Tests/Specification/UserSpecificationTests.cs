using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Users;

namespace BookStore.Tests.Specification;

public class UserSpecificationTests : SpecificationTestBase
{
    [Test]
    public void UserNameSpecification_Satisfies()
    {
        UserNameSpecification specification = new(name: "User");

        bool result = IsSatisfiedBy(specification, new User { Name = "User" });

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserNameSpecification_DoesNotSatisfy()
    {
        UserNameSpecification specification = new(name: "User1");

        bool result = IsSatisfiedBy(specification, new User { Name = "User2" });

        Assert.That(result, Is.False);
    }
}