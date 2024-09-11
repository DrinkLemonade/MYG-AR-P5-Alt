using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableEntryButton : MonoBehaviour
{
    public ScriptableDBEntry entry;

    [SerializeField]
    TextMeshProUGUI tmp;
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
            tmp.text = e.entryName;
            img.gameObject.SetActive(false);
            filter.mesh = e.associatedMesh;

            rTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            renderCam.targetTexture = rTexture;
            raw.texture = renderCam.targetTexture;
        }
        else if (entry is ScriptableCategory)
        {
            var e = (ScriptableCategory)entry;
            tmp.text = e.entryName;
            filter.gameObject.SetActive(false);
            img.sprite = e.image;
        }
        else Debug.LogError("Entry not supported!");
    }
}
