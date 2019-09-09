using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartController : MonoBehaviour, IRoute
{
    public RectTransform CartProductsParent;

    public Text TotalPrice;

    public Text ButtonText;

    private List<CartProduct> _cartProducts;
    private List<IPrefabComponent> _cartProductsPool;

    private Route _thisRoute = new Route()
    {
        RoutePath = "Cart"
    };

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Refresh(Route route)
    {
        ChangeButtonText();
        CartData.Instance.GetCurrentCartProducts(UserDetailsController.Instance.ID_CLIENT, onCartProductsLoaded);
    }

    private void ChangeButtonText()
    {
        if (UserDetailsController.Instance.Client == null)
        {
            ButtonText.text = "Autentifica-te";
        }
        else
        {
            ButtonText.text = "Comanda";
        }
    }

    public void onCartProductsLoaded(List<CartProduct> cartProducts)
    {
        _cartProducts = cartProducts;

        if (_cartProductsPool != null)
        {
            foreach (var item in _cartProductsPool)
            {
                item.GameObject.SetActive(false);
            }
        }

        float totalPrice = 0;

        foreach (CartProduct cartProduct in _cartProducts)
        {
            var wasNull = UsefullUtils.CheckInPool(
                cartProduct.id_product_options,
                GameHiddenOptions.Instance.CartProductPrefab,
                CartProductsParent.transform,
                out IPrefabComponent cartProductComponent,
                ref _cartProductsPool
                );

            cartProductComponent.Id = cartProduct.id_product_options;
            cartProductComponent.Route = new Route();

            cartProductComponent.GameObject.name = cartProduct.product_name;
            (cartProductComponent as CartProductComponent).Name.text = cartProduct.product_name;
            (cartProductComponent as CartProductComponent).Description.text = GetComandaDescription(cartProduct);
            (cartProductComponent as CartProductComponent).Quantity.text = cartProduct.quantity.ToString();
            (cartProductComponent as CartProductComponent).PriceTotal.text = cartProduct.total_price.ToString() + " Lei";

            totalPrice += cartProduct.total_price;

            (cartProductComponent as CartProductComponent).OnChangeQuantity = OnChangeQuantity;

            Sprite sprite = Resources.Load("ProductImages/" + cartProduct.picture_path, typeof(Sprite)) as Sprite;
            (cartProductComponent as CartProductComponent).SetImage(sprite);

            if (wasNull)
            {
                _cartProductsPool.Add(cartProductComponent);
            }
        }

        var newHeight = (_cartProducts.Count) * 100;
        CartProductsParent.sizeDelta = new Vector2(CartProductsParent.sizeDelta.x, newHeight);

        TotalPrice.text = totalPrice.ToString();
    }

    private string GetComandaDescription(CartProduct cartProduct)
    {
        string returnValue = string.Empty;
        if (string.IsNullOrEmpty(cartProduct.option_file) == false)
        {
            returnValue += "Fisier: " + cartProduct.option_file;
        }
        else if (string.IsNullOrEmpty(cartProduct.option_text) == false)
        {
            returnValue += "Text: " + cartProduct.option_text;
        }
        if (string.IsNullOrEmpty(cartProduct.option_text_verso) == false)
        {
            returnValue += "\n Verso: " + cartProduct.option_text_verso;
        }
        if (string.IsNullOrEmpty(cartProduct.option_name) == false)
        {
            returnValue += "Nume: " + cartProduct.option_name;
        }
        return returnValue;
    }

    public void Comanda()
    {
        if (UserDetailsController.Instance.Client == null)
        {
            Router.Instance.ChangeRoute("Auth");
            return;
        }

        CartData.Instance.Comanda(UserDetailsController.Instance.Client, () =>
        {
            Refresh(null);
        });
    }

    public void OnChangeQuantity(int id_product_options, bool increase)
    {
        Debug.Log(increase == true ? "+1" : "-1");
        CartData.Instance.ChangeQuantity(id_product_options, increase, UserDetailsController.Instance.ID_CLIENT, onCartProductsLoaded);
    }

    public Route GetRoute()
    {
        return _thisRoute;
    }
}
