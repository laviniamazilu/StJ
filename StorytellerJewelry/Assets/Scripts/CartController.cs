using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour, IRoute
{
    private Route _thisRoute = new Route()
    {
        RoutePath = "Cart"
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Refresh(Route route)
    {

    }

    public Route GetRoute()
    {
        return _thisRoute;
    }
}
