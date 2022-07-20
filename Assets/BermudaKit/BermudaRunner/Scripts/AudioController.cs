using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.Play();
        }

        if (RunnerPlayer._isGameStarted)
        {
            sound.Play();
            RunnerPlayer._isGameStarted = false;
        }
    }
}
