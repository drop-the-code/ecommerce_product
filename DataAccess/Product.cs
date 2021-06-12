// ReSharper disable InconsistentNaming
namespace ecommerce_product.DataAccess
{
    public class Product
    {
        // DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProviderCNPJ { get; set; }
        public string Description { get; set; }
        
    }

}