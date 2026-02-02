using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text messageText;
    public GameObject panel;


    void Awake()
    {
        instance = this;
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