using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bermuda.Runner;
using Bermuda.Animation;

public class SwitchCharacter : MonoBehaviour
{
    [SerializeField] private BermudaRunnerCharacter _character;
    private int selectedCharacter;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private SimpleAnimancer[] animancers;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        Transform t = transform.Find("localMover");

        characters = new GameObject[t.childCount-1]; 
        animancers = new SimpleAnimancer[t.childCount-1];

        for (int i = 0, j=0; i <= characters.Length; i++)
        {
            if (t.GetChild(i).gameObject.name == "Main Camera")
                continue;
            characters[j] = t.GetChild(i).gameObject;
            animancers[j] = t.GetChild(i).GetComponent<SimpleAnimancer>();
            j++;
        }

        foreach (GameObject ch in characters)
        {
            ch.SetActive(false);
            //Debug.Log("character: " + ch);
        }
       
        /*foreach (SimpleAnimancer s in animancers)
        {
            Debug.Log("animancer: " + s);
        }*/
        
        if (characters[selectedCharacter])
        {
            characters[selectedCharacter].SetActive(true);
            _character.SetAnimancer(animancers[selectedCharacter]);
        }

    }

    public void SwitchAvatar()
    {
        /*characters[selectedCharacter].SetActive(false);
        //selectedCharacter = (selectedCharacter + 1) % characters.Length;
        selectedCharacter = selectedCharacter + 1;
        characters[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        _character.SetAnimancer(animancers[selectedCharacter]);
        */
        
        switch (selectedCharacter)
        {
            case 0:
                characters[selectedCharacter].SetActive(false);
                
                selectedCharacter = selectedCharacter + 2;
                characters[selectedCharacter].SetActive(true);
                PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
                _character.SetAnimancer(animancers[selectedCharacter]);
                break;

            case 2:
                characters[selectedCharacter].SetActive(false);
                
                selectedCharacter = selectedCharacter - 2;
                characters[selectedCharacter].SetActive(true);
                PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
                _character.SetAnimancer(animancers[selectedCharacter]);
                break;
        }
    }
}
