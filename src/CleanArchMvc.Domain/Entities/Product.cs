using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
  public sealed class Product : Entity
  {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; }

    public Product(string name, string description, decimal price, int stock, string image)
    {
      ValidateDomain(name, description, price, stock, image);

      Name = name;
      Description = description;
      Price = price;
      Stock = stock;
      Image = image;
    }

    public Product(int id, string name, string description, decimal price, int stock, string image)
    : this(name, description, price, stock, image)
    {
      ValidadeId(id);
      Id = id;
    }

    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
      ValidateDomain(name, description, price, stock, image);

      Name = name;
      Description = description;
      Price = price;
      Stock = stock;
      Image = image;
      CategoryId = categoryId;
    }

    private void ValidadeId(int id)
    {
      DomainExceptionValidation.When(id < 0, "Invalid id value: must be greater than or equal to zero");
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
      ValidateName(name);
      ValidateDescription(description);
      ValidatePrice(price);
      ValidateStock(stock);
      ValidateImage(image);
    }

    private void ValidateName(string name)
    {
      DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name: could not be null or empty");
      DomainExceptionValidation.When(name.Length < 3, "Invalid name: too short, minimum 3 characteres");
    }

    private void ValidateDescription(string description)
    {
      DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description: could not be null or empty");
      DomainExceptionValidation.When(description.Length < 5, "Invalid description: too short, minimum 5 characteres");
    }

    private void ValidatePrice(decimal price)
    {
      DomainExceptionValidation.When(price < 0, "Invalid price value: must be greater than or equal to zero");
    }

    private void ValidateStock(int stock)
    {
      DomainExceptionValidation.When(stock < 0, "Invalid stock value: must be greater than or equal to zero");
    }

    private void ValidateImage(string image)
    {
      DomainExceptionValidation.When(image?.Length > 250, "Invalid image name: too long, maximum 250 characteres");
    }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
  }
}