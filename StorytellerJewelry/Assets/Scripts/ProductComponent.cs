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

    public void SetImage(Sprite sprite)
    {
        Image.sprite = sprite;
    }
}
