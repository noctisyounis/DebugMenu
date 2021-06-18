using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public RectTransform buttonWrapper;

    private Text _title;
    private string _parent;
    private List<string> _subfolders = new List<string>();
    
    private void Awake() 
    {
        _title = GetComponentInChildren<Text>();    
    }

    public void Setup(string title, string[] subfolders, string parent)
    {
        _title.text = title;
        _parent = parent;
        foreach (var subfolder in subfolders)
        {
            if(_subfolders.Contains(subfolder)) continue;

            _subfolders.Add(subfolder);
        }
    }

    public void SwitchTo(string subfolder)
    {
        if (!_subfolders.Contains(subfolder)) return;
        
        DebugBuilder.livingPanels[subfolder].gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    internal void Return()
    {
        DebugBuilder.livingPanels[_parent].gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
