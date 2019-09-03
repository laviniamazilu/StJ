using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientData : MonoBehaviour
{
    private static ClientData _clientData;
    public static ClientData Instance
    {
        get { return _clientData; }
    }

    public delegate void OnRecievedClientCallback(Client client);
    private OnRecievedClientCallback OnRecievedClient;

    private string _urlAuthenticateDevice = "/Stj/UserService/AuthenticateDevice.php";
    private string _urlGetClient = "/Stj/UserService/GetClient.php";

    void Awake()
    {
        _clientData = this;
    }

    public void AuthenticateDevice(string id_device, OnRecievedClientCallback onRecievedClient)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlAuthenticateDevice + "?id_device=" + id_device;
        Debug.Log(_url);
        OnRecievedClient = onRecievedClient;
        StartCoroutine(WaitForRequest(_url));
    }

    public void GetClient(int id, OnRecievedClientCallback onRecievedClient)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetClient + "?id=" + id;
        Debug.Log(_url);
        OnRecievedClient = onRecievedClient;
        StartCoroutine(WaitForRequest(_url));
    }

    private IEnumerator WaitForRequest(string _url)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            Client client = JsonUtility.FromJson<Client>(www.downloadHandler.text);
            OnRecievedClient(client);
        }
    }
}
