using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        //services etc...
        SceneManager.LoadSceneAsync(1);
    }
}
