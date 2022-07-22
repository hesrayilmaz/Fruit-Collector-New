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
        {
            cantBuyCharacter.SetActive(false);
            buyCharacter.SetActive(true);
        }
        else
        {
            buyCharacter.SetActive(false);
            cantBuyCharacter.SetActive(true);
        }
            
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
   
}
