using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Assets.Scripts.Utils;
using System.Linq;

public class CartData : MonoBehaviour
{
    private static CartData _productData;
    public static CartData Instance
    {
        get { return _productData; }
    }

    public delegate void OnRequestCompleteCallback();
    private OnRequestCompleteCallback OnRequestComplete;

    public delegate void OnGetCurrentCartCallback(List<CartProduct> cartProducts);
    private OnGetCurrentCartCallback OnGetCurrentCart;

    private string _urlPostCartProduct = "/Stj/CartService/SaveProductInCart.php";
    private string _urlGetCartProducts = "/Stj/CartService/GetCartInProgress.php";
    private string _urlPostChangeQuantity = "/Stj/CartService/ChangeQuantity.php";
    private string _urlPostDoComanda = "/Stj/CartService/Comanda.php";

    void Awake()
    {
        _productData = this;
    }

    public void SaveProductToCart(ProductOptions productOptions, OnRequestCompleteCallback onRequestComplete)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlPostCartProduct;
        Debug.Log(_url);

        byte[] pictureBytes = null;
        bool hasBytes = false;
        if (productOptions.picture_bytes != null)
        {
            hasBytes = true;
            pictureBytes = productOptions.picture_bytes;
            productOptions.picture_bytes = null;
        }

        var wwwForm = new WWWForm();
        wwwForm.AddField("data", JsonUtility.ToJson(productOptions));

        if (hasBytes)
        {
            wwwForm.AddBinaryData("picture_bytes", pictureBytes);
        }

        OnRequestComplete = onRequestComplete;
        StartCoroutine(WaitForRequest(_url, wwwForm));
    }

    public void GetCurrentCartProducts(int id_client, OnGetCurrentCartCallback onGetCurrentCart)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetCartProducts + "?id_client=" + id_client;
        Debug.Log(_url);

        OnGetCurrentCart = onGetCurrentCart;
        StartCoroutine(WaitForCartRequest(_url));
    }

    public void ChangeQuantity(int id_product_options, bool increase, int id_client, OnGetCurrentCartCallback onGetCurrentCart)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlPostChangeQuantity + "?id_client=" + id_client;
        Debug.Log(_url);

        var wwwForm = new WWWForm();
        wwwForm.AddField("id_product_options", id_product_options);
        wwwForm.AddField("increase", increase.ToString());

        OnGetCurrentCart = onGetCurrentCart;
        StartCoroutine(WaitForChangeQuantityRequest(id_client, _url, wwwForm));
    }

    public void Comanda(Client client, OnRequestCompleteCallback onRequestComplete)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlPostDoComanda;
        Debug.Log(_url);

        var wwwForm = new WWWForm();
        wwwForm.AddField("client", JsonUtility.ToJson(client));

        OnRequestComplete = onRequestComplete;
        StartCoroutine(WaitForComandaRequest(_url, wwwForm));
    }

    private IEnumerator WaitForRequest(string _url, WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(_url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            OnRequestComplete();
        }
    }

    private IEnumerator WaitForCartRequest(string _url)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            CartProduct[] cartProducts = JsonHelper.FromJson<CartProduct>(www.downloadHandler.text);
            OnGetCurrentCart(cartProducts.ToList());
        }
    }

    private IEnumerator WaitForChangeQuantityRequest(int id_client, string _url, WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(_url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            GetCurrentCartProducts(id_client, OnGetCurrentCart);
        }
    }

    private IEnumerator WaitForComandaRequest(string _url, WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(_url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            OnRequestComplete();
        }
    }
}
