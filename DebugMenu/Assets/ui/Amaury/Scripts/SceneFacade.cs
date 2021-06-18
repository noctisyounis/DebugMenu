using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFacade : MonoBehaviour
{

    #region private

    private bool isLoaded;
    #endregion
    #region Main

    public void LoadSceneAdditive(string sceneName)
    {
        if(!isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            isLoaded = true;
        }
        
    }

    public void RemoveAdditiveScene(string sceneName)
    {
        if (isLoaded)
        {
        SceneManager.UnloadSceneAsync(sceneName);
            isLoaded = false;
        }
    }

    #endregion
}