using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    private new string name;
    //public Text text;
    public InputField inputField;
    public void SaveName()
    {
        name = inputField.text;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartNew()
    {
        if (!String.IsNullOrEmpty(name))
        {
            File.WriteAllText(Application.persistentDataPath + "/name.txt", name);
            //File.WriteAllText(Application.persistentDataPath + "/score.txt", "0");
        }
        SceneManager.LoadScene(1);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
