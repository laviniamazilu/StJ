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
    public Text AfterLoginText;
    public InputField LoginEmailInput;
    public InputField LoginPhonePassword;

    public GameObject FieldsContainer;
    public GameObject LoginContainer;

    public Text ButtonText;
    public Text ButtonTextIcon;

    private const string _editIcon = "";
    private const string _signInIcon = "";
    private const string _registerIcon = "";

    public Text TextEroare;

    public InputField FirstNameInput;
    public InputField LastNameInput;
    public InputField EmailInput;
    public InputField PhoneInput;
    public InputField AddressInput;
    public InputField CityInput;
    public InputField CountyInput;
    public InputField PasswordInput;

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
        Client clientToSave = null;

        switch (CurrentAuthStep)
        {
            case AuthStep.Login:

                if (string.IsNullOrEmpty(LoginEmailInput.text) || string.IsNullOrEmpty(LoginEmailInput.text))
                {
                    return;
                }

                var clientToLogin = new Client()
                {
                    id = UserDetailsController.Instance.ID_CLIENT,
                    email = LoginEmailInput.text,
                    password = LoginPhonePassword.text
                };
                ClientData.Instance.Login(clientToLogin, (string message) =>
                {
                    switch (message)
                    {
                        case "NO_EXISTS":
                        case "FAILED":
                            AfterLoginText.text = "Can't login with this combination of Email and Password.";
                            break;
                        case "SUCCESS":
                            AfterLoginText.text = "";
                            AppStart.Instance.IS_LOGGED_IN = true;
                            AppStart.Instance.WriteString("LOGGED_IN");
                            UserDetailsController.Instance.GetUser(UserDetailsController.Instance.ID_CLIENT);
                            Router.Instance.ChangeRoute("Home");
                            break;
                        default: break;
                    }
                });
                break;
            case AuthStep.Register:

                if (
                    string.IsNullOrEmpty(FirstNameInput.text)
                    || string.IsNullOrEmpty(LastNameInput.text)
                    || string.IsNullOrEmpty(EmailInput.text)
                    || string.IsNullOrEmpty(PhoneInput.text)
                    || string.IsNullOrEmpty(AddressInput.text)
                    || string.IsNullOrEmpty(CityInput.text)
                    || string.IsNullOrEmpty(CountyInput.text)
                    || string.IsNullOrEmpty(PasswordInput.text)
                )
                {
                    TextEroare.text = "Nu ai completat toate campurile!";
                    return;
                }
                TextEroare.text = "";

                clientToSave = new Client()
                {
                    id = UserDetailsController.Instance.ID_CLIENT,
                    firstname = FirstNameInput.text,
                    lastname = LastNameInput.text,
                    email = EmailInput.text,
                    phone = PhoneInput.text,
                    address1 = AddressInput.text,
                    city = CityInput.text,
                    county = CountyInput.text,
                    password = PasswordInput.text,
                };

                ClientData.Instance.RegisterUser(clientToSave, (string message) =>
                {
                    CurrentAuthStep = AuthStep.Login;
                    ChangeView();
                });

                break;
            case AuthStep.Edit:

                if (
                    string.IsNullOrEmpty(FirstNameInput.text)
                    || string.IsNullOrEmpty(LastNameInput.text)
                    || string.IsNullOrEmpty(EmailInput.text)
                    || string.IsNullOrEmpty(PhoneInput.text)
                    || string.IsNullOrEmpty(AddressInput.text)
                    || string.IsNullOrEmpty(CityInput.text)
                    || string.IsNullOrEmpty(CountyInput.text)
                    || string.IsNullOrEmpty(PasswordInput.text)
                )
                {
                    TextEroare.text = "Nu ai completat toate campurile!";
                    return;
                }
                TextEroare.text = "";

                clientToSave = new Client()
                {
                    id = UserDetailsController.Instance.ID_CLIENT,
                    firstname = FirstNameInput.text,
                    lastname = LastNameInput.text,
                    email = EmailInput.text,
                    phone = PhoneInput.text,
                    address1 = AddressInput.text,
                    city = CityInput.text,
                    county = CountyInput.text,
                    password = PasswordInput.text,
                };

                ClientData.Instance.RegisterUser(clientToSave, (string message) =>
                {
                    Router.Instance.ChangeRoute("Home");
                });

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
