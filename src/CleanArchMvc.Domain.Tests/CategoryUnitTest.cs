using System;
using Xunit;
using FluentAssertions;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Some Category Name");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_WithNegativeIdValue_ResultDomainException()
        {
            Action action = () => new Category(-1, "Some Category Name");
            action.Should()
                .Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_ShortNameValue_ResultDomainException()
        {
            Action action = () => new Category(1, "AB");
            action.Should()
                .Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_MissingNameValue_ResultDomainException()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_ResultDomainException()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}
