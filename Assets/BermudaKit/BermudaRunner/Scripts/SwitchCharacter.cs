using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bermuda.Runner;
using Bermuda.Animation;

public class SwitchCharacter : MonoBehaviour
{
    [SerializeField] private BermudaRunnerCharacter _character;
    private SimpleAnimancer _animancer1, _animancer2;
    [SerializeField] private GameObject _character1, _character2;
    private int _whichCharacterIsOn = 1;

   // Start is called before the first frame update
   void Start()
    {
        _character1.gameObject.SetActive(false);
        _character2.gameObject.SetActive(true);
        _animancer1 = _character1.GetComponent<SimpleAnimancer>();
        _animancer2 = _character2.GetComponent<SimpleAnimancer>();
    }

    public void SwitchAvatar()
    {
        switch (_whichCharacterIsOn)
        {
            case 1:
                _whichCharacterIsOn = 2;
                _character1.gameObject.SetActive(false);
                _character2.gameObject.SetActive(true);
                _character.SetAnimancer(_animancer2);
                break;

            case 2:
                _whichCharacterIsOn = 1;
                _character1.gameObject.SetActive(true);
                _character2.gameObject.SetActive(false);
                _character.SetAnimancer(_animancer1);
                break;
        }
    }
}
