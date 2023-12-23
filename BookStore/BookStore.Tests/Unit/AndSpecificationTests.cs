using BookStore.DAL.Models;
using BookStore.DAL.Specifications;

namespace BookStore.Tests.Unit;

public class AndSpecificationTests: SpecificationTestBase
{
    private ISpecification<BaseModel> _trueSpecification = new TrueSpecification<BaseModel>();
    private ISpecification<BaseModel> _falseSpecification = new FalseSpecification<BaseModel>();

    [Test]
    public void TrueAndTrue_Satisfies()
    {
        AndSpecification<BaseModel> specification = new(_trueSpecification, _trueSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void TrueAndFalse_DoesNotSatisfy()
    {
        AndSpecification<BaseModel> specification = new(_trueSpecification, _falseSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void FalseAndFalse_DoesNotSatisfy()
    {
        AndSpecification<BaseModel> specification = new(_falseSpecification, _falseSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.That(result, Is.False);
    }
}