using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    #region Unity API


    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("J'avance");
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Je reviens en arrière");
        }
    }

    #endregion
}
