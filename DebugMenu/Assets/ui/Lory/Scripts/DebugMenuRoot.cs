using System.Collections.Generic;
using UnityEngine;

public class DebugMenuRoot : MonoBehaviour
{
    #region Testing

    private string[] testArray = new string[] { "Gizmo/Afficher/Collider", "FrameRate", "Options Characters/Heal", "Options Characters/Boosts", "Quitter" };

    #endregion 

    #region Exposed

    public static DebugMenuRoot m_instance;

    [SerializeField]
    private string _debugMenuName;
    [SerializeField]
    private RectTransform _debugMenuPanel;

    #endregion


    #region Unity API

    private void Awake()
    {
        m_instance = this;
        _menus = new Dictionary<string, DebugMenu>();
        StartGenerate();
    }

    #endregion

    #region Main

    public void GeneratePanel(List<string> paths, int depth)
    {
        var generatedMenus = new List<string>();
        foreach (string path in paths)
        {
            string[] commands = path.Split('/');
            var panelName = commands[depth];
            var panelPath = "";
            var parentPath = "";

            for (int i = 0; i <= depth; i++)
            {
                if(i != 0)
                {
                    panelPath += "/";
                }
                panelPath += commands[i];
            }
            for (int i = 0; i < depth; i++)
            {
                if (i != 0)
                {
                    parentPath += "/";
                }
                parentPath += commands[i];
            }
            if (!_menus.ContainsKey(panelPath))
            {
                var menuPanel = Instantiate(_debugMenuPanel, transform);
                var panel = menuPanel.GetComponent<DebugMenu>();

                panel.Title = panelName;
                panel.m_depth = depth;
                panel.Paths = new List<string>();
                panel.ParentPath = parentPath;

                _menus.Add(panelPath, panel);
                generatedMenus.Add(panelPath);
            }
            _menus[panelPath].Paths.Add(path);
        }

        foreach (var item in generatedMenus)
        {
            _menus[item].StartGenerate();
        }
    }

    public void TryDisplayPanel(string path)
    {
        if (_menus.ContainsKey(path))
        {
            DisplayPanel(path);
        }
        else
        {
            InvokeMethod(path);
        }
    }

    public void DisplayPanel(string path)
    {
        foreach(var item in _menus)
        {
            item.Value.gameObject.SetActive(false);
        }
        _menus[path].gameObject.SetActive(true);
    }

    public void InvokeMethod(string path)
    {
        Debug.Log($"Action At {path}");
    }

    private void StartGenerate()
    {
        if (!_wasGenerate)
        {
            var rootedPaths = LinkPathsToRoot(new List<string>(testArray));
            GeneratePanel(rootedPaths, 0);
            _wasGenerate = true;
            DisplayPanel(_debugMenuName);
        }
    }

    #endregion

    #region Utils

    private List<string> LinkPathsToRoot(List<string> paths)
    {
        var result = new List<string>();

        foreach (var path in paths)
        {
            result.Add($"{_debugMenuName}/{path}");
        }

        return result;
    }

    #endregion


    #region Private

    private Dictionary<string, DebugMenu> _menus;

    private bool _wasGenerate;

    #endregion
}