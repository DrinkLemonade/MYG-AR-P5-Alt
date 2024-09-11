using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Utility script that renames textmeshpro text to the GO's name.
public class AutoRename : MonoBehaviour
{
    void Awake()
    {
        var tmp = GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = gameObject.name;
    }
}
