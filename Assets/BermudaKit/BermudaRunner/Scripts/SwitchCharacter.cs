using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bermuda.Runner;
using Bermuda.Animation;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchCharacter : MonoBehaviour
{
    [SerializeField] private BermudaRunnerCharacter _character;
    [SerializeField] private EndOfLevelUI _panel;
    private int selectedCharacter;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private SimpleAnimancer[] animancers;
    public TextMeshProUGUI necessaryFruit;
    public static int fruitValue = 35;
    public static bool _isAvatarChanged = false;
    
   
    // Start is called before the first frame update
    void Start()
    {
        // int.TryParse(necessaryFruit.GetParsedText(), out fruitValue);
        //selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        selectedCharacter = 0;
        Transform t = transform.Find("localMover");

        characters = new GameObject[t.childCount - 1];
        animancers = new SimpleAnimancer[t.childCount - 1];

        for (int i = 0, j = 0; i <= characters.Length; i++)
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

        PlayerPrefs.SetInt("selectedCharacter", 0);
        characters[0].SetActive(true);
        _character.SetAnimancer(animancers[0]);
    }
    
    private void Update()
    {
        /*if (_isAvatarChanged)
        {
            characters[selectedCharacter-1].SetActive(false);
            characters[selectedCharacter].SetActive(true);
            _character.SetAnimancer(animancers[selectedCharacter]);
            _isAvatarChanged = false;
        }*/
    }
    public void SwitchAvatar()
    {
        if (fruitValue <= ScoreUI.score)
        {
            ScoreUI.score = ScoreUI.score - fruitValue;
            characters[selectedCharacter].SetActive(false);
            //selectedCharacter = (selectedCharacter + 1) % characters.Length;
            selectedCharacter = selectedCharacter + 1;
            characters[selectedCharacter].SetActive(true);
            _character.SetAnimancer(animancers[selectedCharacter]);
            PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
            _isAvatarChanged = true;

        }
    }

}
