using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingSceneUILogic : MonoBehaviour
{
    [SerializeField]
    GameObject activePage, landingPage, accountCreationPage;
    public void Quit()
    {
        Application.Quit();
    }

    public void SwitchPage(GameObject newPage)
    {
        activePage.SetActive(false);
        activePage = newPage;
        activePage.SetActive(true);
    }

    public void SwitchPageToAccountCreation()
    {
        SwitchPage(accountCreationPage);
    }

    public void SwitchPageToLanding()
    {
        SwitchPage(landingPage);
    }
}
