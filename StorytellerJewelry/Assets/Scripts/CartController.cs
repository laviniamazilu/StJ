using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour, IRoute
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Refresh()
    {
        throw new System.NotImplementedException();
    }
}
