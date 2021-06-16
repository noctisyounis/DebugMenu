using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugUIManager : MonoBehaviour
{


    #region Testing

    private string[] testArray= new string[] {"gizmo/afficher" ,"FrameRate" , "Quitter","optionsCharacters/heal","optionsCharacters/boosts" };


    private void Start()
    {
        OnShow(testArray);
    }

    #endregion


    #region Exposed
    [SerializeField]
    private GameObject _buttonPrefab;
    [SerializeField]
    private GameObject _panelPrefab;
   
    public static string[] MenuList;
    #endregion


    #region methods

    private void OnShow(string[] menusArray )
    {
        MenuList = menusArray;
        HashSet<string> firstmenu = new HashSet<string>();
        for (int i = 0; i < menusArray.Length; i++)
        {
            string[] commands = menusArray[i].Split('/');
            firstmenu.Add(commands[0]);
        }

        foreach (string name in firstmenu)
        {
            _buttonPrefab.GetComponent<Button>().GetComponentInChildren<Text>().text = name;
            GameObject.Instantiate(_buttonPrefab, _transform);

        }


    }


    #endregion


    #region Unity API

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    #endregion


    #region Private

    private Transform _transform;

    private Dictionary<string,GameObject> _commandsArray;

    #endregion
}
