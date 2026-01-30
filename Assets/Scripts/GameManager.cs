using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Door levelDoor;
    public TextMeshProUGUI funFactText;

    void Awake()
    {
        instance = this;
    }

    public void CollectSafe(string fact)
    {
        funFactText.text = fact;
        funFactText.gameObject.SetActive(true);
        levelDoor.ActivateDoor();
    }
}
