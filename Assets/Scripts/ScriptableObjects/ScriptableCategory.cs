using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture Category", menuName = "Scriptable Furniture Category")]
public class ScriptableCategory : ScriptableDBEntry
{
    [SerializeField]
    public string entryName = "Entry Name";
    [TextArea]
    [SerializeField]
    public string description = "Entry Description";
    [SerializeField]
    public Sprite image;
}
