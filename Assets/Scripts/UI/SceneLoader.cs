using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [Header("UI Reference")]
    public GameObject loadingScreen;
    public Slider progressBar;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Hide loading screen initially
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
            if (progressBar != null) progressBar.value = 0;
        }

        // Optional: Fake load for effect if the scene is too fast
        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            if (progressBar != null)
                progressBar.value = progress;

            yield return null;
        }

        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
}
