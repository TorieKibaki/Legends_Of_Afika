using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool hasSafeCollectible = false;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void CollectSafe()
    {
        hasSafeCollectible = true;
    }


    public void ResetLevel()
    {
        hasSafeCollectible = false;
    }
}