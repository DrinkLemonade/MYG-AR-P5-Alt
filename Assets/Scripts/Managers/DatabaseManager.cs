using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager i;

    [SerializeField] CategoryDatabase categoryDatabase;
    [SerializeField] FurnitureDatabase furnitureDatabase;
    void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);
        i = this;

        categoryDatabase.InitDatabase();
        furnitureDatabase.InitDatabase();
    }

    public ScriptableCategory FindCategoryByName(string name)
    {
        //TODO: Use a database of databases so we can generalize this function to any database?
        //Safely cast
        var results = (ScriptableCategory)CategoryDatabase.StaticDataList.Find(entry => (entry as ScriptableCategory)?.entryName == name);
        if (results == null) Debug.LogError($"Couldn't find category \"{name}\" in categories database!");
        return results;
    }
}
