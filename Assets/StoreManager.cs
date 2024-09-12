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

    [SerializeField]
    PopulateContentList searchContents;
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

    private void SwitchToPage(GameObject page)
    {
        activePage.SetActive(false);
        activePage = page;
        activePage.SetActive(true);
    }

    void PageLogin()
    {

    }

    public void EnterFurnitureDetailsPage(ScriptableFurniture furn)
    {
        selectedProduct = furn;

        SwitchToPage(pageInfo);

        selectedProductDisplay.entry = selectedProduct;
        selectedProductDisplay.Init();
    }

    public void EnterMainPage()
    {
        SwitchToPage(pageLanding);
    }

    public void EnterArMode()
    {
        SceneManager.LoadScene("ARScene");
    }

    public void SearchProductsByCategory(ScriptableCategory cat)
    {
        SwitchToPage(pageSearch);
        foreach (var item in searchContents.ButtonsInList)
        {
            ScriptableFurniture furn = (ScriptableFurniture)item.entry;
            if (furn.category == cat) item.gameObject.SetActive(true);
            else item.gameObject.SetActive(false);

        }
    }

    public void SearchProductsByName(string name)
    {
        if (activePage != pageSearch) SwitchToPage(pageSearch);
        foreach (var item in searchContents.ButtonsInList)
        {
            item.gameObject.SetActive(true);
            ScriptableFurniture furn = (ScriptableFurniture)item.entry;
            if (furn.name.Contains(name)) item.gameObject.SetActive(true);
            else item.gameObject.SetActive(false);

        }

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
