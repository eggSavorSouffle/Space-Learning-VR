using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneClick : MonoBehaviour
{
    public string SceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
