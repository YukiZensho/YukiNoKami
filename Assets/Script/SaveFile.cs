using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public UISUTS ui;
    public GameObject BlockFather,PlantFather, ninjin;

    public string docPath;
    private void Start()
    {
        docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        docPath += "\\God's Plan";
        Debug.Log(docPath);
        if (!File.Exists(docPath))
        {
            System.IO.Directory.CreateDirectory(docPath);

        }
        if(!File.Exists(docPath+ "\\NiseJin"))
        {
            Debug.Log("Non-PreExistent");
            ui.PrePlanter();
        }
        {
            StreamReader fileR = new StreamReader(Path.Combine(docPath, "NiseJin"));
            //Location,scale,,itemcount,Thing,quant,collider(bool)
            //pos
            {
                string text = fileR.ReadLine();
                //Debug.Log(text);
                string x = text.Split(new char[] { ' ' })[0];
                string y = text.Split(new char[] { ' ' })[1];
                ninjin.transform.position = new Vector3(float.Parse(x), float.Parse(y));
            }
            //scale
            {
                string text = fileR.ReadLine();
                string x = text.Split(new char[] { ' ' })[0];
                string y = text.Split(new char[] { ' ' })[1];
                ninjin.transform.localScale = new Vector3(float.Parse(x), float.Parse(y));
            }
            //health&hunger
            {
                string text = fileR.ReadLine();
                string x = text.Split(new char[] { ' ' })[0];
                string y = text.Split(new char[] { ' ' })[1];
                ninjin.GetComponent<Health>().HP = float.Parse(x);
                ninjin.GetComponent<Health>().SP = float.Parse(y);
                ninjin.GetComponent<Health>().Heartts();
                ninjin.GetComponent<Health>().Hunger();
            }
            //CraftSlots
            {
                {
                    string text = fileR.ReadLine();
                    string x = text.Split(new char[] { ' ' })[0];
                    string y = text.Split(new char[] { ' ' })[1];
                    ninjin.GetComponent<Inventory>().CT[0] = int.Parse(x);
                    ninjin.GetComponent<Inventory>().CQ[0] = int.Parse(y);
                }
                {
                    string text = fileR.ReadLine();
                    string x = text.Split(new char[] { ' ' })[0];
                    string y = text.Split(new char[] { ' ' })[1];
                    ninjin.GetComponent<Inventory>().CT[1] = int.Parse(x);
                    ninjin.GetComponent<Inventory>().CQ[1] = int.Parse(y);
                }
                {
                    string text = fileR.ReadLine();
                    string x = text.Split(new char[] { ' ' })[0];
                    string y = text.Split(new char[] { ' ' })[1];
                    ninjin.GetComponent<Inventory>().CT[2] = int.Parse(x);
                    ninjin.GetComponent<Inventory>().CQ[2] = int.Parse(y);
                }
            }
            //InvSave
            {
                for (int i = 0; i < ninjin.GetComponent<Inventory>().Item.Length; i++)
                {
                    string text = fileR.ReadLine();
                    string x = text.Split(new char[] { ' ' })[0];
                    string y = text.Split(new char[] { ' ' })[1];
                    ninjin.GetComponent<Inventory>().Item[i].x = int.Parse(x);
                    ninjin.GetComponent<Inventory>().Item[i].y = int.Parse(y);

                }
                {
                    string text = fileR.ReadLine();
                    string x = text.Split(new char[] { ' ' })[0];
                    string y = text.Split(new char[] { ' ' })[1];
                    ninjin.GetComponent<Inventory>().GT = int.Parse(x);
                    ninjin.GetComponent<Inventory>().GQ = int.Parse(y);

                }
            }
            //BlockPlacement
            {
                Inventory inv=ninjin.GetComponent<Inventory>();
                string text = fileR.ReadLine();
                string q = text.Split(new char[] { ' ' })[0];
                for(int i=0;i<int.Parse(q);i++)
                {
                    string line = fileR.ReadLine();
                    //Debug.Log(line);
                    string t = line.Split(new char[] { ' ' })[0];
                    string x = line.Split(new char[] { ' ' })[1];
                    string y = line.Split(new char[] { ' ' })[2];
                    string a = line.Split(new char[] { ' ' })[3];
                    string flX = line.Split(new char[] { ' ' })[4];
                    string flY = line.Split(new char[] { ' ' })[5];
                    inv.Placer((int.Parse(a)==1),false,new Vector3(int.Parse(x),int.Parse(y)),int.Parse(t),int.Parse(flX),int.Parse(flY));
                }
            }
            //PlantPlacement
            {
                Inventory inv = ninjin.GetComponent<Inventory>();
                string text = fileR.ReadLine();
                string q = text.Split(new char[] { ' ' })[0];
                for (int i = 0; i < int.Parse(q); i++)
                {
                    string line = fileR.ReadLine();
//                    Debug.Log(line);
                    string t = line.Split(new char[] { ' ' })[0];
                    string x = line.Split(new char[] { ' ' })[1];
                    string y = line.Split(new char[] { ' ' })[2];
                    string nm = line.Split(new char[] { ' ' })[3];
                    string cat = line.Split(new char[] { ' ' })[4];
                    string fr = line.Split(new char[] { ' ' })[5];
                    if (cat == "t")
                    {
                        GameObject o = Instantiate(inv.Tree, GameObject.Find("Plants").transform);
                        o.transform.position = new Vector3(int.Parse(x), int.Parse(y));
                        o.GetComponent<Thing>().TheThing = int.Parse(t);
                        o.GetComponent<Tree>().nMax = float.Parse(nm);
                        o.GetComponent<Tree>().chstart = -(float.Parse(fr));
                        if (-(float.Parse(fr)) != 0)
                            o.GetComponent<Tree>().grown = true;
                    }
                }
            }
            /*{
                Inventory inv=ninjin.GetComponent<Inventory>();
                string txt = fileR.ReadLine();
                string text = fileR.ReadLine();
                string q = text.Split(new char[] { ' ' })[0];
                for(int i=0;i<int.Parse(q);i++)
                {
                    string line = fileR.ReadLine();
                    Debug.Log(line);
                    string t = line.Split(new char[] { ' ' })[0];
                    string x = line.Split(new char[] { ' ' })[1];
                    string y = line.Split(new char[] { ' ' })[2];
                    string c = line.Split(new char[] { ' ' })[3];//category
                    string nm = line.Split(new char[] { ' ' })[4];//nMax
                    string ch = line.Split(new char[] { ' ' })[5];//Ch
                    GameObject o;
                    if (c == "t")
                        o = Instantiate(inv.Tree,GameObject.Find("Plants").transform);
                    else
                        o = Instantiate(inv.Tree, GameObject.Find("Plants").transform);
                    o.transform.position = new Vector3(int.Parse(x),int.Parse(y));
                    o.GetComponent<Thing>().TheThing = int.Parse(t);
                    o.GetComponent<Tree>().nMax = int.Parse(nm);
                    o.GetComponent<Tree>().chtime = int.Parse(ch);

                }
            }*/
        }
    }
    public void Save()
    {
        //File.Delete(docPath + "SuruvaivuruOverworld");
        using (StreamWriter fileW = new StreamWriter(Path.Combine(docPath, "NiseJin")))
        {

            fileW.Write(ninjin.transform.position.x + " " + ninjin.transform.position.y + " " + "\n");//pos
            fileW.Write(ninjin.transform.localScale.x + " " + ninjin.transform.localScale.y + " " + "\n");//scale
            fileW.Write(ninjin.GetComponent<Health>().HP + " "+ ninjin.GetComponent<Health>().SP + "\n");//Hp&Sp
            //CraftSlots
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[0] + " " + (int)ninjin.GetComponent<Inventory>().CQ[0] + "\n");
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[1] + " " + (int)ninjin.GetComponent<Inventory>().CQ[1] + "\n");
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[2] + " " + (int)ninjin.GetComponent<Inventory>().CQ[2] + "\n");
            //Inventory
            for (int i = 0; i < ninjin.GetComponent<Inventory>().Item.Length;i++) 
            {
                fileW.Write((int)ninjin.GetComponent<Inventory>().Item[i].x+" "+ (int)ninjin.GetComponent<Inventory>().Item[i].y+"\n");
            }
            fileW.Write((int)ninjin.GetComponent<Inventory>().GT + " " + (int)ninjin.GetComponent<Inventory>().GQ + "\n");
            fileW.Write(BlockFather.transform.childCount + "\n");
            //Blocks
            for (int i = 0; i < BlockFather.transform.childCount; i++)
            {
                fileW.Write(BlockFather.transform.GetChild(i).GetComponent<Thing>().TheThing + " " + BlockFather.transform.GetChild(i).transform.position.x + " " + BlockFather.transform.GetChild(i).transform.position.y);
                if (BlockFather.transform.GetChild(i).GetComponent<Collider2D>().enabled)
                    fileW.Write(" 1");
                else fileW.Write(" 0");
                if (BlockFather.transform.GetChild(i).GetComponent<SpriteRenderer>().flipX)
                    fileW.Write(" 1");
                else fileW.Write(" 0");
                if (BlockFather.transform.GetChild(i).GetComponent<SpriteRenderer>().flipY)
                    fileW.Write(" 1\n");
                else fileW.Write(" 0\n");
            }
            //Plants
            fileW.Write(PlantFather.transform.childCount+"\n");
            for(int i = 0;i<PlantFather.transform.childCount;i++)
            {
                fileW.Write(PlantFather.transform.GetChild(i).GetComponent<Thing>().TheThing + " " + PlantFather.transform.GetChild(i).transform.position.x + " " + PlantFather.transform.GetChild(i).transform.position.y+" ");
                Tree t= PlantFather.transform.GetChild(i).GetComponent<Tree>();
                if (t.nMax - t.deltat < 0)
                { 
                    fileW.Write("1 t ");
                    fileW.Write(t.chtime+"\n");
                }
                else
                    fileW.Write((t.nMax - t.deltat) + " t 0\n");

            }
            /*
            fileW.Write(PlantFather.transform.childCount + "\n");
            for (int i = 0; i < PlantFather.transform.childCount; i++)
            {
                fileW.Write(PlantFather.transform.GetChild(i).GetComponent<Thing>().TheThing + " " + PlantFather.transform.GetChild(i).transform.position.x + " " + PlantFather.transform.GetChild(i).transform.position.y+ " ");
                if(PlantFather.transform.GetChild(i).GetComponent<Tree>() as Tree)
                {
                    fileW.Write("t ");
                    if (PlantFather.transform.GetChild(i).GetComponent<Tree>().nMax - PlantFather.transform.GetChild(i).GetComponent<Tree>().deltat>0)
                    {
                        fileW.Write((PlantFather.transform.GetChild(i).GetComponent<Tree>().nMax - PlantFather.transform.GetChild(i).GetComponent<Tree>().deltat)+" ");
                    }
                    else
                    {
                        fileW.Write("1 ");
                    }
                    fileW.Write(Math.Abs(PlantFather.transform.GetChild(i).GetComponent<Tree>().chtime- PlantFather.transform.GetChild(i).GetComponent<Tree>().start) + "\n");
                }
            }
            */
        }
    }
    private void OnApplicationQuit()
    {

        Save();
    }
}
