using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    #region Exposed

    public string m_path;

    [SerializeField]
    private Image _arrowHover;
    [SerializeField]
    private RectTransform _parentButton;

    #endregion


    #region Unity API

    private void Awake()
    {
        _arrowHover.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        DebugMenu.m_menuDebugButton.Add(GetComponent<Button>());
    }

    private void OnDisable()
    {
        DebugMenu.m_menuDebugButton.Remove(GetComponent<Button>());
    }

    private void OnDestroy()
    {
        DebugMenu.m_menuDebugButton.Remove(GetComponent<Button>());
    }

    #endregion


    #region Main

    public void OnClick()
    {
        DebugMenuRoot.m_instance.TryDisplayPanel(m_path);
    }

    #endregion


    #region Utils

    public void IndicateHover()
    {
        _arrowHover.gameObject.SetActive(true);
    }

    public void IndicateNoHover()
    {
        _arrowHover.gameObject.SetActive(false);
    }

    #endregion


    #region Private

    private Image _currentInstantiate;

    #endregion
}