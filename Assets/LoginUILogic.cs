using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUILogic : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField emailField, passwordField;
    [SerializeField]
    TextMeshProUGUI incorrectCredentialsWarning;

    
    [SerializeField]
    UserWebAPI webApi;

    
    public void LoginAttempt()
    {
        webApi.LoginPrepareInformation(emailField.text, passwordField.text);
        webApi.LoginAttempt();
    }
    
}
