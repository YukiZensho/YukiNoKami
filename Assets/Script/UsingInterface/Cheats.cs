using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public UISUTS u;
    public Inventory inventory;
    public Health health;
    public SaveFile save;
    public Camera Camera;


    public bool cheatOn,cheatAble;
    public string text;
    public char x;
    void Update()
    {
        if (cheatAble)
        {
            if (Input.GetKeyDown("s"))
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    save.Save();
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                cheatOn = !cheatOn;
                u.paused = cheatOn;
                u.position.text = "";
                text = "";
            }
            if (cheatOn)
            {
                u.paused = true;
                u.FrameRate.enabled = true;
                u.position.enabled = true;
                u.blockCount.enabled = false;
                u.plantCount.enabled = false;
                u.Hovers.enabled = false;
                u.HovPos.enabled = false;
                u.MousePos.enabled = false;
                u.FrameRate.text = text;
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.Backspace))
                        if (text.Length > 0)
                            text = text.Remove(text.Length - 1);
                    if ((x = WhatKey()) != (char)255)
                        text = text + x;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        try
                        {
                            Doer();
                        }
                        catch (System.Exception e)
                        {
                            u.position.text = e.Message;
                        }
                    }
                }
            }
        }
    }
    public void Doer() // The one who does
    {
        string t1 = text.Split(new char[] { ' ' })[0];
        if (t1 == "gettr" || t1 == "√1")
        {
            try{ string s= text.Split(new char[] { ' ' })[1];}
            catch{throw new System.ArgumentException("Gettr gives you stuff");}
            string t2 = text.Split(new char[] { ' ' })[1];
            try { int.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
            if (t2 == "?") u.position.text = "Gettr gives you stuff";
            else
            {
                try { string s = text.Split(new char[] { ' ' })[2]; }
                catch { throw new System.ArgumentException("This fuction takes two arguments"); }
                string t3 = text.Split(new char[] { ' ' })[2];
                try {int.Parse(t2);}catch{throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid");}
                try{int.Parse(t3);} catch{throw new System.ArgumentException((t3 != "") ? t3 + " is not a valid number" : "NULL is not valid");}
                inventory.Gettr(new Vector2((int)(float.Parse(t2)), (int)(float.Parse(t3))));
                u.position.text = "Got " + (int)(float.Parse(t3)) + " \"" + (int)(float.Parse(t2)) + "\"(s)";
            }
        }
        else if (t1 == "goto" || t1 == "√2")
        {
            try { string s = text.Split(new char[] { ' ' })[1]; }
            catch { throw new System.ArgumentException("goto teleports"); }
            string t2 = text.Split(new char[] { ' ' })[1];
            if (t2 == "?") u.position.text = "goto teleports";
            else
            {
                try { string s = text.Split(new char[] { ' ' })[2]; }
                catch { throw new System.ArgumentException("This fuction takes two arguments"); }
                string t3 = text.Split(new char[] { ' ' })[2];
                Vector3 pos;
                pos.x = 0;
                pos.y = 0;
                pos.z = 0;
                if (t2[0] == '√')
                {
                    t2 = t2.Substring(1);
                    try { float.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
                    pos.x = inventory.gameObject.transform.position.x + float.Parse(t2);
                    u.position.text = "Moved by " + float.Parse(t2) + "X ";
                }
                else
                {
                    try { float.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
                    pos.x = float.Parse(t2);
                    u.position.text = "Moved to " + float.Parse(t2) + "X ";
                }
                if (t3[0] == '√')
                {
                    t3 = t3.Substring(1);
                    try { float.Parse(t3); } catch { throw new System.ArgumentException((t3 != "") ? t3 + " is not a valid number" : "NULL is not valid"); }
                    pos.y = inventory.gameObject.transform.position.y + float.Parse(t3);
                    u.position.text += "and by " + float.Parse(t3) + "Y";
                }
                else
                {
                    try { float.Parse(t3); } catch { throw new System.ArgumentException((t3 != "") ? t3 + " is not a valid number" : "NULL is not valid"); }
                    pos.y = float.Parse(t3);
                    u.position.text += "and to " + float.Parse(t3) + "Y";
                }
                inventory.gameObject.transform.SetPositionAndRotation(pos, Quaternion.Euler(0f, 0f, 0f));

            }
        }
        else if (t1 == "view" || t1 == "√3")
        {
            try { string s = text.Split(new char[] { ' ' })[1]; }
            catch { throw new System.ArgumentException("view manages video settings"); }
            string t2 = text.Split(new char[] { ' ' })[1];
            if (t2 == "?") u.position.text = "view manages video settings";
            else
            {
                try { string s = text.Split(new char[] { ' ' })[2]; }
                catch { throw new System.ArgumentException("This fuction takes two arguments"); }
                string t3 = text.Split(new char[] { ' ' })[2];
                if (t2 == "mouse" || t2 == "√1")
                {
                    if (t3 == "1" || t3 == "true")
                    {
                        Cursor.visible = true;
                    }
                    if (t3 == "0" || t3 == "false")
                    {
                        Cursor.visible = false;
                    }
                    if (t3 == "!")
                    {
                        Cursor.visible = !Cursor.visible;
                    }
                    u.position.text = "Mouse was set to " + Cursor.visible;
                }
                if (t2 == "camsize" || t2 == "√2")
                {
                    if (t3 == "?" || t3 == "value")
                        u.position.text = "camsize = " + Camera.orthographicSize;
                    else
                    {
                        if (float.Parse(t3) % 15.0001f == 0)
                            Camera.orthographicSize = 3.5f;
                        else Camera.orthographicSize = float.Parse(t3) % 15.0001f;
                        u.position.text = "camsize = " + Camera.orthographicSize;
                    }
                }
                else
                {
                    u.position.text = "Nothing, yet";
                }
            }
        }
        else if (t1 == "rev" || t1 == "√4")
        {
            try { string s = text.Split(new char[] { ' ' })[1]; }
            catch { throw new System.ArgumentException("rev manages health"); }

            string t2 = text.Split(new char[] { ' ' })[1];
            if (t2 == "?") u.position.text = "rev manages health";
            else
            {
                try { float.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
                if (float.Parse(t2) < 0)
                {
                    health.Hurting(-(float.Parse(t2)));
                    u.position.text = "Damge: " + (-float.Parse(t2));
                }
                else
                {
                    health.HP += float.Parse(t2);
                    if (health.HP > 16) health.HP = 16;
                    u.position.text = "Revived: " + (float.Parse(t2));
                }
                health.Heartts();
            }
        }
        else if (t1 == "scale" || t1 == "√5")
        {
            try { string s = text.Split(new char[] { ' ' })[1]; }
            catch { throw new System.ArgumentException("scale ... :/"); }
            string t2 = text.Split(new char[] { ' ' })[1];
            if (t2 == "?") u.position.text = "scale ... :/";
            else
            {
                try { string s = text.Split(new char[] { ' ' })[2]; }
                catch { throw new System.ArgumentException("This fuction takes two arguments"); }
                string t3 = text.Split(new char[] { ' ' })[2];
                try { float.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
                try { float.Parse(t3); } catch { throw new System.ArgumentException((t3 != "") ? t3 + " is not a valid number" : "NULL is not valid"); }
                inventory.gameObject.transform.localScale = new Vector3(float.Parse(t2), float.Parse(t3), 0f);
                u.position.text = "Scaled " + (int)(float.Parse(t2)) + "." + (int)((float.Parse(t2) * 100) % 100) + " on X and " + (int)(float.Parse(t3)) + "." + (int)((float.Parse(t3) * 100) % 100) + " on Y";
            }
        }
        else if (t1 == "feed" || t1 == "√6")
        {
            try { string s = text.Split(new char[] { ' ' })[1]; }
            catch { throw new System.ArgumentException("feed manages food points"); }
            string t2 = text.Split(new char[] { ' ' })[1];
            if (t2 == "?") u.position.text = "feed manages food points";
            else
            {
                try { float.Parse(t2); } catch { throw new System.ArgumentException((t2 != "") ? t2 + " is not a valid number" : "NULL is not valid"); }
                health.HungCh((int)(float.Parse(t2)));
                u.position.text = "feeded: " + (float.Parse(t2));
                health.Hunger();
            }
        }
        else if(t1== "√")
        { u.position.text = "√ is used for shortcuts, try √1"; }
        else
        {
            text = "";
            u.position.text = "Function Not Found";
        }
        text = "";

    }


    public char WhatKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
            return 'a';
        else if (Input.GetKeyDown(KeyCode.B))
            return 'b';
        else if (Input.GetKeyDown(KeyCode.C))
            return 'c';
        else if (Input.GetKeyDown(KeyCode.D))
            return 'd';
        else if (Input.GetKeyDown(KeyCode.E))
            return 'e';
        else if (Input.GetKeyDown(KeyCode.F))
            return 'f';
        else if (Input.GetKeyDown(KeyCode.G))
            return 'g';
        else if (Input.GetKeyDown(KeyCode.H))
            return 'h';
        else if (Input.GetKeyDown(KeyCode.I))
            return 'i';
        else if (Input.GetKeyDown(KeyCode.J))
            return 'j';
        else if (Input.GetKeyDown(KeyCode.K))
            return 'k';
        else if (Input.GetKeyDown(KeyCode.L))
            return 'l';
        else if (Input.GetKeyDown(KeyCode.M))
            return 'm';
        else if (Input.GetKeyDown(KeyCode.N))
            return 'n';
        else if (Input.GetKeyDown(KeyCode.O))
            return 'o';
        else if (Input.GetKeyDown(KeyCode.P))
            return 'p';
        else if (Input.GetKeyDown(KeyCode.Q))
            return 'q';
        else if (Input.GetKeyDown(KeyCode.R))
            return 'r';
        else if (Input.GetKeyDown(KeyCode.S))
            return 's';
        else if (Input.GetKeyDown(KeyCode.T))
            return 't';
        else if (Input.GetKeyDown(KeyCode.U))
            return 'u';
        else if (Input.GetKeyDown(KeyCode.V))
            return 'v';
        else if (Input.GetKeyDown(KeyCode.W))
            return 'w';
        else if (Input.GetKeyDown(KeyCode.X))
            return 'x';
        else if (Input.GetKeyDown(KeyCode.Y))
            return 'y';
        else if (Input.GetKeyDown(KeyCode.Z))
            return 'z';
        else if (Input.GetKeyDown("0"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return ')';
            return '0';
        }
        else if (Input.GetKeyDown("1"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '!';
            return '1';
        }
        else if (Input.GetKeyDown("2"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '@';
            return '2';
        }
        else if (Input.GetKeyDown("3"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '#';
            return '3';
        }
        else if (Input.GetKeyDown("4"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '$';
            return '4';
        }
        else if (Input.GetKeyDown("5"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '%';
            return '5';
        }
        else if (Input.GetKeyDown("6"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '^';
            return '6';
        }
        else if (Input.GetKeyDown("7"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '&';
            return '7';
        }
        else if (Input.GetKeyDown("8"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '*';
            return '8';
        }
        else if (Input.GetKeyDown("9"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '(';
            return '9';
        }
        else if (Input.GetKeyDown(KeyCode.Space))
            return ' ';
        else if (Input.GetKeyDown(KeyCode.Tab))
            return '√';
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '_';
            return '-';
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '+';
            return '=';
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '[';
            return '{';
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return ']';
            return '}';
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return ':';
            return ';';
        }
        else if (Input.GetKeyDown(KeyCode.Quote))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '\"';
            return '\'';
        }
        else if (Input.GetKeyDown("\\"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '\\';
            return '|';
        }
        else if (Input.GetKeyDown(","))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '<';
            return ',';
        }
        else if (Input.GetKeyDown("."))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '>';
            return '.';
        }
        else if (Input.GetKeyDown("/"))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                return '?';
            return '/';
        }
        else
            return (char)255;
    }
}