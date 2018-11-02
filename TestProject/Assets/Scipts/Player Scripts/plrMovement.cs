using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plrMovement : MonoBehaviour {
    private Animator anim;

	void Awake ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {

        float movHor = Input.GetAxis("Horizontal");
        float movVert = Input.GetAxis("Vertical");

        Move(movHor, movVert);

	}

    private void Move(float hor, float vert)
    {
        if(hor != 0 || vert != 0)
        {
            anim.SetFloat("fwdMovement", vert);
            anim.SetFloat("strafeMovement", hor);
        }

    }
}
