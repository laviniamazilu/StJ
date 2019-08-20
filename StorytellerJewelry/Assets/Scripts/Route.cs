using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Route : MonoBehaviour, IRoute
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void Refresh()
//    {

//    }
//}

public interface IRoute
{
    void Refresh();

    GameObject GetGameObject();
}
