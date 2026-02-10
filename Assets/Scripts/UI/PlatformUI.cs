using UnityEngine;

public class PlatformUI : MonoBehaviour
{
    public GameObject mobileControls;

    void Start()
    {
        if (!Application.isMobilePlatform)
            mobileControls.SetActive(false);
    }
}
