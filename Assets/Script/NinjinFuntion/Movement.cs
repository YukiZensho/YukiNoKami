using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed,swingtime;
    public int dir;// > 0        < 1      /\ 2     \/ 3
    public Animator anim;
    void Update()
    {
        if (!gameObject.GetComponent<Inventory>().PapyrusOpen)
        {
            gameObject.transform.Translate(new Vector3(Input.GetAxis("Xx") * Time.deltaTime * speed*(gameObject.transform.localScale.x+.01f), Input.GetAxis("Yy") * Time.deltaTime * speed * (gameObject.transform.localScale.y + .01f)));
            if (Input.GetAxis("Xx") != 0f || Input.GetAxis("Yy") != 0f)
            {
                anim.SetFloat("Xx", Input.GetAxis("Xx"));
                anim.SetFloat("Yy", Input.GetAxis("Yy"));
                anim.SetFloat("Moving", 1);
                gameObject.GetComponent<Health>().SP -= 0.001f;
            }
            else
            {
                anim.SetFloat("Moving", 0);
            }
            if (swingtime > 0)
            {
                anim.SetFloat("Moving", 2);
                swingtime -= 1;
            }
            if (Input.GetMouseButtonDown(0))
            {
                swingtime = 21;
                gameObject.GetComponent<Health>().SP -= 0.01f;
            }
            dir = (Input.GetAxis("Xx") > .1f) ?0:(Input.GetAxis("Xx") < -.1f) ?1:(Input.GetAxis("Yy") > .1f) ?2:(Input.GetAxis("Yy") < -.1f) ?3:dir;
        }
    }
}
