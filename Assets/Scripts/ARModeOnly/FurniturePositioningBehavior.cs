using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FurniturePositioningBehavior : VuforiaMonoBehaviour
{
    [SerializeField]
    GameObject prefab;


    public void InteractiveHitTest(HitTestResult res)
    {
        //TODO
        //If anchor already exists, skip to creating object
        //3 different prefabs on this anchor, as an example of placing 3 objects on the ground floor
        Debug.Log(res);
        AnchorBehaviour anchor; //ImageTarget or model or point in space.
        anchor = VuforiaBehaviour.Instance.ObserverFactory.CreateAnchorBehaviour("Ancre", res); //This gives the anchor a position in the hierarchy and a name
        var box = Instantiate(prefab, Vector3.zero, Quaternion.identity, anchor.transform);
        box.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
