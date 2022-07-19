using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bermuda.Runner;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject buyCharacter;
    [SerializeField] private GameObject cantBuyCharacter;
    [SerializeField] private BermudaRunnerCharacter character;
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "GameOverGround")
        {
            character.SetRun(false);
            character.IdleAnimation();
            panel.SetActive(true);
            Debug.Log("val: " + SwitchCharacter.fruitValue + " eldeki: " + ScoreUI.score);
            if (SwitchCharacter.fruitValue <= ScoreUI.score)
                buyCharacter.SetActive(true);
            else  
                cantBuyCharacter.SetActive(true);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
