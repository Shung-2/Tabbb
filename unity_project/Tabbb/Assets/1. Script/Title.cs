using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Game";

    public void ClickStart()
    {
        Debug.Log("시작");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickExit()
    {
        Debug.Log("종료");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
