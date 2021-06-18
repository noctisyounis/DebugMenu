using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class DebugUIManager : MonoBehaviour
{


    #region Testing

    private string[] testArray= new string[] {"gizmo/afficher" ,"FrameRate" , "Quitter","optionsCharacters/heal","optionsCharacters/boosts" };


    private void Start()
    {
        split(testArray);
    }

    #endregion


    #region Exposed

    [SerializeField]
    private GameObject _buttonPrefab;
    [SerializeField]
    private GameObject _panelPrefab;

    [SerializeField]
    private EventSystem _event;
      
    #endregion


    #region methods

    private void split(string[] menusArray )
    {
        bool firstButtonSelected = false;
        HashSet<string> firstmenu = new HashSet<string>();
        for (int i = 0; i < menusArray.Length; i++)
        {
            string[] commands = menusArray[i].Split('/');
           
           firstmenu.Add(commands[0]);
        }
        

        foreach (string name in firstmenu)
        {
            _buttonPrefab.GetComponent<Button>().GetComponentInChildren<Text>().text = name;
            GameObject button =GameObject.Instantiate(_buttonPrefab, _transform);
            if (!firstButtonSelected)
            {
                firstButtonSelected = true;
                _event.firstSelectedGameObject = button;
            }
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

    #endregion
}
