using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubCategoriesController : MonoBehaviour, IRoute
{
    private Route _thisRoute = new Route()
    {
        RoutePath = "SubCategories"
    };

    public CategoriesController CategoriesController;

    public void Refresh(Route route)
    {
        _thisRoute = route;

        CategoriesController.PopulateCategories(_thisRoute.RouteKey, "Products");

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
