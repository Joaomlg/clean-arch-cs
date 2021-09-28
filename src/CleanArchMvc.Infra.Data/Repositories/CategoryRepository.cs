using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CleanArchMvc.Domain.Interface;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Infra.Data.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    ApplicationDbContext _context;
    
    public CategoryRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int? id)
    {
      return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
      _context.Add(category);
      await _context.SaveChangesAsync();
      return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
      _context.Update(category);
      await _context.SaveChangesAsync();
      return category;
    }

    public async Task<Category> RemoveAsync(Category category)
    {
      _context.Remove(category);
      await _context.SaveChangesAsync();
      return category;
    }
  }
}