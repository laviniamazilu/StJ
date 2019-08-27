using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using System.Linq;

public class ProductData : MonoBehaviour
{
    private static ProductData _productData;
    public static ProductData Instance
    {
        get { return _productData; }
    }

    public delegate void OnRecievedProductsCallback(List<Product> products);
    private OnRecievedProductsCallback OnRecievedProducts;

    private string _urlGetProducts = "/Stj/ProductService/GetProducts.php";

    void Awake()
    {
        _productData = this;
    }

    public void GetProducts(int subCategoryId, OnRecievedProductsCallback onRecievedProducts)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetProducts + "?subCategoryId=" + subCategoryId;
        OnRecievedProducts = onRecievedProducts;
        StartCoroutine(WaitForRequest(_url));
    }

    private IEnumerator WaitForRequest(string _url)
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

            Product[] products2 = JsonHelper.FromJson<Product>(www.downloadHandler.text);
            OnRecievedProducts(products2.ToList());
        }
    }
}
