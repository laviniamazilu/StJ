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

    private string _actionRoutePath;

    public void PopulateCategories(int categoryId = 0, string actionRoutePath = "SubCategories")
    {
        _actionRoutePath = actionRoutePath;

        CategoryData.Instance.GetCategories(categoryId, OnCategoriesLoaded);

        //if (categoryId == 0)
        //{
        //    Categories = MockData.GetCategories();
        //}
        //else
        //{
        //    Categories = MockData.GetSubCategories(categoryId);
        //}
    }

    private void OnCategoriesLoaded(List<Category> categories)
    {
        Categories = categories;

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
                category.id,
                GameHiddenOptions.Instance.CategoryPrefab,
                this.transform,
                out IPrefabComponent categoryComponent,
                ref _categoriesPool
                );

            categoryComponent.Id = category.id;
            categoryComponent.Route = new Route()
            {
                RoutePath = _actionRoutePath,
                RouteKey = category.id
            };

            (categoryComponent as CategoryComponent).CategoryName.text = category.description;

            if (wasNull)
            {
                _categoriesPool.Add(categoryComponent);
            }
        }
    }

    private CategoryComponent CreateCategoryComponent(Category category)
    {
        var go = UsefullUtils.CreateUiObject(Instantiate(GameHiddenOptions.Instance.CategoryPrefab), category.description, this.transform);
        return go.GetComponent<CategoryComponent>();
    }
}
