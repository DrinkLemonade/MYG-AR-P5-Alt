using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableDatabase<T> : ScriptableObject //Making this generic allows every database to have its own static list
{
    [SerializeField]
    private List<ScriptableDBEntry> dataList;
    public static List<ScriptableDBEntry> StaticDataList;
    public void InitDatabase()
    {
        //Debug.Log("Creating DB...");
        StaticDataList = new List<ScriptableDBEntry>();
        if (dataList == null) Debug.LogError($"dataList null in?");
        foreach (ScriptableDBEntry entry in dataList)
        {
            StaticDataList.Add(entry);
        }
    }
}

public abstract class ScriptableDBEntry : ScriptableObject
{
 
}