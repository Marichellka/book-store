using BookStore.DAL.Models;
using BookStore.DAL.Specifications;

namespace BookStore.Tests.Unit;

public class OrSpecificationTests: SpecificationTestBase
{
    private ISpecification<BaseModel> _trueSpecification = new TrueSpecification<BaseModel>();
    private ISpecification<BaseModel> _falseSpecification = new FalseSpecification<BaseModel>();

    [Test]
    public void TrueAndTrue_Satisfies()
    {
        OrSpecification<BaseModel> specification = new(_trueSpecification, _trueSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.True(result);
    }

    [Test]
    public void TrueAndFalse_Satisfies()
    {
        OrSpecification<BaseModel> specification = new(_trueSpecification, _falseSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.True(result);
    }

    [Test]
    public void FalseAndFalse_DoesNotSatisfy()
    {
        OrSpecification<BaseModel> specification = new(_falseSpecification, _falseSpecification);

        bool result = IsSatisfiedBy(specification, new BaseModel());
        
        Assert.False(result);
    }
}