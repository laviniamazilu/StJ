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
        if (_productsPool != null)
        {
            foreach (IPrefabComponent product in _productsPool)
            {
                product.GameObject.SetActive(false);
            }
        }

        var categoryId = route.ExtraRouteKeys != null ? route.ExtraRouteKeys["categoryId"] : 0;
        var subCategoryId = route.RouteKey;

        ProductData.Instance.GetProducts(categoryId, subCategoryId, OnProductsLoaded);
    }

    private void OnProductsLoaded(List<Product> products)
    {
        _products = products;

        foreach (Product product in _products)
        {
            var wasNull = UsefullUtils.CheckInPool(
                product.id,
                GameHiddenOptions.Instance.ProductPrefab,
                ProductsParent.transform,
                out IPrefabComponent productComponent,
                ref _productsPool
                );

            productComponent.Id = product.id;
            productComponent.Route = new Route()
            {
                RoutePath = "Product",
                RouteKey = product.id
            };

            productComponent.GameObject.name = product.name;
            (productComponent as ProductComponent).Name.text = product.name;
            (productComponent as ProductComponent).PriceOld.text = product.product_price.ToString() + " Lei";
            var randomNr = ((int)Random.Range(1, 4)) * 10;
            var oldPrice = UsefullUtils.GetPercent(product.product_price, 100 - randomNr);
            (productComponent as ProductComponent).ReducePercent.text = "- " + randomNr.ToString() + " %";
            (productComponent as ProductComponent).Price.text = oldPrice + " Lei";

            Sprite sprite = Resources.Load("ProductImages/" + product.picture_path, typeof(Sprite)) as Sprite;
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
