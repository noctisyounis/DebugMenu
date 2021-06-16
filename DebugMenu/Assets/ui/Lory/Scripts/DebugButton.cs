using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private Image _arrowHover;
    [SerializeField]
    private RectTransform _parentButton;

    #endregion


    #region Unity API

    private void Awake()
    {
        _arrowHover.rectTransform.position = _parentButton.rect.position + new Vector2(-30, 20);
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

    public void IndicateHover()
    {
        _currentInstantiate = Instantiate(_arrowHover, _parentButton);
    }

    public void IndicateNoHover()
    {
        Destroy(_currentInstantiate);
    }

    #endregion


    #region Private

    private Image _currentInstantiate;

    #endregion
}