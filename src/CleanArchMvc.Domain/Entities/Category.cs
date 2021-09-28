using System.Collections.Generic;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }
        
        public Category(string name)
        {
            ValidateName(name);
            Name = name;
        }
        
        public Category(int id, string name)
        {
            ValidadeId(id);
            ValidateName(name);

            Id = id;
            Name = name;
        }

        public void Update(string name)
        {
            ValidateName(name);
            Name = name;
        }

        public ICollection<Product> Products { get; set; }

        private void ValidadeId(int id)
        {
            DomainExceptionValidation.When(id < 0, "Invalid id value: must be greater than or equal to zero");
        }

        private void ValidateName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name: could not be null or empty");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name: too short, minimum 3 characteres");
        }
    }
}