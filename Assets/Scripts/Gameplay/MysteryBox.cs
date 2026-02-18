using UnityEngine;
using System.Collections;

public class MysteryBox : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject collectibleObject; 
    public float fadeDuration = 0.5f;

    [Header("Visuals")]
    public SpriteRenderer boxSprite;

    private bool isOpened = false;

    void Start()
    {
        // Hide collectible initially
        if (collectibleObject != null)
        {
            collectibleObject.SetActive(false);
        }
        
        if (boxSprite == null) boxSprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenBox();
        }
    }

    private void OpenBox()
    {
        isOpened = true;
        StartCoroutine(RevealProcess());
    }

    IEnumerator RevealProcess()
    {
        // Fade out box
        if (boxSprite != null)
        {
            float timer = 0f;
            Color originalColor = boxSprite.color;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                boxSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            
            boxSprite.enabled = false;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }

        // Activate the item
        if (collectibleObject != null)
        {
            collectibleObject.SetActive(true);
        }
    }
}
