using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISUTS : MonoBehaviour
{
    public GameObject TextBox,Ninjin,hotbar;
    public BlockPointer BlockPointer;
    public Text FrameRate,position,blockCount,plantCount,Hovers,HovPos,MousePos;

    public bool TextingBox,MainTextOpen,paused,advanced;
    public float LastFrame, framerate;
    public int treez;
    public void PrePlanter()
    {
        for (int i = 0; i < treez; i++)
            Ninjin.GetComponent<Inventory>().Placer(true, false, new Vector3(Random.Range(-treez, treez), Random.Range(-treez, treez)), Random.Range(13,16),0,0);
    
    }
    public void PlacedPrePlanter(int X0,int Y0,int X1, int Y1)
    {
        for (int i = 0; i < Random.Range(0, treez); i++)
            Ninjin.GetComponent<Inventory>().Placer(true, false, new Vector3(Random.Range(X0, X1), Random.Range(Y0, Y1)), Random.Range(13, 16), 0, 0);

    }

    void Update()
    {
        Ninjin.SetActive(!paused);
        if (!paused)
        {
            TextBox.SetActive(TextingBox);
            hotbar.SetActive(!advanced);
            if (MainTextOpen)
            {
                if(advanced)
                {
                    blockCount.enabled = true;
                    blockCount.text = GameObject.Find("BlockLocation").transform.childCount + " Blocks";
                    plantCount.enabled = true;
                    plantCount.text = GameObject.Find("Plants").transform.childCount + " Plants";
                    Hovers.enabled = true;
                    if (BlockPointer.Touching&& BlockPointer.touched.GetComponent<Thing>() as Thing)
                        Hovers.text = "Hovers " + BlockPointer.touched.GetComponent<Thing>().TheThing;
                    else
                        Hovers.text = "Hovers -1";
                    HovPos.enabled = true;
                    HovPos.text = "HovPos " + BlockPointer.transform.position.x+"X "+ BlockPointer.transform.position.y+"Y";
                    MousePos.enabled = true;
                    MousePos.text = "Mouse " + (Input.mousePosition.x-.5f)+" "+ Input.mousePosition.y;
                }
                FrameRate.enabled = true;
                position.enabled = true;
                float calltime = Time.time;
                framerate = 1 / (calltime - LastFrame);
                int t = (int)(framerate * 100);

               // FrameRate.text = t / 100 + "." + Mathf.Abs(t % 100) + " FPS";
                FrameRate.text = t / 100 +  " FPS";
                int x, y;
                x = (int)(Ninjin.transform.position.x * 1000);
                y = (int)(Ninjin.transform.position.y * 1000);
                position.text = x / 1000 + "." + Mathf.Abs(x % 1000) + " X " + y / 1000 + "." + Mathf.Abs(y % 1000) + " Y ";
                LastFrame = calltime;
            }
            else
            {
                blockCount.enabled = false;
                plantCount.enabled = false;
                FrameRate.enabled = false;
                position.enabled = false;
                Hovers.enabled = false;
                HovPos.enabled = false;
                MousePos.enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    advanced = !advanced;
                MainTextOpen = !MainTextOpen;
            }
        }
        if (Input.GetKeyDown(KeyCode.Pause))
            paused = !paused;
        if (Input.GetKeyDown(KeyCode.PageDown))
            paused = false;
        if (Input.GetKeyDown(KeyCode.PageUp))
            paused = true;
    }
}
