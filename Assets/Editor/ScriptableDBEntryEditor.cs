using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableDBEntry))]
public class ScriptableDBEntryEditor : Editor
{
    //I never got this to work previously, I'll have to rethink this
    /*
    public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
    {
        Debug.Log("special editor!");
        ScriptableDBEntry actor = target as ScriptableDBEntry;

        if (actor == null || actor.icon == null)
            return null;

        Texture2D previewTexture = null;

        while (previewTexture == null)
        {
            previewTexture = AssetPreview.GetAssetPreview(actor.icon.texture);
        }

        Texture2D cache = new Texture2D(width, height);
        return cache;
    }
    */
}
