using UnityEngine;

public class StartPopup : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            gameObject.SetActive(false);
    }
}
