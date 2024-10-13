using DG.Tweening;
using System;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountUI : MonoBehaviour
{
    [SerializeField]
    UserWebAPI userApi;

    //UI
    [SerializeField]
    TMP_InputField nameField, nicknameField, emailField, passwordField;
    [SerializeField]
    Button buttonValidate;
    [SerializeField]
    TextMeshProUGUI emailWarning, passwordWarning;

    [SerializeField]
    GameObject accountCreatedPopup;

    //Validating the login credentials and moving the window afterwards
    bool validated = false;
    float validateSlide = 0;
    [SerializeField]
    Ease easeType;
    [SerializeField]
    AnimationCurve easeCurve;
    [SerializeField]
    float easeTimeSeconds = 2f;
    [SerializeField]
    bool useCurveInsteadOfEase = false;

    //Shown in text fields when they are empty.
    [SerializeField]
    string shownIfEmpty = "Overwrite this value in editor.";

    //Button clicking audio
    [SerializeField]
    AudioClip onClicked;

    void Start()
    {
        //Hide warnings
        emailWarning.enabled = false;
        passwordWarning.enabled = false;

        //Email
        //SetEmptyDefaultText(emailField, true);

        //Password
        //SetEmptyDefaultText(passwordField, true);
    }

    void SetEmptyDefaultText(TMP_InputField field, bool active)
    {
        //If true: the chosen text field will display the string it must display when nothing has been entered.
        //If false, display nothing.
        Debug.Log($"running SetEmpty for {field.name}, active: {active}");
        //field.text.style.unityFontStyleAndWeight = active ? FontStyle.Italic : FontStyle.Normal;
        field.SetTextWithoutNotify(active ? shownIfEmpty : "");
    }

    public void CreateAccountButtonClick()
    {
        if (validated) return;

        Debug.Log($"button clicked! Contents are: {emailField.text}, {passwordField.text}");

        //Is the email field non-empty?
        bool okMail = emailField.text != "";
        //If yes, we can check if it's valid. (Checking an empty string would cause an exception.)
        if (okMail) okMail = EmailIsValid(emailField.text);
        //Is the password field non-empty, and also NOT the default "enter your password" text?
        bool okPassword = passwordField.text != "" && passwordField.text != shownIfEmpty;

        //Show the error message labels if needed.
        emailWarning.enabled = !okMail;
        passwordWarning.enabled = !okPassword;

        //If the email and password are valid, the login goes through.
        if (okMail && okPassword) ValidateCreateAccount();
    }

    void ValidateCreateAccount()
    {
        userApi.CreateAccountPrepareInformation(nameField.text, nicknameField.text, emailField.text, passwordField.text);
        userApi.CreateAccountAttempt();

        return;
        //Not used currently
        //Do we have a custom animation curve set up in the inspector? If yes, use it with DOTween to slide offscreen (-110% from default position).
        if (easeCurve != null && useCurveInsteadOfEase) DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeCurve);
        //If not, use the ease type selected in the inspector dropdown.
        else DOTween.To(() => validateSlide, x => validateSlide = x, -110, easeTimeSeconds).SetEase(easeType);
    }

    private void Update()
    {
        //Adjust position if we're making the window slide off-screen.
        //loginWindow.style.top = Length.Percent(validateSlide);
    }

    public bool EmailIsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}