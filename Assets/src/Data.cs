using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
   
    const string PREFAB_PATH = "Data";
    static Data mInstance = null;
    public UsersManager usersManager;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                    go.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            return mInstance;
        }
    }
    void Awake()
    {        
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        usersManager = GetComponent<UsersManager>();
    }  
}
