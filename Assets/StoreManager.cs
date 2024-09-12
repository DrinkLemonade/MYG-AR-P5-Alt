using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public static StoreManager i;

    GameObject activePage;
    public ScriptableFurniture selectedProduct;

    [SerializeField]
    ScriptableEntryButton selectedProductDisplay;

    [SerializeField]
    GameObject pageLogin, pageLanding, pageSearch, pageInfo;
    enum Page
    {
        Login, Landing, SearchByCategory, SearchByName, FurnitureInfo
    }

    private void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);

        i = this;

        activePage = pageLanding;
        pageLanding.SetActive(true);
    }

    void PageLogin()
    {

    }

    public void EnterFurnitureDetailsPage(ScriptableFurniture furn)
    {
        selectedProduct = furn;

        activePage.SetActive(false);
        activePage = pageInfo;
        activePage.SetActive(true);

        selectedProductDisplay.entry = selectedProduct;
        selectedProductDisplay.Init();
    }

    public void EnterMainPage()
    {
        activePage.SetActive(false);
        activePage = pageLanding;
        activePage.SetActive(true);
    }

    public void EnterArMode()
    {
        SceneManager.LoadScene("ARScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
