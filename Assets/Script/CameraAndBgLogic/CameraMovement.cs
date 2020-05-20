using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public UISUTS u;
    public GameObject Target,grass,Grassblock;
    public Vector3 TargetedPos;
    public int Speed,Si,Sj;
    private void Start()
    {
        for(int i=0;i<Si;i++)
        {
            for(int j=0;j<Sj;j++)
            {
                GameObject t;
                t = Instantiate(Grassblock);
                t.transform.SetParent(grass.transform);
                t.transform.localPosition = new Vector3(j, i);
            }
        }
    }
    void Update()
    {
        if (!u.paused&&!Target.GetComponent<Inventory>().PapyrusOpen)
        {
            TargetedPos = Vector3.Lerp(transform.position, new Vector3(Target.transform.position.x + ((Input.mousePosition.x - Screen.width / 2) / 100), Target.transform.position.y + ((Input.mousePosition.y - Screen.height / 2) / 105)), Time.deltaTime * Speed);
            transform.position = new Vector3(TargetedPos.x, TargetedPos.y, -10);
            grass.transform.position = new Vector3(Mathf.Floor(gameObject.transform.position.x - (Sj/2)), Mathf.Floor(gameObject.transform.position.y -2- (Si/2)));
        }
        else
        {
            TargetedPos = Vector3.Lerp(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y), Time.deltaTime * Speed);
            transform.position = new Vector3(TargetedPos.x, TargetedPos.y, -10);
            grass.transform.position = new Vector3(Mathf.Floor(gameObject.transform.position.x - (Sj / 2)), Mathf.Floor(gameObject.transform.position.y -2- (Si / 2)));
        }
    }
}
