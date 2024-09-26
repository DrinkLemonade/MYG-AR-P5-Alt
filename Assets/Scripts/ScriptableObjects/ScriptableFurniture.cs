using System;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Scriptable Furniture")]
public class ScriptableFurniture : ScriptableDBEntry
{
    [SerializeField]
    public string entryName = "Entry Name";
    [TextArea]
    [SerializeField]
    public string description = "Entry Description";
    [SerializeField]
    public ScriptableCategory category;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public Mesh associatedMesh;

    [SerializeField]
    private int priceInteger;
    [SerializeField]
    [Range(0, 99)]
    private int priceFraction;

    [HideInInspector]
    public string PriceFormatted => FormatPrice();

    string FormatPrice()
    {
        decimal price = priceInteger + (priceFraction / 100);
        return price.ToString("C2", //2 digit point precision
                          CultureInfo.CreateSpecificCulture("fr-FR")); //Metropolitan France
    }

    public void SetPriceFromJson(string price)
    {
        var splitPrice = price.Split("."); //Example: "33.99" will be split into "33" and "99"
        priceInteger = System.Convert.ToInt32(splitPrice[0]);
        priceFraction = System.Convert.ToInt32(splitPrice[1]);
    }
}
