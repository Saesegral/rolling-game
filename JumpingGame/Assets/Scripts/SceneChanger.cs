using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void NextScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //Later consider remaking this so that it automatically takes you to the scene that matches the name of the button instead of taking an argument.

}
