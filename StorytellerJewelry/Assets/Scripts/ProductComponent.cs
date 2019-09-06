using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductComponent : MonoBehaviour, IPrefabComponent
{
    public int Id { get; set; }
    public Route Route { get; set; }

    public GameObject GameObject { get { return this.gameObject; } }

    public Text Name;
    public Image Image;
    public Text Price;
    public Text PriceOld;
    public Text ReducePercent;

    public void SetImage(Sprite sprite)
    {
        Image.sprite = sprite;
    }

    public void GoToProduct()
    {
        Router.Instance.ChangeRouteInternal(new Route() { RoutePath = "Product", RouteKey = Id });
    }
}
