using UnityEngine;


public class SafeCollectible : MonoBehaviour
{
    public string funFact;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.CollectSafe();
            UIManager.instance.ShowFunFact(funFact);
            Destroy(gameObject);
        }
    }
}