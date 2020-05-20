using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicFunc : MonoBehaviour
{
    public int clock,scene;
    public GameObject NoEscape;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(691, 553,false);
    }

    [System.Obsolete]
    private void OnApplicationQuit()
    {
        if (scene > 4)
        { 
            SaveFile saveFile;
            saveFile = GameObject.Find("_SUTS").GetComponent<SaveFile>();
            saveFile.Save();
        }
        if(!Input.GetKey(KeyCode.End))
            Application.CancelQuit();
    }
    // Update is called once per frame
    void Update()
    {
        if (clock < Mathf.Pow(10, 5)) 
        clock += 1;
        else clock = 1;
        if (Input.GetKeyDown(KeyCode.Return))
            if (scene == 0) SceneManager.LoadScene((scene++)+1);
        if (Input.GetKeyDown(KeyCode.F4))
            Screen.fullScreen = !Screen.fullScreen;
        if (Input.GetKeyDown(KeyCode.End))
            Application.Quit();
        if (scene > 0)
            NoEscape.GetComponent<Image>().enabled = Input.GetKey(KeyCode.Escape);

    }
}
