using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SceneGlobalManager : SingletonPersistent<SceneGlobalManager>
{

    public void LoadSelector(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
    public void LoadGameWithResults()
    {
        SceneManager.UnloadSceneAsync("CharacterSelection");
        StartCoroutine(LoadGameAndResultsAsync());
    }

    public void UnloadGameAndResults()
    {
        SceneManager.UnloadSceneAsync("MainGameGyroscope");
        SceneManager.UnloadSceneAsync("Results");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
                                        Application.Quit();
#endif
    }

    private IEnumerator LoadGameAndResultsAsync()
    {
        AsyncOperation gameLoad = SceneManager.LoadSceneAsync("MainGameGyroscope", LoadSceneMode.Additive);
        yield return gameLoad;

        AsyncOperation resultsLoad = SceneManager.LoadSceneAsync("Results", LoadSceneMode.Additive);
        yield return resultsLoad;

        Scene resultsScene = SceneManager.GetSceneByName("Results");
        if (resultsScene.isLoaded)
        {
            GameObject[] rootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }

        Scene currentScene = SceneManager.GetSceneByName("CharacterSelection");
        if (currentScene.IsValid())
        {
            yield return SceneManager.UnloadSceneAsync(currentScene);
        }
    }
    public void ShowResults()
    {
        Scene gameScene = SceneManager.GetSceneByName("MainGameGyroscope");
        if (gameScene.IsValid())
        {
            GameObject[] rootObjects = gameScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }
        Scene resultsScene = SceneManager.GetSceneByName("Results");
        if (resultsScene.IsValid())
        {
            GameObject[] rootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(true);
            }
        }
    }

    public void RestartGame()
    {
       SceneManager.LoadScene("S9_Moviles");
        Time.timeScale = 1f;
    }
}