using Lean.Touch;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

public class FurnitureInstantiator : DefaultObserverEventHandler
{
    public ImageTargetBehaviour mImageTarget;
    public GameObject planePrefab;
    //Camera cam;
    [SerializeField]
    public GameObject furnitureBasePrefab, aimReticlePrefab, aimReticleParent, aimReticle;
    [SerializeField]
    float reticleHoverAmount = 0.1f, planeScale = 0.1f, furnitureScale = 0.1f;
    Vector3 raycastHitPosition;

    bool setupDone = false;
    [SerializeField]
    LayerMask floorMask;

    [SerializeField]
    Material transparentMaterial;

    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            //Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Physics.Raycast(ray.origin, ray.direction, out hitInfo, 9999, floorMask);

        //Keep track of hit info for object placement
        raycastHitPosition = hitInfo.point;
        if (aimReticle != null)
        {
            aimReticle.transform.position = new Vector3(raycastHitPosition.x, raycastHitPosition.y + reticleHoverAmount, raycastHitPosition.z);
        }
        Debug.DrawLine(Camera.main.transform.position, hitInfo.point, Color.red);

    }

    protected override void OnTrackingFound()
    {
        if (!setupDone)
        {
            SetUpPlaneAndReticle();
            setupDone = true;
        }
        aimReticle.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        //Presumably this won't fire if no tracking has been found and thus no plane/reticle exist?
        if (aimReticle != null) aimReticle.SetActive(false);
    }

    public void SetUpPlaneAndReticle()
    {
        GameObject plane = Instantiate(planePrefab);
        plane.transform.parent = mImageTarget.transform;
        plane.transform.localPosition = Vector3.zero;
        plane.transform.localRotation = Quaternion.identity;
        plane.transform.localScale = Vector3.one * planeScale;
        plane.transform.gameObject.SetActive(true);

        aimReticle = Instantiate(aimReticlePrefab, aimReticleParent.transform);
    }

    public ARFurnitureEntity InstantiatePreview(ScriptableFurniture scriptable)
    {
        Debug.Log($"Instantiating: {scriptable.entryName}");
        //InstantiateThis base object and give it actual furniture prefab as child
        var prefab = scriptable.associatedMesh;
        GameObject furnBase = Instantiate(furnitureBasePrefab);

        //Give base object a reference to the scriptable object
        ARFurnitureEntity furnLogic = furnBase.GetComponent<ARFurnitureEntity>();
        furnLogic.reference = scriptable;
        furnLogic.filter.mesh = scriptable.associatedMesh;
        furnLogic.coll.sharedMesh = scriptable.associatedMesh;

        //Parent to ImageTarget and name
        furnBase.transform.parent = mImageTarget.transform;
        furnBase.name = scriptable.entryName;

        //Place and scale
        furnBase.transform.position = raycastHitPosition;
        furnBase.transform.localScale = Vector3.one * furnitureScale;

        return furnLogic;
    }





}