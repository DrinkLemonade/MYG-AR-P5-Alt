using UnityEngine;

public class SessionData : MonoBehaviour
{
    public ScriptableFurniture viewInAr;
    public static SessionData i;
    private void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);

        i = this;
    }
}
