using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour, IRoute
{
    private Route _thisRoute = new Route()
    {
        RoutePath = "Home"
    };

    public CategoriesController CategoriesController;

    public void Refresh(Route route)
    {
        CategoriesController.PopulateCategories();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Route GetRoute()
    {
        return _thisRoute;
    }
}
