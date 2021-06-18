
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationManager : MonoBehaviour
{

    #region exposed
    [SerializeField]
    private EventSystem _event;
    #endregion
    #region Unity API


    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {

            Debug.Log("J'avance");

            _event.firstSelectedGameObject = DebugMenu.m_menuDebugButton[0].gameObject;
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Je reviens en arrière");
        }
    }

    #endregion
}
