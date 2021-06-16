using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService1;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_product.Services

{
    public class ProductService : ProductCRUD.ProductCRUDBase
    {
        private DataAccess.AppDbContext db = null;
        
        public ProductService(DataAccess.AppDbContext db)
        {
            this.db = db;
            this.db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public override Task<Products> SelectAll(Empty request, ServerCallContext context)
        {
            Products responseData = new Products();
            var query = from prod in db.Products select new Product()
            {
                ProductID = prod.ProductID,
                ProductName = prod.ProductName,
                ProductPrice = prod.ProductPrice,
                ProviderCNPJ = prod.ProviderCNPJ,
                Description = prod.Description
            };
            responseData.Items.AddRange(query.ToArray());
            return Task.FromResult(responseData);
        }

        public override Task<Product> SelectByID(ProductFilter request, ServerCallContext context)
        {
            var query = db.Products.Find(request.ProductID);
            Product prod = new Product()
            {
                ProductID = query.ProductID,
                ProductName = query.ProductName,
                ProductPrice = query.ProductPrice,
                ProviderCNPJ = query.ProviderCNPJ,
                Description = query.Description
            };
            return Task.FromResult(prod);
        }
        public override Task<Empty> Insert(Product request, ServerCallContext context)
        {
            db.Products.Add(new DataAccess.Product()
            {
                ProductID = request.ProductID,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProviderCNPJ = request.ProviderCNPJ,
                Description = request.Description
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> Update(Product request, ServerCallContext context)
        {
            db.Products.Update(new DataAccess.Product()
            {
                ProductID = request.ProductID,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProviderCNPJ = request.ProviderCNPJ,
                Description = request.Description
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }


        
        public override Task<Empty> Delete(ProductFilter request, ServerCallContext context)
        {
            var query = db.Products.Find(request.ProductID);
            
            db.Products.Remove(new DataAccess.Product()
            {
                ProductID = query.ProductID,
                ProductName = query.ProductName,
                ProductPrice = query.ProductPrice,
                ProviderCNPJ = query.ProviderCNPJ,
                Description = query.Description
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }
    }
}
