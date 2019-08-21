using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceEvents : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        // Android backbutton
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Router.Instance.GoBack();
        }
    }
}
