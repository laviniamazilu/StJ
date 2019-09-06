using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public ProductsController Products;
    public ProductPageController ProductPageController;
    public AuthController Auth;
    public Drawer DrawerInstance;

    private Dictionary<string, IRoute> _routes;

    public Route PreviousRoute;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init() {

        DrawerInstance.gameObject.SetActive(true);
        ((CompileStyles)FindObjectOfType(typeof(CompileStyles))).Init(true);

        _routes = new Dictionary<string, IRoute>()
        {
            { "Home", (HomePage as IRoute) },
            { "SubCategories", (SubCategoriesPage as IRoute) },
            { "Cart", (CartPage as IRoute) },
            { "Auth", (Auth as IRoute) },
            { "Products", (Products as IRoute) },
            { "Product", (ProductPageController as IRoute) }
        };
    }

    public void ChangeRoute(string routeName)
    {
        ChangeRouteInternal(new Route() { RoutePath = routeName });
    }

    public void ChangeRouteInternal(Route routePath)
    {
        StoreBackRoute();
        Drawer.Instance.CloseDrawer();

        foreach (KeyValuePair<string, IRoute> route in _routes)
        {
            if (route.Key == routePath.RoutePath)
            {
                route.Value.GetGameObject().SetActive(true);
                route.Value.Refresh(routePath);
            }
            else
            {
                route.Value.GetGameObject().SetActive(false);
            }
        }
    }

    private void StoreBackRoute()
    {
        var previousRoute = _routes.FirstOrDefault(r => r.Value.GetGameObject().activeSelf == true);
        //if (previousRoute)
        //{
            PreviousRoute = previousRoute.Value?.GetRoute();
        //}
    }

    public void GoBack()
    {
        ChangeRouteInternal(PreviousRoute);
    }
}
