using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bermuda.Runner;

public class RunnerPlayer : MonoBehaviour
{
    [SerializeField] private BermudaRunnerCharacter _runnerCharacter;
    public static bool _isGameStarted = false;
    public static bool _isGameOver = false;
    [SerializeField] private GameObject _startButton;

    void Start()
    {
        _runnerCharacter.IdleAnimation();
        Debug.Log("idle");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _runnerCharacter.StartToRun();
        }

       /* if (_isGameStarted)
        {
            _runnerCharacter.StartToRun();
        }*/

        if (ChangeScene._isLevelChanged)
        {
            _runnerCharacter.StartToRun();
            ChangeScene._isLevelChanged = false;
        }
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _runnerCharacter.StartToRun();
        _startButton.SetActive(false);
    }
}
