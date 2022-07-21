using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bermuda.Runner;
using Ali.Helper;

public class ChangeScene : MonoBehaviour
{
    public static int _whichLevelIsOn;
    public static bool _isLevelChanged = false;
    [SerializeField] private BermudaRunnerCharacter _character;
    [SerializeField] private EndOfLevelUI panel; 

    private void Start()
    {
        HCLevelManager.Instance.Init();
        HCLevelManager.Instance.GenerateCurrentLevel();
        _whichLevelIsOn = 0;
    }
    public void Continue()
    {
        HCLevelManager.Instance.LevelUp();
        HCLevelManager.Instance.GenerateCurrentLevel();
        _whichLevelIsOn++;
        _isLevelChanged = true;
        panel.HidePanel();
    }

}
