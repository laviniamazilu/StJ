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

    public delegate void OnRequestCompleteCallback(string message);
    private OnRequestCompleteCallback OnRequestComplete;

    public delegate void OnRecievedClientCallback(Client client);
    private OnRecievedClientCallback OnRecievedClient;

    private string _urlAuthenticateDevice = "/Stj/UserService/AuthenticateDevice.php";
    private string _urlGetClient = "/Stj/UserService/GetClient.php";
    private string _urlLogin = "/Stj/UserService/Login.php";
    private string _urlRegister = "/Stj/UserService/Register.php";

    void Awake()
    {
        _clientData = this;
    }

    public void AuthenticateDevice(string id_device, OnRecievedClientCallback onRecievedClient)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlAuthenticateDevice + "?id_device=" + id_device;
        Debug.Log(_url);
        OnRecievedClient = onRecievedClient;
        StartCoroutine(WaitForClientRequest(_url));
    }

    public void GetClient(int id, OnRecievedClientCallback onRecievedClient)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlGetClient + "?id=" + id;
        Debug.Log(_url);
        OnRecievedClient = onRecievedClient;
        StartCoroutine(WaitForClientRequest(_url));
    }

    public void Login(Client client, OnRequestCompleteCallback onRequestComplete)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlLogin;
        Debug.Log(_url);

        var wwwForm = new WWWForm();
        wwwForm.AddField("client", JsonUtility.ToJson(client));

        OnRequestComplete = onRequestComplete;
        StartCoroutine(WaitForRequest(_url, wwwForm));
    }

    public void RegisterUser(Client client, OnRequestCompleteCallback onRequestComplete)
    {
        var _url = GameHiddenOptions.Instance.ServerURL + _urlRegister;
        Debug.Log(_url);

        var wwwForm = new WWWForm();
        wwwForm.AddField("client", JsonUtility.ToJson(client));

        OnRequestComplete = onRequestComplete;
        StartCoroutine(WaitForRequest(_url, wwwForm));
    }

    private IEnumerator WaitForClientRequest(string _url)
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

    private IEnumerator WaitForRequest(string _url, WWWForm form)
    {
        UnityWebRequest www = UnityWebRequest.Post(_url, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            OnRequestComplete(www.downloadHandler.text);
        }
    }
}
