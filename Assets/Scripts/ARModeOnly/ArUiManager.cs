using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArUiManager : MonoBehaviour
{
    public static ArUiManager i;
    /*[SerializeField]
    ToggleGroup toggleGroup;

    List<Toggle> furnitureToggles;
    Toggle chosenFurniture;
    */
    [SerializeField]
    ARFurnitureInfoPanel infoPanel;

    [SerializeField]
    PopulateContentList favoritesHolder;

    private void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);

        i = this;

        if (SessionData.i.viewInAr != null)
        {

            //TODO: Less sloppy
            bool exists = false;
            foreach (var item in favoritesHolder.ButtonsInList)
            {
                if (item.entry == SessionData.i.viewInAr) exists = true; //Highlight the button or something
                                                         //item.Click();
            }
            if (!exists)
            {
                favoritesHolder.AddFurniture(SessionData.i.viewInAr);
            }

        }
    }

    public void QuitArMode()
    {
        SceneManager.LoadScene("StoreScene");
    }

    //Not used
    /*
    void HandleFingerTap(Lean.Touch.LeanFinger finger)
    {
        Debug.Log("You just tapped the screen with finger " + finger.Index + " at " + finger.ScreenPosition);

        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(finger.ScreenPosition.x, finger.ScreenPosition.y, 0f));
        //Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Physics.Raycast(ray, out hitInfo);

        FurnitureManager.i.ScreenTapped(hitInfo.point);
    }
    */

    public void UpdateInfoPanel()
    {
        return;
        //Unused
        if (FurnitureManager.i.currentFurniture == null) { Debug.LogError("Furniture null!"); return; }

        infoPanel.gameObject.transform.parent = FurnitureManager.i.currentFurniture.transform;

        if (!infoPanel.gameObject.activeInHierarchy) infoPanel.gameObject.SetActive(true);
        infoPanel.UpdateInfo(
            FurnitureManager.i.currentFurniture.reference.entryName,
            FurnitureManager.i.currentFurniture.reference.PriceFormatted,
            FurnitureManager.i.currentFurniture.reference.description);
    }

    public void HideInfoPanel()
    {
        if (infoPanel.gameObject.activeInHierarchy)
            infoPanel.gameObject.SetActive(false);

    }


}
