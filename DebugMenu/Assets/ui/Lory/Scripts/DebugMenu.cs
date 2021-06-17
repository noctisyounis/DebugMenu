using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    #region Testing

    private string[] testArray = new string[] { "Gizmo/Afficher", "FrameRate", "OptionsCharacters/heal", "OptionsCharacters/boosts", "Quitter" };


    private void Start()
    {
        GenerateButton(testArray);
    }

    #endregion 

    #region Exposed

    public static List<Button> m_menuDebugButton = new List<Button>();

    [SerializeField]
    private Text _headerTitle;
    [SerializeField]
    private RectTransform _backgroundMenu;
    [SerializeField]
    private RectTransform _prefabButton;
    [SerializeField]
    private Transform _parentMenuButton;

    [SerializeField]
    private float _textSpacing;

    #endregion


    #region Unity API

    private void OnGUI()
    {
        GUILayout.Button($"{m_menuDebugButton.Count}");
    }

    private void Update()
    {
        ResponsiveMenu();
    }

    #endregion


    #region Utils

    private void ResponsiveMenu()
    {
        var addToList = _parentMenuButton.GetComponentsInChildren<Button>();

        foreach (var element in addToList)
        {
            if (!m_menuDebugButton.Contains(element))
            {
                m_menuDebugButton.Add(element);
            }
        }

        if(m_menuDebugButton.Count < 20)
        {
            var sizeMenu = _prefabButton.rect.height * m_menuDebugButton.Count;
            _backgroundMenu.sizeDelta = new Vector2(_backgroundMenu.rect.width, _headerTitle.rectTransform.rect.height + sizeMenu + _textSpacing);
        }
    }

    private void GenerateButton(string[] menusArray)
    {
        HashSet<string> firstmenu = new HashSet<string>();
        for (int i = 0; i < menusArray.Length; i++)
        {
            string[] commands = menusArray[i].Split('/');
            firstmenu.Add(commands[0]);
        }
        foreach (string name in firstmenu)
        {
            _prefabButton.GetComponent<Button>().GetComponentInChildren<Text>().text = name;
            GameObject.Instantiate(_prefabButton, _parentMenuButton);
        }
    }

    #endregion


    #region Private

    #endregion
}