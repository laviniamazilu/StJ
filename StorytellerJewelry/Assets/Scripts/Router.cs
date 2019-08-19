using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    public GameObject Homepage;
    public GameObject Categorypage;
    public GameObject Drawer;

    // Start is called before the first frame update
    void Start()
    {
        Drawer.SetActive(true);
        //Homepage.SetActive(false);
        //Categorypage.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
