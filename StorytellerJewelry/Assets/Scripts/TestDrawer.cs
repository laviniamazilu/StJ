using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDrawer : MonoBehaviour
{
    public Text text;
    public GameObject Drawer;
    public RectTransform Content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "width: " + Drawer.GetComponent<RectTransform>().sizeDelta.x +
            "\nheight: " + Drawer.GetComponent<RectTransform>().sizeDelta.y +
            "\nwidth: " + Content.sizeDelta.x +
            "\nheight: " + Content.sizeDelta.y +
             "\nscreen width: " + Screen.width +
            "\nscreen height: " + Screen.height +
            "\nposition: " + Drawer.GetComponent<RectTransform>().localPosition.ToString();

    }
}
