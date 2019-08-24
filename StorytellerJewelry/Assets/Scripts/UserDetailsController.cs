using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDetailsController : MonoBehaviour
{
    public User User;

    public InputFieldCustom Name;
    public InputFieldCustom Email;
    public InputFieldCustom AddressCity;
    public InputFieldCustom AddressStreet;

    public Text UserDetailsButtonText;

    private bool _isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        GetUser();
        _isEnabled = true; // so we can make it false
        EnableDisable();
    }

    private void GetUser()
    {
        User = MockData.GetUser();

        Name.InputField.text = User.Name;
        Email.InputField.text = User.Email;
        AddressCity.InputField.text = User.Address;
        AddressStreet.InputField.text = User.AddressStreet;
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
