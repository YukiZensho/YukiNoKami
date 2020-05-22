using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ChunkSaveRead : MonoBehaviour
{
    public UISUTS UISUTS;
    public Cheats cheats;
    public Camera cam;

    public GameObject BlockFather, PlantFather,ninjin;

    public string docPath;//square
    public int pageX,pageY,PageSize;
    public int OldX, OldY;
    public bool Inited;
    private void Start()
    {
        docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        docPath += "\\God's Plan"; 
        Debug.Log(docPath);
        if (!File.Exists(docPath))
        {
            System.IO.Directory.CreateDirectory(docPath);

        }
        pageX = (int)(ninjin.transform.position.x / PageSize);
        pageX -= (ninjin.transform.position.x < 0) ? 1 : 0;
        pageY = (int)(ninjin.transform.position.y / PageSize);
        pageY -= (ninjin.transform.position.y < 0) ? 1 : 0;
        BigLoad(pageX, pageY);
        if (!File.Exists(docPath + "\\NiseJin"))
        {
            Debug.Log("Non-PreExistent");
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
                string c = text.Split(new char[] { ' ' })[2];
                string ct = text.Split(new char[] { ' ' })[3];
                ninjin.GetComponent<Health>().HP = float.Parse(x);
                ninjin.GetComponent<Health>().SP = float.Parse(y);
                cam.orthographicSize = float.Parse(c);
                ninjin.GetComponent<Health>().Heartts();
                ninjin.GetComponent<Health>().Hunger();
                cheats.cheatAble = bool.Parse(ct);
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

        }
    }
    public void BigLoad(int x,int y)
    {
        for (int i = x-1; i <=x+1; i++)
            for (int j = y-1; j <=y+1; j++)
                LoadPage(i, j);
    }
    public void BigUnLoad(int x,int y)
    {
        for (int i = x-1; i <=x+1; i++)
            for (int j = y-1; j <=y+1; j++)
                UnLoadPage(i, j);
    }
    public void Reinit()
    {
        Debug.Log("Reinit");
        if (pageY == OldY)
        {
            if (pageX < OldX)
            {
                UnLoadPage(pageX + 1, pageY + 1);
                UnLoadPage(pageX + 1, pageY);
                UnLoadPage(pageX + 1, pageY - 1);
                LoadPage(pageX - 1, pageY + 1);
                LoadPage(pageX - 1, pageY);
                LoadPage(pageX - 1, pageY - 1);
            }
            else if (pageX > OldX)
            {
                UnLoadPage(pageX - 1, pageY + 1);
                UnLoadPage(pageX - 1, pageY);
                UnLoadPage(pageX - 1, pageY - 1);
                LoadPage(pageX + 1, pageY + 1);
                LoadPage(pageX + 1, pageY);
                LoadPage(pageX + 1, pageY - 1);
            }
        }
        else if(OldX==pageX)
        {

            if (pageY < OldY)
            {
                UnLoadPage(pageX + 1, pageY + 1);
                UnLoadPage(pageX, pageY + 1);
                UnLoadPage(pageX - 1, pageY + 1);
                LoadPage(pageX + 1, pageY - 1);
                LoadPage(pageX    , pageY - 1);
                LoadPage(pageX - 1, pageY - 1);
            }
            else if (pageY > OldY)
            {
                UnLoadPage(pageX + 1, pageY - 1);
                UnLoadPage(pageX, pageY - 1);
                UnLoadPage(pageX - 1, pageY - 1);
                LoadPage(pageX + 1, pageY + 1);
                LoadPage(pageX    , pageY + 1);
                LoadPage(pageX - 1, pageY + 1);
            }
        }
        else
        {
            if(pageX<OldX)
            {
                if(pageY<OldY)
                {
                    UnLoadPage(pageX + 1, pageY + 1);
                    UnLoadPage(pageX + 1, pageY);
                    UnLoadPage(pageX + 1, pageY - 1);
                    UnLoadPage(pageX, pageY - 1);
                    UnLoadPage(pageX - 1, pageY - 1);
                    LoadPage(pageX - 1, pageY+1);
                    LoadPage(pageX - 1, pageY);
                    LoadPage(pageX - 1, pageY-1);
                    LoadPage(pageX, pageY-1);
                    LoadPage(pageX + 1, pageY-1);
                }
                else if(pageY>OldY)
                {
                    UnLoadPage(pageX - 1, pageY + 1);
                    UnLoadPage(pageX - 1, pageY);
                    UnLoadPage(pageX - 1, pageY - 1);
                    UnLoadPage(pageX, pageY - 1);
                    UnLoadPage(pageX + 1, pageY - 1);
                    LoadPage(pageX + 1, pageY+1);
                    LoadPage(pageX + 1, pageY);
                    LoadPage(pageX + 1, pageY-1);
                    LoadPage(pageX, pageY-1);
                    LoadPage(pageX - 1, pageY-1);
                }
            }
            else if(pageX>OldX)
            {
                if(pageY<OldY)
                {
                    UnLoadPage(pageX + 1, pageY + 1);
                    UnLoadPage(pageX + 1, pageY);
                    UnLoadPage(pageX + 1, pageY - 1);
                    UnLoadPage(pageX, pageY - 1);
                    UnLoadPage(pageX - 1, pageY - 1);
                    LoadPage(pageX - 1, pageY+1);
                    LoadPage(pageX - 1, pageY);
                    LoadPage(pageX - 1, pageY-1);
                    LoadPage(pageX, pageY-1);
                    LoadPage(pageX + 1, pageY-1);
                }
                else if(pageY>OldY)
                {
                    UnLoadPage(pageX - 1, pageY + 1);
                    UnLoadPage(pageX - 1, pageY);
                    UnLoadPage(pageX - 1, pageY - 1);
                    UnLoadPage(pageX, pageY - 1);
                    UnLoadPage(pageX + 1, pageY - 1);
                    LoadPage(pageX + 1, pageY+1);
                    LoadPage(pageX + 1, pageY);
                    LoadPage(pageX + 1, pageY-1);
                    LoadPage(pageX, pageY-1);
                    LoadPage(pageX - 1, pageY-1);
                }
            }
        }
    }
    public void LoadPage(int X,int Y)
    {
        if (!File.Exists(docPath + "\\" + X + " " + Y))
        {
            Debug.Log("Non-Existent" + X + " " + Y);
            UISUTS.PlacedPrePlanter(PageSize * X, PageSize * Y, (PageSize * X) + PageSize, (PageSize * Y) + PageSize);
        }
        else
        {
            Debug.Log("Existent" + X + " " + Y);
            StreamReader fileR = new StreamReader(Path.Combine(docPath, X + " " + Y));
            //BlockPlacement
            {
                Inventory inv = ninjin.GetComponent<Inventory>();
                string text = fileR.ReadLine(),line,old="";
                string q = text.Split(new char[] { ' ' })[0];
                for (int i = 0; i < int.Parse(q); i++)
                {
                    line = fileR.ReadLine();
                    if (old == line)
                        continue;
                    old = line;
                    //Debug.Log(line);
                    string t = line.Split(new char[] { ' ' })[0];
                    string x = line.Split(new char[] { ' ' })[1];
                    string y = line.Split(new char[] { ' ' })[2];
                    string a = line.Split(new char[] { ' ' })[3];
                    string flX = line.Split(new char[] { ' ' })[4];
                    string flY = line.Split(new char[] { ' ' })[5];
                    inv.Placer((int.Parse(a) == 1), false, new Vector3(int.Parse(x), int.Parse(y)), int.Parse(t), int.Parse(flX), int.Parse(flY));
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
        }
    }
    public void UnLoadPage(int X,int Y)
    {
        Debug.Log("Unloading"+X + " " + Y);
        using (StreamWriter fileW = new StreamWriter(Path.Combine(docPath, X + " " + Y)))
        {
            int q = 0;
            for (int i = 0; i < BlockFather.transform.childCount; i++)
            {
                if (BlockFather.transform.GetChild(i).transform.position.x > X * PageSize && BlockFather.transform.GetChild(i).transform.position.x < (X * PageSize) + PageSize)
                    if (BlockFather.transform.GetChild(i).transform.position.y > Y * PageSize && BlockFather.transform.GetChild(i).transform.position.y < (Y * PageSize) + PageSize)
                        q+=1;
            }
            fileW.Write(q + "\n");
            //Blocks
            for (int i = 0; i < BlockFather.transform.childCount; i++)
            {
                if (false)
                {
                    StreamReader fileR = new StreamReader(Path.Combine(docPath, X + " " + Y));
                    string t="";
                    StreamReader r = new StreamReader("file_path");
                    while (r.EndOfStream == false)
                    {
                        t = r.ReadLine();
                    }
                    Debug.Log((float.Parse(t.Split(new char[] { ' ' })[1]) - BlockFather.transform.GetChild(i).transform.position.x) + " " + (float.Parse(t.Split(new char[] { ' ' })[2]) - BlockFather.transform.GetChild(i).transform.position.y));
                    if (float.Parse(t.Split(new char[] { ' ' })[1]) == BlockFather.transform.GetChild(i).transform.position.x && float.Parse(t.Split(new char[] { ' ' })[2]) == BlockFather.transform.GetChild(i).transform.position.y)
                        continue;

                }
                if (BlockFather.transform.GetChild(i).transform.position.x > X * PageSize && BlockFather.transform.GetChild(i).transform.position.x <= (X * PageSize)+PageSize)
                    if (BlockFather.transform.GetChild(i).transform.position.y > Y * PageSize && BlockFather.transform.GetChild(i).transform.position.y <= (Y * PageSize) + PageSize)
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
            }
            q = 0;
            for (int i = 0; i < PlantFather.transform.childCount; i++)
            {
                if (PlantFather.transform.GetChild(i).transform.position.x > X * PageSize && PlantFather.transform.GetChild(i).transform.position.x < (X * PageSize) + PageSize)
                    if (PlantFather.transform.GetChild(i).transform.position.y > Y * PageSize && PlantFather.transform.GetChild(i).transform.position.y < (Y * PageSize) + PageSize)
                        q += 1;
            }
            //Plants
            fileW.Write(q + "\n");
            for (int i = 0; i < PlantFather.transform.childCount; i++)
            {
                if (false) {
                    StreamReader fileR = new StreamReader(Path.Combine(docPath, X + " " + Y));
                    string t = "";
                    StreamReader r = new StreamReader("file_path");
                    while (r.EndOfStream == false)
                    {
                        t = r.ReadLine();
                    }
                    Debug.Log((float.Parse(t.Split(new char[] { ' ' })[1]) - BlockFather.transform.GetChild(i).transform.position.x) + " " + (float.Parse(t.Split(new char[] { ' ' })[2]) - BlockFather.transform.GetChild(i).transform.position.y));
                    if (float.Parse(t.Split(new char[] { ' ' })[1]) == BlockFather.transform.GetChild(i).transform.position.x && float.Parse(t.Split(new char[] { ' ' })[2]) == BlockFather.transform.GetChild(i).transform.position.y)
                        continue;
                }
                if (PlantFather.transform.GetChild(i).transform.position.x > X * PageSize && PlantFather.transform.GetChild(i).transform.position.x <= (X * PageSize) + PageSize)
                    if (PlantFather.transform.GetChild(i).transform.position.y > Y * PageSize && PlantFather.transform.GetChild(i).transform.position.y <= (Y * PageSize) + PageSize)
                    {
                        fileW.Write(PlantFather.transform.GetChild(i).GetComponent<Thing>().TheThing + " " + PlantFather.transform.GetChild(i).transform.position.x + " " + PlantFather.transform.GetChild(i).transform.position.y + " ");
                        Tree t = PlantFather.transform.GetChild(i).GetComponent<Tree>();
                        if (t.nMax - t.deltat < 0)
                        {
                            fileW.Write("1 t ");
                            fileW.Write(t.chtime + "\n");
                        }
                        else
                            fileW.Write((t.nMax - t.deltat) + " t 0\n");
                    }
            }
        }
    }
    public void Save()
    {
        //Undebugged
        using (StreamWriter fileW = new StreamWriter(Path.Combine(docPath, "NiseJin")))
        {
            fileW.Write(ninjin.transform.position.x + " " + ninjin.transform.position.y + " " + "\n");//pos
            fileW.Write(ninjin.transform.localScale.x + " " + ninjin.transform.localScale.y + " " + "\n");//scale
            fileW.Write(ninjin.GetComponent<Health>().HP + " " + ninjin.GetComponent<Health>().SP + " " + cam.orthographicSize +" "+cheats.cheatAble+ "\n");//Hp&Sp
            //CraftSlots
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[0] + " " + (int)ninjin.GetComponent<Inventory>().CQ[0] + "\n");
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[1] + " " + (int)ninjin.GetComponent<Inventory>().CQ[1] + "\n");
            fileW.Write((int)ninjin.GetComponent<Inventory>().CT[2] + " " + (int)ninjin.GetComponent<Inventory>().CQ[2] + "\n");
            //Inventory
            for (int i = 0; i < ninjin.GetComponent<Inventory>().Item.Length; i++)
            {
                fileW.Write((int)ninjin.GetComponent<Inventory>().Item[i].x + " " + (int)ninjin.GetComponent<Inventory>().Item[i].y + "\n");
            }
            fileW.Write((int)ninjin.GetComponent<Inventory>().GT + " " + (int)ninjin.GetComponent<Inventory>().GQ + "\n");
        }
        BigUnLoad(pageX, pageY);
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    void Update()
    {
        pageX = (int)(ninjin.transform.position.x / PageSize);
        pageX -= (ninjin.transform.position.x<0) ?1:0;
        pageY = (int)(ninjin.transform.position.y / PageSize);
        pageY -= (ninjin.transform.position.y < 0) ? 1 : 0;
        if (OldX != pageX || OldY != pageY)
        {
            if (Mathf.Abs(OldX - pageX) <= 1 || Mathf.Abs(OldY - pageY) <= 1)
                Reinit();
            else
            {
                BigLoad(pageX, pageY);
                BigUnLoad(OldX, OldY);
            }
        }
        OldX = pageX;
        OldY = pageY;
    }
}
