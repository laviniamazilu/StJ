using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartProductComponent : MonoBehaviour, IPrefabComponent
{
    private int _id;
    public int Id { get { return _id; } set { _id = value; } }
    public Route _route;
    public Route Route { get { return _route; } set { _route = value; } }

    public GameObject GameObject { get { return this.gameObject; } }

    public delegate void ChangeQuantityCallback(int id_product_options, bool increase);
    public ChangeQuantityCallback OnChangeQuantity;

    public Image Image;
    public Text Name;
    public Text Description;
    public Text Quantity;
    public Text PriceTotal;

    public void SetImage(Sprite sprite)
    {
        Image.sprite = sprite;
    }

    public void ChangeQuantity(bool increase)
    {
        OnChangeQuantity(Id, increase);
    }
}
