using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuThings : MonoBehaviour
{
    public string docPath;
    private void Start()
    {

        docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        docPath += "\\God's Plan";
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Delete()
    {
        Directory.Delete(docPath,true);
    }
    public void Open()
    {
        System.Diagnostics.Process.Start("explorer.exe", @docPath);
    }
    public void LoadScene(int n)
    {
        SceneManager.LoadSceneAsync(n);
    }
}
