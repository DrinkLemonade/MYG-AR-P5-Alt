using Gameplay;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class PopulateContentList : MonoBehaviour
{
    [SerializeField]
    CategoryDatabase catDb;
    [SerializeField]
    FurnitureDatabase furnDb;

    [SerializeField]
    int populateAmount = 3;

    ScriptableDatabase<ScriptableDBEntry> database;
    
    [SerializeField]
    GameObject optionHolder, scriptableEntryButtonPrefab;

    //Normally I'd like to do this in a generic, reusable way, but I'm short on time.
    //TODO fix
    //ScriptableDatabase<ScriptableDBEntry> database;

    [SerializeField]
    PopulateCustom customPopulateLogic = PopulateCustom.No;
    enum PopulateCustom
    {
        No, Recommendations, Favorites, Random
    }
    void Awake()
    {
        if (furnDb != null)
        {
            List<ScriptableDBEntry> list = new(FurnitureDatabase.StaticDataList);
            if (customPopulateLogic == PopulateCustom.Random) list.Shuffle();

            int i = 0;
            foreach (var item in list)
            {
                var furn = (ScriptableFurniture)item;
                var go = Instantiate(scriptableEntryButtonPrefab, optionHolder.transform);
                var logic = go.GetComponent<ScriptableEntryButton>();
                logic.entry = furn;
                logic.Init();
                Debug.Log(furn.entryName);
                i++;
                if (i == populateAmount) break;
            }
        }
        else if (catDb != null)
        {
            foreach (var item in CategoryDatabase.StaticDataList)
            {
                var cat = (ScriptableCategory)item;
                var go = Instantiate(scriptableEntryButtonPrefab, optionHolder.transform);
                var logic = go.GetComponent<ScriptableEntryButton>();
                logic.entry = cat;
                logic.Init();
                Debug.Log(cat.entryName);
            }

        }
        else Debug.LogError("Forgot to assign a DB");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
