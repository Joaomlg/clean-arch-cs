using System;
using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Tests
{
  public class ProductUnitTest
  {
    [Fact]
    public void CreateProduct_WithValidParameters_ResultNoValidationException()
    {
      Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "Product.img");
      action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNegativeIdValue_ResultValidationException()
    {
      Action action = () => new Product(-1, "Product name", "Product description", 9.99m, 99, "Product.img");
      action.Should().Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithShortNameValue_ResultValidationException()
    {
      Action action = () => new Product(1, "Pr", "Product description", 9.99m, 99, "Product.img");
      action.Should().Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithLongImageName_ResultValidationException()
    {
      Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, new string('a', 300));
      action.Should().Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_ResultNoValidationException()
    {
      Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
      action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_ResultNoNullReferenceException()
    {
      Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
      action.Should().NotThrow<NullReferenceException>();
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_WithInvalidStockValue_ResultValidationException(int stock)
    {
      Action action = () => new Product(1, "Product name", "Product description", 9.99m, stock, "Product.img");
      action.Should().Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithInvalidPriceValue_ResultValidationException()
    {
      Action action = () => new Product(1, "Product name", "Product description", -9.99m, 1, "Product.img");
      action.Should().Throw<DomainExceptionValidation>();
    }
  }
}