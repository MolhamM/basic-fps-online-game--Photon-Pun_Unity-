using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    [SerializeField]
    bool InGame;
    #region MONO CALLBACKS
    void Start()
    {
        if (InGame)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
        }
    }
    #endregion
    #region PRIVATE METHODS
    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    #endregion

}
