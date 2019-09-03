using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDetailsController : MonoBehaviour
{
    private static UserDetailsController _userDetailsController;
    public static UserDetailsController Instance { get { return _userDetailsController; } }

    public Client Client;

    public InputFieldCustom Name;
    public InputFieldCustom Email;
    public InputFieldCustom AddressCity;
    public InputFieldCustom AddressStreet;

    public Text UserDetailsButtonText;

    private bool _isEnabled;

    void Awake()
    {
        _userDetailsController = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isEnabled = false; // so we can make it false
        EnableDisable();
    }

    public void GetUser(int id)
    {
        ClientData.Instance.GetClient(id, (Client client) => {

            if (client == null)
            {
                UserDetailsButtonText.text = "Authentifica-te";
                return;
            }

            Client = client;

            Name.InputField.text = Client.firstname + Client.lastname;
            Email.InputField.text = Client.email;
            AddressCity.InputField.text = Client.address1;
            AddressStreet.InputField.text = Client.address2;
        });

        
    }

    public void EnableDisable()
    {
        _isEnabled = !_isEnabled;
        if (_isEnabled)
        {
            Name.SetEnabled();
            Email.SetEnabled();
            AddressCity.SetEnabled();
            AddressStreet.SetEnabled();

            UserDetailsButtonText.text = "Salveaza";
        }
        else
        {
            Name.SetDisabled();
            Email.SetDisabled();
            AddressCity.SetDisabled();
            AddressStreet.SetDisabled();

            UserDetailsButtonText.text = "Editeaza";
        }
    }
}
