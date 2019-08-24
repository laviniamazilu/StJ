using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsController : MonoBehaviour, IRoute
{
    private Route _thisRoute = new Route()
    {
        RoutePath = "Products"
    };

    public RectTransform ProductsParent;

    public void Refresh(Route route)
    {
        
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
