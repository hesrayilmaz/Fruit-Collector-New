using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bermuda.Runner;

public class ChangeScene : MonoBehaviour
{
    public static int _whichLevelIsOn = 0;
 
    public void Continue()
    {
        transform.GetChild(_whichLevelIsOn).gameObject.SetActive(false);
        transform.GetChild(_whichLevelIsOn + 1).gameObject.SetActive(true);
        _whichLevelIsOn++;
    }

}
