using UnityEditor;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    public static FurnitureManager i;
    [SerializeField]
    GameObject imageTarget;
    [SerializeField]
    FurnitureInstantiator instantiator;

    public ARFurnitureEntity currentFurniture;

    [SerializeField]
    float scale = 0.25f;

    private void Update()
    {
        if (currentFurniture == null) return;
        //if (!currentFurniture.placed)
            //currentFurniture.transform.position = instantiator.aimReticle.transform.position;

    }

    private void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);

        i = this;
        //DontDestroyOnLoad(transform.parent);
    }

    public void InstantiateFurniture(ScriptableFurniture scriptableFurniture)
    {
        //No currently previewed furniture
        if (currentFurniture == null)
        {
            var newFurn = InstantiateThis(scriptableFurniture);
            SelectFurniture(newFurn);
        }
        //We're selecting a different furniture to preview
        else if (PrefabUtility.GetCorrespondingObjectFromSource(currentFurniture) != scriptableFurniture.associatedMesh)
        {
            //Destroy(currentFurniture); //Hmmm. Need to think about UX here
            var newFurn = InstantiateThis(scriptableFurniture);
            SelectFurniture(newFurn);
        }
        //We have furniture selected and we're deselecting it
        else
        {
            //Destroy(currentFurniture);
            //currentFurniture = null;
        }


        ARFurnitureEntity InstantiateThis(ScriptableFurniture scr)
        {
            Debug.Log($"{scriptableFurniture.entryName} instantiated");
            return instantiator.InstantiatePreview(scr);
        }

        //Do some checks and whatever
        //If everything goes right...
        //instantiator.mImageTarget = imageTarget.GetComponent<ImageTargetBehaviour>();
        //instantiator.planePrefab = scriptableFurniture.associatedMesh;

        //InstantiateThis(scriptableFurniture.associatedMesh, imageTarget.transform);
        //currentFurniture = placer.SetUpPlaneAndReticle();
        //currentFurniture.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SelectFurniture(ARFurnitureEntity furn)
    {
        if (currentFurniture != null) currentFurniture.Deselect();
        currentFurniture = furn;
        //furn.DoSelectionEvents();
        //ArUiManager.i.UpdateInfoPanel();
        Debug.Log($"{furn.gameObject.name} selected!");
    }

    //Empty location on screen was tapped
    public void SelectNothing()
    {
        Debug.Log("Screen tapped! Nothing was selected");

        if (currentFurniture == null) return; //Already selecting nothing :V
        currentFurniture.Deselect();
        currentFurniture = null;
        ArUiManager.i.HideInfoPanel();
    }


    public void ScreenTapped(Vector3 worldPosition)
    {
        /*
        if (currentFurniture != null)
        {
            currentFurniture.transform.position = worldPosition;
            currentFurniture.placed = true;
        }
        */
    }
    

}
