using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ali.Helper;

public class GameOverPanel : MonoBehaviour
{
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("RunnerScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
