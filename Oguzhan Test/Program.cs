using Oguzhan_Test.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OguzhanBilgi_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        public static OrderStatistics getOrderStatistics(List<Order> orders)
        {
            OrderStatistics orderStatics = new OrderStatistics();
            List<OrderStatisticCategory> orderStaticsCategories = new List<OrderStatisticCategory>();
            List<OrderProductsEntity> orderProducts = new List<OrderProductsEntity>();

            foreach (var order in orders)
            {
                foreach (var product in order.products)
                {
                    if (orderStaticsCategories.Count == 0)   // İlk işlem
                        orderStaticsCategories.Add(new OrderStatisticCategory   // yeni category istatistiği ekler
                        {
                            ID = product.CategoryID,
                            TotalPriceOfProductsSold = orderProducts.FirstOrDefault(o => o.OrderID == order.ID && o.ProductID == product.ID).Price,
                            NumberOfProductsSold = 1
                        });
                    else
                    {
                        if (orderStaticsCategories.Any(c => c.ID == product.CategoryID)) // bu kategoride daha önce ürün okundu
                        {   // var olan category istatistiğini günceller
                            var orderCategory = orderStaticsCategories.FirstOrDefault(c => c.ID == product.CategoryID);
                            orderCategory.TotalPriceOfProductsSold += orderProducts
                                .FirstOrDefault(o => o.OrderID == order.ID && o.ProductID == product.ID).Price;
                            orderCategory.NumberOfProductsSold = ++ orderStaticsCategories
                                .FirstOrDefault(c => c.ID == product.CategoryID).NumberOfProductsSold;
                        }
                        else  // bu kategoride daha önce ürün okunmadı
                            orderStaticsCategories.Add(new OrderStatisticCategory   // yeni category istatistiği ekler
                            {
                                ID = product.CategoryID,
                                TotalPriceOfProductsSold = orderProducts.FirstOrDefault(o => o.OrderID == order.ID && o.ProductID == product.ID).Price,
                                NumberOfProductsSold = 1
                            });
                    }
                }
            }

            orderStatics.categories = orderStaticsCategories;

            return orderStatics;
        }

        public static List<ProductsEntity> getProductsOfCategoryAndDescendants(int categoryID)
        {
            // Burayı doldurun:
            // Verilen ID'ye sahip kategori ve onun alt kategorilerindeki
            // tüm ürünleri veritabanından alıp List<Product>
            // tipinde döndüren kodu yazınız
            List<ProductsEntity> products = new List<ProductsEntity>();

            // Ana katagorideki product
            var category = getCategoryByID(categoryID);
            products.AddRange(getProductsByCategoryID(category.ID));

            foreach(var subCategory in getCategoriesByParentID(categoryID))
            {
                products.AddRange(getProductsByCategoryID(subCategory.ID));
            }

            return products;
        }

        public static CategoriesEntity getCategoryByID(int ID)  // AnaID
        {
            // db kodu
            return new CategoriesEntity();
        }

        public static List<CategoriesEntity> getCategoriesByParentID(int parentId) // SubID
        {
            // db kodu
            return new List<CategoriesEntity>();
        }

        public static List<ProductsEntity> getProductsByCategoryID(int categoryID) // Verilen category'deki ürünleri getirir.
        {
            // db kodu
            return new List<ProductsEntity>();
        }
    }

    public class Order
    {
        public int ID { get; set; }
        public List<ProductsEntity> products { get; set; }
    }
    public class OrderStatistics
    {
        public List<OrderStatisticCategory> categories { get; set; }
    }
    public class OrderStatisticCategory
    {
        public int ID { get; set; }
        public int NumberOfProductsSold { get; set; }
        public double TotalPriceOfProductsSold { get; set; }
    }
}