using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeInAr : MonoBehaviour
{
    // Start is called before the first frame update
    public void Click()
    {
        SessionData.i.viewInAr = StoreManager.i.selectedProduct;
        StoreManager.i.EnterArMode();
    }
}
