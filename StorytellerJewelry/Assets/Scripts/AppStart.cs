using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AppStart : MonoBehaviour
{
    private static AppStart _appStart;
    public static AppStart Instance
    {
        get { return _appStart; }
    }

    private void Awake()
    {
        _appStart = this;
    }

    public TextAsset LocalSettings;

    public string StartPage = "Home";

    public bool IS_LOGGED_IN;

    public int id_client;
    public string id_device;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        ReadString();

        id_device = SystemInfo.deviceUniqueIdentifier;
        ClientData.Instance.AuthenticateDevice(id_device, (Client client) =>
        {
            id_client = client.id;

            UserDetailsController.Instance.GetUser(id_client);
            Router.Instance.ChangeRoute(StartPage);
        });

        Router.Instance.Init();
    }

    public void WriteString(string setting)
    {
        string path = UsefullUtils.GetPathToStreamingAssetsFile("Settings.txt");
        File.WriteAllText(path, setting);
    }

    public void ReadString()
    {
        string path = UsefullUtils.GetPathToStreamingAssetsFile("Settings.txt");

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        IS_LOGGED_IN = reader.ReadToEnd() == "LOGGED_IN";
        reader.Close();
    }
}
