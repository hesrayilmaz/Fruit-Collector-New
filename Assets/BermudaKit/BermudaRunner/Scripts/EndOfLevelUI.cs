using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject buyCharacter;
    [SerializeField] private GameObject cantBuyCharacter;

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        if (SwitchCharacter.fruitValue <= ScoreUI.score)
            buyCharacter.SetActive(true);
        else
            cantBuyCharacter.SetActive(true);
    }
   
}