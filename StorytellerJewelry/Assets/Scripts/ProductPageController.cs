using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductPageController : MonoBehaviour, IRoute
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Image Picture;
    public Text Name;
    public Text Price;
    public Text Description;
    public Text MaterialDescription;
    public Text ModelDescription;
    public Text MeasureDescription;

    private Route _thisRoute = new Route()
    {
        RoutePath = "Products"
    };

    public void Refresh(Route route)
    {
        if (route.RouteKey == 0)
            route.RouteKey = 1;

        ProductData.Instance.GetProduct(route.RouteKey, (Product product) => {

            Sprite sprite = Resources.Load("ProductImages/" + product.picture_path, typeof(Sprite)) as Sprite;
            Picture.sprite = sprite;

            Name.text = product.name;
            Price.text = product.product_price + " Lei";

            Description.text = product.description;
            MaterialDescription.text = product.material_name + ", " + product.material_price + " Lei";
            ModelDescription.text = product.model_name + ", " + product.model_grams + "g, " + product.model_price + " Lei";
            MeasureDescription.text = "Diameter: " + product.measure_diameter + ", Thickness: " + product.measure_thickness + ", Height: " + product.measure_height + ", Width: " + product.measure_width;
        });
    }

    public Route GetRoute()
    {
        return _thisRoute;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
