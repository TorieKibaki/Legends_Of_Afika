using UnityEngine;

public class Random : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { }
  
        public void Quit()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}

