using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using Assets.Scripts.Utils;
using System.Linq;

public class CategoryData : MonoBehaviour
{
    private static CategoryData _categoryData;
    public static CategoryData Instance
    {
        get { return _categoryData; }
    }

    public delegate void OnRecievedCategoriesCallback(List<Category> categories);
    private OnRecievedCategoriesCallback OnRecievedCategories;

    private string _urlGetCategories = "/Stj/CategoryService/GetCategories.php";

    void Awake()
    {
        _categoryData = this;
    }

    public void GetCategories(int categoryId, OnRecievedCategoriesCallback onRecievedCategories)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetCategories + "?categoryId=" + categoryId;
        OnRecievedCategories = onRecievedCategories;
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
            if (OnRecievedCategories != null)
            {
                Debug.Log(www.downloadHandler.text);

                Category[] categories = JsonHelper.FromJson<Category>(www.downloadHandler.text);
                OnRecievedCategories(categories.ToList());
                OnRecievedCategories = null;
            }
        }
    }
}
