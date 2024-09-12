using Lean.Touch;
using UnityEngine;

public class ARFurnitureEntity : MonoBehaviour
{
    public ScriptableFurniture reference;

    public bool placed = false;

    [SerializeField]
    public MeshFilter filter;
    [SerializeField]
    MeshRenderer renderer;
    [SerializeField]
    public MeshCollider coll;
    [SerializeField]
    Material transparentMaterial, opaqueMaterial, materialWhenSelected;

    [SerializeField]
    ARFurnitureInfoPanel infoPanel;

    [SerializeField]
    public Outline outline;

    [SerializeField]
    LeanSelectableByFinger leanSelectable;
    [SerializeField]
    LeanDragTranslate leanDragTranslate; 
    void Awake()
    {
        leanDragTranslate.Camera = Camera.main;
        Debug.Log(Camera.main.gameObject.name + " is main camera GO");

        infoPanel.UpdateInfo(reference.entryName, reference.PriceFormatted, reference.description);
        infoPanel.gameObject.SetActive(false);

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    //Called by events
    public void Select()
    {
        //renderer.material = materialWhenSelected;
        if (leanSelectable.IsSelected)
        {
            //If already selected, toggle info panel
            infoPanel.gameObject.SetActive(!infoPanel.gameObject.activeInHierarchy);
        }
        else
        {
            infoPanel.gameObject.SetActive(false);
            FurnitureManager.i.SelectFurniture(this);

            if (outline == null) Debug.LogError("outline null??");
            else outline.enabled = true;

        }
    }

    //Called by LeanSelectable events
    public void Deselect()
    {
        if (outline == null) Debug.LogError("outline null??");
        else outline.enabled = false;
        infoPanel.gameObject.SetActive(false);
        //renderer.material = opaqueMaterial;

    }


}
