using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public static bool isRestart = false;


    public void restartGame()
    {
        isRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //yeniden yuklemede "Play Game" panel aktif olmas?n diye kontrol edecek

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
