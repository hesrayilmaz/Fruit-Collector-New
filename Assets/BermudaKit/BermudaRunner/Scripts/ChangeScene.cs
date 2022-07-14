using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bermuda.Runner;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject characterButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private BermudaRunnerCharacter character;
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "GameOverGround")
        {
            character.SetRun(false);
            character.IdleAnimation();
            characterButton.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
