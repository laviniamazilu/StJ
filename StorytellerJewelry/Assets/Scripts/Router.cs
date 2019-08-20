using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    private static Router _instance;
    public static Router Instance
    {
        get { return _instance; }
    }

    public HomeController HomePage;
    public SubCategoriesController SubCategoriesPage;
    public CartController CartPage;
    public Drawer Drawer;

    private Dictionary<string, IRoute> _routes;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Drawer.gameObject.SetActive(true);

        _routes = new Dictionary<string, IRoute>()
        {
            { "Home", (HomePage as IRoute) },
            { "SubCategories", (SubCategoriesPage as IRoute) },
            { "Cart", (CartPage as IRoute) }
        };

        ChangeRoute("Home");
    }

    public void ChangeRoute(string routeName)
    {
        foreach (KeyValuePair<string, IRoute> route in _routes)
        {
            if (route.Key == routeName)
            {
                route.Value.GetGameObject().SetActive(true);
                route.Value.Refresh();
            }
            else
            {
                route.Value.GetGameObject().SetActive(false);
            }
        }
    }
}
