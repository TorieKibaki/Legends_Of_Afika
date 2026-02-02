using UnityEngine;
using System.Collections;


public class DisappearingTile : MonoBehaviour
{
    private Vector3 startPos;


    void Start()
    {
        startPos = transform.position;
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        transform.position = startPos;
        gameObject.SetActive(true);
    }
}