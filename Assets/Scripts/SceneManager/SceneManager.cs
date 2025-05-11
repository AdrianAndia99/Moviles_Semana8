using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SceneGlobalManager : SingletonPersistent<SceneGlobalManager>
{
    //[Header("Carga de Escena")]
    //[SerializeField] private AudioMixerSO masterMixerSO;
    //[SerializeField] private AudioMixerSO sfxMixerSO;
    //[SerializeField] private AudioMixerSO musicMixerSO;
    //[SerializeField] private ScoreManager scoreManager;

    //[SerializeField] private ProjectilePoolSO projectilePoolSO;

    private void Start()
    {
        //masterMixerSO.EnableSound();
        //sfxMixerSO.EnableSound();
        //musicMixerSO.EnableSound();
    }

    public void LoadSelector()
    {
        SceneManager.LoadSceneAsync("CharacterSelection", LoadSceneMode.Single);
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

    //adicional lab6 moviles
    //public void UpdateMasterVolume(float value)
    //{
    //    masterMixerSO.UpdateVolume(value);
    //}

    //public void UpdateSFXVolume(float value)
    //{
    //    sfxMixerSO.UpdateVolume(value);
    //}
    //public void UpdateMusicVolume(float value)
    //{
    //    musicMixerSO.UpdateVolume(value);
    //}
    //public float GetMasterVolume()
    //{
    //    return masterMixerSO.GetCurrentVolumeValue();
    //}
    //public float GetSFXVolume()
    //{
    //    return sfxMixerSO.GetCurrentVolumeValue();
    //}
    //public float GetMusicVolume()
    //{
    //    return musicMixerSO.GetCurrentVolumeValue();
    //}

    public void RestartGame()
    {
       // scoreManager.ResetScore();
        SceneManager.UnloadSceneAsync("MainGameGyroscope");
       // projectilePoolSO.ClearPool();
        StartCoroutine(ReloadGameScene());
    }

    private IEnumerator ReloadGameScene()
    {
        AsyncOperation gameLoad = SceneManager.LoadSceneAsync("MainGameGyroscope", LoadSceneMode.Additive);
        yield return gameLoad;

        Scene resultsScene = SceneManager.GetSceneByName("Results");
        if (resultsScene.IsValid())
        {
            GameObject[] rootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }

        Scene gameScene = SceneManager.GetSceneByName("MainGameGyroscope");
        if (gameScene.IsValid())
        {
            GameObject[] rootObjects = gameScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(true);
            }
        }
    }
}