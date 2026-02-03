using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI messageText;
    public GameObject panel;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowFunFact(string fact)
    {
        panel.SetActive(true);
        messageText.text = fact;
    }

    public void ShowHint(string hint)
    {
        panel.SetActive(true);
        messageText.text = hint;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
