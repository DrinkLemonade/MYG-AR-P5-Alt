using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager i;

    [SerializeField] CategoryDatabase categoryDatabase;
    [SerializeField] FurnitureDatabase furnitureDatabase;
    void Awake()
    {
        if (i != null)
            Destroy(i.gameObject);
        i = this;

        categoryDatabase.InitDatabase();
        furnitureDatabase.InitDatabase();
    }
}
