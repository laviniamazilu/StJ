using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class MockData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>() {
                new Category() { id = 1, description = "Pentru ea" },
                new Category() { id = 2, description = "Pentru el" },
                new Category() { id = 3, description = "Pentru cuplu" },
                new Category() { id = 4, description = "Pentru copii" }
            };
        }

        public static List<Category> GetSubCategories(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return new List<Category>() {
                        new Category() { id = 5, description = "Bratari", id_category = categoryId },
                        new Category() { id = 6, description = "Lanturi", id_category = categoryId },
                        new Category() { id = 7, description = "Coliere cu nume", id_category = categoryId }
                    };
                case 2:
                    return new List<Category>() {
                        new Category() { id = 5, description = "Bratari", id_category = categoryId },
                        new Category() { id = 6, description = "Lanturi", id_category = categoryId }
                    };
                case 3:
                    return new List<Category>() {
                        new Category() { id = 5, description = "Bratari", id_category = categoryId },
                        new Category() { id = 6, description = "Lanturi", id_category = categoryId }
                    };
                case 4:
                    return new List<Category>() {
                        new Category() { id = 5, description = "Bratari", id_category = categoryId },
                        new Category() { id = 6, description = "Lanturi", id_category = categoryId },
                        new Category() { id = 8, description = "Banuti mot", id_category = categoryId }
                    };
                default:
                    return new List<Category>();
            }
        }

        public static User GetUser()
        {
            return new User() { Name = "Lavinia Mazilu", Address = "", Email = "lavmaz@gmail.com" };
        }

        public static List<Product> GetProducts(int categoryId = 0)
        {
            return new List<Product>() {
                        new Product() { id = 5, name = "Bratara 1", picture_path = "Lant1", id_subcategory = categoryId },
                        new Product() { id = 6, name = "Bratara 2", picture_path = "Lant1", id_subcategory = categoryId },
                        new Product() { id = 7, name = "Coliere 1", picture_path = "Lant1", id_subcategory = categoryId },
                        new Product() { id = 7, name = "Coliere 2", picture_path = "Lant1", id_subcategory = categoryId }
                        };
            //switch (categoryId)
            //{
            //    case 1:
            //        return new List<Product>() {
            //            new Product() { Id = 5, Description = "Bratari", CategoryId = categoryId },
            //            new Product() { Id = 6, Description = "Lanturi", CategoryId = categoryId },
            //            new Product() { Id = 7, Description = "Coliere cu nume", CategoryId = categoryId }
            //        };
            //    case 2:
            //        return new List<Product>() {
            //            new Product() { Id = 5, Description = "Bratari", CategoryId = categoryId },
            //            new Product() { Id = 6, Description = "Lanturi", CategoryId = categoryId }
            //        };
            //    case 3:
            //        return new List<Product>() {
            //            new Product() { Id = 5, Description = "Bratari", CategoryId = categoryId },
            //            new Product() { Id = 6, Description = "Lanturi", CategoryId = categoryId }
            //        };
            //    case 4:
            //        return new List<Product>() {
            //            new Product() { Id = 5, Description = "Bratari", CategoryId = categoryId },
            //            new Product() { Id = 6, Description = "Lanturi", CategoryId = categoryId },
            //            new Product() { Id = 8, Description = "Banuti mot", CategoryId = categoryId }
            //        };
            //    default:
            //        return new List<Product>();
            //}
        }
    }
}
