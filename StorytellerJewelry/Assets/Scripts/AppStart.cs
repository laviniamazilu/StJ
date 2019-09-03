using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    public string StartPage = "Home";

    public int id_client;
    public string id_device;

    // Start is called before the first frame update
    void Start()
    {
        id_device = SystemInfo.deviceUniqueIdentifier;
        ClientData.Instance.AuthenticateDevice(id_device, (Client client) =>
        {
            id_client = client.id;

            UserDetailsController.Instance.GetUser(id_client);
        });

        Router.Instance.Init();
        Router.Instance.ChangeRoute(StartPage);
    }


}
