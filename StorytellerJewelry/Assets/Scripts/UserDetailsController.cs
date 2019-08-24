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
    public InputFieldCustom Address;

    // Start is called before the first frame update
    void Start()
    {
        GetUser();
    }

    private void GetUser()
    {
        User = MockData.GetUser();

        Name.InputField.text = User.Name;
        Email.InputField.text = User.Email;
        Address.InputField.text = User.Address;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
