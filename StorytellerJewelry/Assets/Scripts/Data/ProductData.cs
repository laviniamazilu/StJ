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

    public delegate void OnRecievedProductCallback(Product product);
    private OnRecievedProductCallback OnRecievedProduct;

    private string _urlGetProducts = "/Stj/ProductService/GetProducts.php";
    private string _urlGetProduct = "/Stj/ProductService/GetProduct.php";

    void Awake()
    {
        _productData = this;
    }

    public void GetProducts(int categoryId, int subCategoryId, OnRecievedProductsCallback onRecievedProducts)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetProducts + "?id_category=" + categoryId + "&id_subcategory=" + subCategoryId;
        Debug.Log(_url);
        OnRecievedProducts = onRecievedProducts;
        StartCoroutine(WaitForProductsRequest(_url));
    }

    public void GetProduct(int productId, OnRecievedProductCallback onRecievedProduct)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetProduct + "?id_product=" + productId;
        Debug.Log(_url);
        OnRecievedProduct = onRecievedProduct;
        StartCoroutine(WaitForRequest(_url));
    }

    private IEnumerator WaitForProductsRequest(string _url)
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

            Product products = JsonUtility.FromJson<Product>(www.downloadHandler.text);
            OnRecievedProduct(products);
        }
    }
}
