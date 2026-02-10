using UnityEngine;

public class WebGLFocusFix : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval(
            "document.body.focus();"
        );
#endif
    }
}
