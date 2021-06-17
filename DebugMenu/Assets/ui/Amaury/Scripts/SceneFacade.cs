using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFacade : MonoBehaviour
{
    #region Main

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void RemoveAdditiveScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    #endregion
}