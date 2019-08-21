using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;
using System.Linq;

public class CategoriesController : MonoBehaviour
{
    public GameObject CategoryPrefab;

    public List<Category> Categories;

    private List<CategoryComponent> _categoriesPool;

    public void PopulateCategories(int categoryId = 0)
    {
        if (categoryId == 0)
        {
            Categories = MockData.GetCategories();
        }
        else
        {
            Categories = MockData.GetSubCategories(categoryId);
        }

        if (_categoriesPool != null)
        {
            foreach (CategoryComponent cc in _categoriesPool)
            {
                cc.gameObject.SetActive(false);
            }
        }

        foreach (Category category in Categories)
        {
            CategoryComponent categoryComponent = null;
            var wasNull = false;

            if (_categoriesPool == null)
            {
                _categoriesPool = new List<CategoryComponent>();
                wasNull = true;
            }
            else
            {
                wasNull = _categoriesPool.Count(c => c.Id == category.Id) == 0;
                if (wasNull == false)
                {
                    categoryComponent = _categoriesPool.FirstOrDefault(c => c.Id == category.Id);
                    categoryComponent.gameObject.SetActive(true);
                }
            }

            if (wasNull == true)
            {
                categoryComponent = CreateCategoryComponent(category);
            }

            categoryComponent.Id = category.Id;
            categoryComponent.CategoryName.text = category.Description;
            categoryComponent.Route = new Route()
            {
                RoutePath = "SubCategories",
                RouteKey = category.Id
            };

            if (wasNull)
            {
                _categoriesPool.Add(categoryComponent);
            }
        }
    }

    private CategoryComponent CreateCategoryComponent(Category category)
    {
        var go = UsefullUtils.CreateUiObject(Instantiate(CategoryPrefab), category.Description, this.transform);
        return go.GetComponent<CategoryComponent>();
    }
}
