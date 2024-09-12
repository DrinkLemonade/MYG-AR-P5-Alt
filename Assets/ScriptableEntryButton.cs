using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEntryButton : MonoBehaviour
{
    public ScriptableDBEntry entry;

    [SerializeField]
    TextMeshProUGUI tmpName, tmpDesc, tmpPrice;
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
            tmpName.text = e.entryName;
            filter.gameObject.SetActive(false);
            renderCam.gameObject.SetActive(false);
            raw.gameObject.SetActive(false);
            img.sprite = e.image;
        }
        else Debug.LogError("Entry not supported!");
    }

    public void SetProductRender(ScriptableFurniture furn)
    {
        tmpName.text = furn.entryName;
        filter.mesh = furn.associatedMesh;

        renderCam.targetTexture = rTexture;
        raw.texture = renderCam.targetTexture;

        if (tmpDesc != null) tmpDesc.text = furn.description;
        if (tmpPrice != null) tmpPrice.text = furn.PriceFormatted;

    }

    public void Click()
    {
        if (entry is ScriptableFurniture)
        {
            StoreManager.i.EnterFurnitureDetailsPage((ScriptableFurniture)entry);
        }

    }
}
