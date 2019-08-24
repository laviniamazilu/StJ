using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountController : MonoBehaviour
{
    public CategoriesController CategoriesController;

    // Start is called before the first frame update
    void Start()
    {
        CategoriesController.PopulateCategories();
        CategoriesController.gameObject.GetComponent<Style>().ApplyStyle(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
