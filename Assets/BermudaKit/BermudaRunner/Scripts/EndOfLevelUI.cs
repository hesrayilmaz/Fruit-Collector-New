using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject buyCharacter;
    [SerializeField] private GameObject cantBuyCharacter;
    [SerializeField] private GameObject[] characters;
    public static int nextCharacter;
   

    private void Start()
    {
        Transform t = transform.Find("Character");
        characters = new GameObject[t.childCount];
        nextCharacter = 0;
        
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = t.GetChild(i).gameObject;
        }

        foreach (GameObject ch in characters)
        {
            ch.SetActive(false);
            //Debug.Log("character: " + ch);
        }
        //PlayerPrefs.SetInt("nextCharacter", 0);
        characters[nextCharacter].SetActive(true);
    }

    public void SetNextCharacter()
    {
        
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        //characters[nextCharacter - 1].SetActive(false);
        //characters[nextCharacter].SetActive(true);
        // characters[PlayerPrefs.GetInt("nextCharacter")].SetActive(true);
         if (SwitchCharacter._isAvatarChanged)
         {
             nextCharacter++;
            // while (nextCharacter > 0)
            //{
            characters[nextCharacter - 1].SetActive(false);
            characters[nextCharacter].SetActive(true);
            // }
        }

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
