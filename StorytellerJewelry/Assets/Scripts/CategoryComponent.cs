using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryComponent : MonoBehaviour
{
    public int Id;
    public Route Route;
    public Text CategoryName;

    public void OnClick()
    {
        Router.Instance.ChangeRouteInternal(Route);
    }
}
