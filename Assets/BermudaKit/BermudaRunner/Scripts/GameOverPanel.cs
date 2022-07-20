using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
