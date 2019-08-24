using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils;
using System.Linq;

public class CategoriesController : MonoBehaviour
{
    public List<Category> Categories;

    private List<IPrefabComponent> _categoriesPool;

    public void PopulateCategories(int categoryId = 0, string actionRoutePath = "SubCategories")
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
            var wasNull = UsefullUtils.CheckInPool(
                category.Id,
                GameHiddenOptions.Instance.CategoryPrefab,
                this.transform,
                out IPrefabComponent categoryComponent,
                ref _categoriesPool
                );

            categoryComponent.Id = category.Id;
            categoryComponent.Route = new Route()
            {
                RoutePath = actionRoutePath,
                RouteKey = category.Id
            };

            (categoryComponent as CategoryComponent).CategoryName.text = category.Description;

            if (wasNull)
            {
                _categoriesPool.Add(categoryComponent);
            }
        }
    }

    private CategoryComponent CreateCategoryComponent(Category category)
    {
        var go = UsefullUtils.CreateUiObject(Instantiate(GameHiddenOptions.Instance.CategoryPrefab), category.Description, this.transform);
        return go.GetComponent<CategoryComponent>();
    }
}
