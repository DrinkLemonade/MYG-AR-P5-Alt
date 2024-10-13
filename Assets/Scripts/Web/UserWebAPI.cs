using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class UserWebAPI : MonoBehaviour
{
    [SerializeField]
    string userLogin = "http://localhost/tests/userLogin.php",
           userCreate = "http://localhost/tests/userCreate.php";

    [SerializeField]
    ApiRequest request;
    [SerializeField]
    string furnitureInfoRequestName = "";

    [SerializeField]
    LoginUILogic loginUi;
    //[SerializeField]
    //CreateAccountUI createAccountUi;

    string loginAttemptEmail;
    string loginAttemptPassword;

    string createAccountAttemptFullName;
    string createAccountAttemptNickname;
    string createAccountAttemptEmail;
    string createAccountAttemptPassword;

    private string downloadedTextResult;
    enum ApiRequest
    {
        CreateAccount, LogIn, DeleteAccount, GetAccountInformation
    }

    public void LoginPrepareInformation(string email, string password)
    {
        loginAttemptEmail = email;
        loginAttemptPassword = password;
    }

    public void CreateAccountPrepareInformation(string nameFull, string nameWeCallYou, string email, string password)
    {
        createAccountAttemptFullName = nameFull;
        createAccountAttemptNickname = nameWeCallYou;
        createAccountAttemptEmail = email;
        createAccountAttemptPassword = password;
    }

    public void LoginAttempt()
    {
       
        StartCoroutine(LoginAttemptCoroutine());
    }

    public void CreateAccountAttempt()
    {
        StartCoroutine(CreateAccountAttemptCoroutine());
    }

    public IEnumerator LoginAttemptCoroutine()
    {

        //For a login, a SELECT of username, using password_verify to check
        //Arg 1: Password of user being input, Arg 2 password in database
        //Returns whether there's a match

        WWWForm form = new();
        form.AddField("email", loginAttemptEmail);
        form.AddField("password", loginAttemptPassword);

        //form.AddField("email", loginUi.emailField.text);
        //form.AddField("password", loginUi.passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post(userLogin, form);
        yield return www.SendWebRequest();



        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);

        }
        else Debug.Log(www.downloadHandler.text);
    }

    public IEnumerator CreateAccountAttemptCoroutine()
    {

        //For a login, a SELECT of username, using password_verify to check
        //Arg 1: Password of user being input, Arg 2 password in database
        //Returns whether there's a match

        WWWForm form = new();
        form.AddField("nameFull", createAccountAttemptFullName);
        form.AddField("nameWeCallYou", createAccountAttemptNickname);
        form.AddField("email", createAccountAttemptEmail);
        form.AddField("password", createAccountAttemptPassword);

        //form.AddField("email", loginUi.emailField.text);
        //form.AddField("password", loginUi.passwordField.text);

        UnityWebRequest www = UnityWebRequest.Post(userCreate, form);
        yield return www.SendWebRequest();



        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);

        }
        else Debug.Log(www.downloadHandler.text);
    }
    public void MakeRequest()
    {
        StartCoroutine(MakeRequestCoroutine());
    }

    IEnumerator MakeRequestCoroutine()
    {
        switch (request)
        {
            case ApiRequest.CreateAccount:
                {
                      
                    break;
                }
            case ApiRequest.LogIn:
                {
                    
                    break;
                }
            case ApiRequest.DeleteAccount:
                break;
            case ApiRequest.GetAccountInformation:
                break;
            default:
                break;
        }
        yield break;
    }

    IEnumerator SendAPICall(string webString)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{userLogin}?{webString}"))
        {
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Received:\n" + www.downloadHandler.text);
                if (www.downloadHandler.text == "[]")
                {
                    Debug.LogError("SQL request returned nothing!");
                    yield break;
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }

            //Save results so they're not destroyed when we exit this local function
            downloadedTextResult = www.downloadHandler.text;
        } // The using block ensures www.Dispose() is called when this block is exited
        yield break;
    }

    [System.Serializable]
    public class FurnitureData
    {
        public int id;
        public string name;
        public string description;
        public string category;
        public string image;
        public string model;
        public string price;
    }

    public List<ScriptableFurniture> GenerateScriptableFurnitureFromJson(string jsonString)
    {
        List<ScriptableFurniture> newFurniture = new();
        var data = JsonConvert.DeserializeObject<FurnitureData[]>(jsonString);

        for (int i = 0; i < data.Length; i++)
        {
            ScriptableFurniture furniture = ScriptableObject.CreateInstance<ScriptableFurniture>();
            furniture.name = data[i].name; //The actual asset's name
            furniture.entryName = data[i].name;
            furniture.description = data[i].description;
            furniture.SetPriceFromJson(data[i].price);
            furniture.category = DatabaseManager.i.FindCategoryByName(data[i].category);

            newFurniture.Add(furniture);
        }
        return newFurniture;
    }

    public List<ScriptableCategory> GenerateScriptableCategoriesFromJson(string jsonString)
    {
        List<ScriptableCategory> newCategories = new();
        var data = JsonConvert.DeserializeObject<FurnitureData[]>(jsonString);

        for (int i = 0; i < data.Length; i++)
        {
            ScriptableCategory cat = ScriptableObject.CreateInstance<ScriptableCategory>();
            cat.name = data[i].name; //The actual asset's name
            cat.entryName = data[i].name;
            cat.description = data[i].description;

            newCategories.Add(cat);
        }
        return newCategories;
    }

}
