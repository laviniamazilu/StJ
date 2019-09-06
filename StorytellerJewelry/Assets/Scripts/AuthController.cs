using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour, IRoute
{
    private Route _thisRoute = new Route()
    {
        RoutePath = "Auth"
    };

    private enum AuthStep
    {
        Login,
        Register,
        Edit
    }

    private AuthStep CurrentAuthStep;

    public Text InfoText;

    public GameObject FieldsContainer;
    public GameObject LoginContainer;

    public Text ButtonText;
    public Text ButtonTextIcon;

    private const string _editIcon = "";
    private const string _signInIcon = "";
    private const string _registerIcon = "";

    public InputField FirstNameInput;
    public InputField LastNameInput;
    public InputField EmailInput;
    public InputField PhoneInput;
    public InputField AddressInput;
    public InputField CityInput;
    public InputField CountyInput;
    public InputField PasswordInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Refresh(Route route)
    {
        // user is NOT logged in
        if (route.RouteKey == 0)
        {
            CurrentAuthStep = AuthStep.Login;
        }
        else
        {
            CurrentAuthStep = AuthStep.Edit;
            FirstNameInput.text = UserDetailsController.Instance.Client.firstname;
            LastNameInput.text = UserDetailsController.Instance.Client.lastname;
            EmailInput.text = UserDetailsController.Instance.Client.email;
            PhoneInput.text = UserDetailsController.Instance.Client.phone;
            AddressInput.text = UserDetailsController.Instance.Client.address1;
            CityInput.text = UserDetailsController.Instance.Client.city;
            CountyInput.text = UserDetailsController.Instance.Client.county;
            PasswordInput.text = UserDetailsController.Instance.Client.password;
        }
        ChangeView();
    }

    public void ShowRegisterFields()
    {
        CurrentAuthStep = AuthStep.Register;
        ChangeView();
    }

    public void DoAction()
    {
        switch (CurrentAuthStep)
        {
            case AuthStep.Login:
                Debug.Log("Login");
                break;
            case AuthStep.Register:
                
                CurrentAuthStep = AuthStep.Login;


                break;
            case AuthStep.Edit:
                Debug.Log("Save");
                break;
            default:
                break;
        }
    }

    private void ChangeView()
    {
        switch (CurrentAuthStep)
        {
            case AuthStep.Login:
                FieldsContainer.SetActive(false);
                LoginContainer.SetActive(true);
                ButtonText.text = "Logheaza-te";
                ButtonTextIcon.text = _signInIcon;
                break;
            case AuthStep.Register:
                LoginContainer.SetActive(false);
                FieldsContainer.SetActive(true);
                ButtonText.text = "Inregistreaza-te";
                ButtonTextIcon.text = _registerIcon;
                break;
            case AuthStep.Edit:
                LoginContainer.SetActive(false);
                FieldsContainer.SetActive(true);
                ButtonText.text = "Salveaza";
                ButtonTextIcon.text = _editIcon;
                break;
            default:
                break;
        }
    }

    public Route GetRoute()
    {
        return _thisRoute;
    }
}
