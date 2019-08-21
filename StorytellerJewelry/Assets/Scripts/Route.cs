using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoute
{
    void Refresh(Route route);

    GameObject GetGameObject();

    Route GetRoute();
}

public class Route
{
    public string RoutePath;
    public int RouteKey = 0;
    public string RouteExtras;
}
