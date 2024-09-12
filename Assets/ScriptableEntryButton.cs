using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptableEntryButton : MonoBehaviour
{
    public ScriptableDBEntry entry;

    [SerializeField]
    TextMeshProUGUI tmpNameMain, tmpNameAr, tmpDesc, tmpPrice;
    [SerializeField]
    GameObject mainNameplate, arModeNameplate;
    [SerializeField]
    Image img;
    [SerializeField]
    MeshFilter filter;

    [SerializeField]
    Camera renderCam;
    [SerializeField]
    RawImage raw;
    RenderTexture rTexture;

    public void Init()
    {
        if (entry == null) { Debug.LogError("Entry null!"); return; }
        //Down cast
        //TODO less sloppy
        if (entry is ScriptableFurniture)
        {
            var e = (ScriptableFurniture)entry;
            img.gameObject.SetActive(false);
            rTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);

            SetProductRender(e);
        }
        else if (entry is ScriptableCategory)
        {
            var e = (ScriptableCategory)entry;
            tmpNameMain.text = e.entryName;
            filter.gameObject.SetActive(false);
            renderCam.gameObject.SetActive(false);
            raw.gameObject.SetActive(false);
            img.sprite = e.image;
        }
        else Debug.LogError("Entry not supported!");
    }

    public void NameplateMode(bool arMode)
    {
        if (arMode)
        {
            ScriptableFurniture e = (ScriptableFurniture)entry;
            tmpNameAr.text = e.entryName;
            arModeNameplate.SetActive(true);
            mainNameplate.SetActive(false);
        }
        else
        {
            ScriptableFurniture e = (ScriptableFurniture)entry;
            tmpNameMain.text = e.entryName;
            arModeNameplate.SetActive(false);
            mainNameplate.SetActive(true);
        }
    }

    public void SetProductRender(ScriptableFurniture furn)
    {
        tmpNameMain.text = furn.entryName;
        filter.mesh = furn.associatedMesh;

        renderCam.targetTexture = rTexture;
        raw.texture = renderCam.targetTexture;

        if (tmpDesc != null) tmpDesc.text = furn.description;
        if (tmpPrice != null) tmpPrice.text = furn.PriceFormatted;

    }

    public void Click()
    {
        if (SceneManager.GetActiveScene().name == "StoreScene")
        {
            if (entry is ScriptableFurniture)
            {
                StoreManager.i.EnterFurnitureDetailsPage((ScriptableFurniture)entry);
            }
            else if (entry is ScriptableCategory)
            {
                StoreManager.i.SearchProductsByCategory((ScriptableCategory)entry);
            }
        }
        else if (SceneManager.GetActiveScene().name == "ARScene")
        {
            if (entry is ScriptableFurniture)
            {
                FurnitureManager.i.InstantiateFurniture((ScriptableFurniture)entry);
            }
        }
    }
}
