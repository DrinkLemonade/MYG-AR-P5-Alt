using UnityEditor;
using UnityEngine;

public class ScriptableDBGenerator : MonoBehaviour
{
    [SerializeField] string SOtoCreate;
    public void CreateScriptableObject()
    {
        ScriptableDatabase<ScriptableDBEntry> _inst = (ScriptableDatabase<ScriptableDBEntry>)ScriptableObject.CreateInstance(SOtoCreate);
        string _fileName = SOtoCreate;

        string _path = "Assets/ScriptableObjects/" + _fileName + ".asset"; //Don't forget to add file extension, otherwise this could overwrite the folder with an empty version
        AssetDatabase.CreateAsset(_inst, _path);
        AssetDatabase.SaveAssets();
    }
}
