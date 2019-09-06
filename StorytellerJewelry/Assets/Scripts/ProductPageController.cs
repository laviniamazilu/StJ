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

    public Text test;

    private Route _thisRoute = new Route()
    {
        RoutePath = "Products"
    };

    public void Refresh(Route route)
    {
        if (route.RouteKey == 0)
            route.RouteKey = 1;

        ProductData.Instance.GetProduct(route.RouteKey, (Product product) =>
        {

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

    public void TakePic()
    {
        int maxSize = 512;

        NativeCamera.Permission permission = NativeCamera.TakePicture((string path) => {

            test.text = "Image path: " + path;

            Debug.Log(test.text);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        }, maxSize);

        test.text = "permission: " + permission; 
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
