using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var DbProduct = _db.Products.FirstOrDefault(item => item.Id == product.Id);
            if(DbProduct != null)
            {
                DbProduct.Title = product.Title;
                DbProduct.Description = product.Description;
                DbProduct.ISBN = product.ISBN;
                DbProduct.Author = product.Author;
                DbProduct.CategoryId = product.CategoryId;
                DbProduct.CoverTypeId = product.CoverTypeId;
                DbProduct.Price = product.Price;
                DbProduct.ListPrice = product.ListPrice;
                if(product.ImageUrl != null)
                    DbProduct.ImageUrl = product.ImageUrl;
                _db.Products.Update(DbProduct);
            }
           
        }
    }
}
