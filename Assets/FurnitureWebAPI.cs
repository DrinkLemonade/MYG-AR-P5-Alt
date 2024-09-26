using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class FurnitureWebAPI : MonoBehaviour
{
    [SerializeField]
    string apiPhpUrl = "http://localhost/tests/test.php";

    public List<ScriptableFurniture> downloadedFurniture;
    public List<ScriptableCategory> downloadedCategories;

    [SerializeField]
    ApiRequest request;
    [SerializeField]
    string furnitureInfoRequestName = "";

    private string downloadedTextResult;
    enum ApiRequest
    {
        MakeFurnitureScriptables, MakeCategoryScriptables, ListFurniture, ListCategories, FurnitureInfo
    }

    public void MakeRequest()
    {
        StartCoroutine(MakeRequestCoroutine());
    }

    IEnumerator MakeRequestCoroutine()
    {
        switch (request)
        {
            //Make scriptables from SQL DB
            case ApiRequest.MakeCategoryScriptables:
                {
                    yield return DownloadJsonTextWithWebRequest("ListCategories");
                    //Listed categories are added to this API as a WIP way to visualize it.
                    List<ScriptableCategory> results = GenerateScriptableCategoriesFromJson(downloadedTextResult);
                    downloadedCategories.AddRange(results);
                    CategoryDatabase.StaticDataList.AddRange(results);
                    break;
                }
            case ApiRequest.MakeFurnitureScriptables:
                {
                    yield return DownloadJsonTextWithWebRequest("ListFurniture");
                    //Listed furniture is added to this API as a WIP way to visualize it.
                    List<ScriptableFurniture> results = GenerateScriptableFurnitureFromJson(downloadedTextResult);
                    downloadedFurniture.AddRange(results);
                    FurnitureDatabase.StaticDataList.AddRange(results);
                    break;
                }
            //List stuff in SQL DB
            case ApiRequest.ListFurniture:
                yield return DownloadJsonTextWithWebRequest("ListFurniture");
                break;
            case ApiRequest.ListCategories:
                yield return DownloadJsonTextWithWebRequest("ListCategories");
                break;

            //Display info for one piece of furniture in SQL DB 
            case ApiRequest.FurnitureInfo:
                if (furnitureInfoRequestName == "")
                {
                    Debug.LogError("Didn't provide a name for the requested furniture.");
                    break;
                }
                furnitureInfoRequestName.Replace(" ", "+");
                yield return DownloadJsonTextWithWebRequest($"FurnitureInfo={furnitureInfoRequestName}");
                break;
            default:
                break;
        }


        IEnumerator DownloadJsonTextWithWebRequest(string webString)
        {
            using (UnityWebRequest www = UnityWebRequest.Get($"{apiPhpUrl}?{webString}"))
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
