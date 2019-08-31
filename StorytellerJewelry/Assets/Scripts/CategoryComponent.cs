using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryComponent : MonoBehaviour, IPrefabComponent
{
    public int Id { get; set; }
    public Route Route { get; set; }
    public Text CategoryName;

    public void OnClick()
    {
        Router.Instance.ChangeRouteInternal(Route);
    }

    public GameObject GameObject { get { return this.gameObject; } }
}
