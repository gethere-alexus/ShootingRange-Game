using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]
public class ButtonManager : MonoBehaviour
{

    public void OnPlayClicked()
    {

        SceneManager.LoadScene(1);
    }
    public void OnExitClicked()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); 
        #endif
    }
    public void OnSettingsClicked()
    {

    }

}
