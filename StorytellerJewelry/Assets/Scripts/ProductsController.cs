using Assets.Scripts.Data;
using Assets.Scripts.Utils;
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

    private List<IPrefabComponent> _productsPool;
    private List<Product> _products;

    public void Refresh(Route route)
    {
        //_products = MockData.GetProducts();

        if (_productsPool != null)
        {
            foreach (IPrefabComponent product in _productsPool)
            {
                product.GameObject.SetActive(false);
            }
        }

        ProductData.Instance.GetProducts(route.RouteKey, OnProductsLoaded);
    }

    private void OnProductsLoaded(List<Product> products)
    {
        _products = products;

        foreach (Product product in _products)
        {
            var wasNull = UsefullUtils.CheckInPool(
                product.Id,
                GameHiddenOptions.Instance.ProductPrefab,
                ProductsParent.transform,
                out IPrefabComponent productComponent,
                ref _productsPool
                );

            productComponent.Id = product.Id;
            productComponent.Route = new Route()
            {
                RoutePath = "Product",
                RouteKey = product.Id
            };

            productComponent.GameObject.name = product.Name;
            (productComponent as ProductComponent).Name.text = product.Name;

            Sprite sprite = Resources.Load("ProductImages/" + product.PicturePath, typeof(Sprite)) as Sprite;
            (productComponent as ProductComponent).SetImage(sprite);

            if (wasNull)
            {
                _productsPool.Add(productComponent);
            }
        }

        var newHeight = (_products.Count / 2) * 240;
        ProductsParent.sizeDelta = new Vector2(ProductsParent.sizeDelta.x, newHeight);
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
