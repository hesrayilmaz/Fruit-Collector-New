using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bermuda.Runner;

public class RunnerPlayer : MonoBehaviour
{
    [SerializeField] private BermudaRunnerCharacter _runnerCharacter;

    void Start()
    {
        _runnerCharacter.IdleAnimation();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _runnerCharacter.StartToRun();
        }

        if (ChangeScene._isLevelChanged)
        {
            _runnerCharacter.IdleAnimation();
            ChangeScene._isLevelChanged = false;
        }
    }
}
