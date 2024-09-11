using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FurnitureSelectButton : MonoBehaviour
{
    [SerializeField]
    ScriptableFurniture furnitureData;
    TextMeshProUGUI buttonText;


    // Start is called before the first frame update
    void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = furnitureData.entryName;
    }

    // Update is called once per frame
    public void Click()
    {
        FurnitureManager.i.InstantiateFurniture(furnitureData);
    }
}
