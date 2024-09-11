using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ARFurnitureInfoPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmpTitle, tmpPrice, tmpDescription;

    public void UpdateInfo(string title, string price, string desc)
    {
        tmpTitle.text = title;
        tmpPrice.text = price;
        tmpDescription.text = desc;
    }
}
