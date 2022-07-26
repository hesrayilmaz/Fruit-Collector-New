using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fruitTextBuy;
    [SerializeField] private TextMeshProUGUI fruitTextCantBuy;
    [SerializeField] private GameObject buyCharacter;
    [SerializeField] private GameObject cantBuyCharacter;
    //[SerializeField] private GameObject[] characters;
    public static int nextCharacter;
    KeyValuePair<GameObject, int>[] characters;
    public static int fruitValue=35;

 
    public void Init()
    {
        Transform t = transform.Find("Character");
        //characters = new GameObject[t.childCount];
        characters = new KeyValuePair<GameObject, int>[t.childCount];
        nextCharacter = 0;

        /*for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = t.GetChild(i).gameObject;
        }*/
       
        for (int i = 0; i < t.childCount; i++)
        {
            characters[i] = new KeyValuePair<GameObject, int>(t.GetChild(i).gameObject,fruitValue);
            fruitValue += 15;
        }

        foreach (KeyValuePair<GameObject,int> pair in characters)
        {
            pair.Key.SetActive(false);
        }
        fruitValue = 35;
        characters[nextCharacter].Key.SetActive(true);
    }

    private void Update()
    {
        fruitTextBuy.text = "" + fruitValue;
        fruitTextCantBuy.text = "" + fruitValue;
    }
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        Debug.Log(nextCharacter);
        
        if (SwitchCharacter._isAvatarChanged)
         {
            nextCharacter++;
            fruitValue = characters[nextCharacter].Value;
            characters[nextCharacter - 1].Key.SetActive(false);
            characters[nextCharacter].Key.SetActive(true);
            SwitchCharacter._isAvatarChanged = false;
        }

        if (characters[nextCharacter].Value <= ScoreUI.score)
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

    /*public int GetFruitValue()
    {
        return characters[nextCharacter].Value;
    }*/
   
}
