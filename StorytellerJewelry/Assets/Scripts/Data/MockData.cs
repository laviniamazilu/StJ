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
                new Category() { Id = 1, Description = "Pentru ea" },
                new Category() { Id = 2, Description = "Pentru el" },
                new Category() { Id = 3, Description = "Pentru cuplu" },
                new Category() { Id = 4, Description = "Pentru copii" }
            };
        }

        public static List<Category> GetSubCategories(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return new List<Category>() {
                        new Category() { Id = 5, Description = "Bratari", CategoryId = categoryId },
                        new Category() { Id = 6, Description = "Lanturi", CategoryId = categoryId },
                        new Category() { Id = 7, Description = "Coliere cu nume", CategoryId = categoryId }
                    };
                case 2:
                    return new List<Category>() {
                        new Category() { Id = 5, Description = "Bratari", CategoryId = categoryId },
                        new Category() { Id = 6, Description = "Lanturi", CategoryId = categoryId }
                    };
                case 3:
                    return new List<Category>() {
                        new Category() { Id = 5, Description = "Bratari", CategoryId = categoryId },
                        new Category() { Id = 6, Description = "Lanturi", CategoryId = categoryId }
                    };
                case 4:
                    return new List<Category>() {
                        new Category() { Id = 5, Description = "Bratari", CategoryId = categoryId },
                        new Category() { Id = 6, Description = "Lanturi", CategoryId = categoryId },
                        new Category() { Id = 8, Description = "Banuti mot", CategoryId = categoryId }
                    };
                default:
                    return new List<Category>();
            }

        }
    }
}
