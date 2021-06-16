using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private Text _headerTitle;
    [SerializeField]
    private RectTransform _backgroundMenu;
    [SerializeField]
    private RectTransform _prefabButton;
    [SerializeField]
    private Transform _parentMenuButton;
    [SerializeField]
    private List<Button> _menuDebugButton;

    [SerializeField]
    private float _textSpacing;

    #endregion


    #region Unity API

    private void Awake()
    {
        _menuDebugButton = new List<Button>();
    }

    private void Update()
    {
        responsiveMenu();
    }

    #endregion


    #region Utils

    private void responsiveMenu()
    {
        var addToList = _parentMenuButton.GetComponentsInChildren<Button>();

        foreach (var element in addToList)
        {
            if (!_menuDebugButton.Contains(element))
            {
                _menuDebugButton.Add(element);
            }
        }

        var sizeMenu = _prefabButton.rect.height * _menuDebugButton.Count;
        _backgroundMenu.sizeDelta = new Vector2(_backgroundMenu.rect.width, _headerTitle.rectTransform.rect.height + sizeMenu + _textSpacing);
    }

    #endregion


    #region Private

    #endregion
}