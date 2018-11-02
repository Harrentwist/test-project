using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plrAttack : MonoBehaviour
{
    public Transform weapon;

    private Animator anim;

	void Awake ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            lightAttack(Random.Range(0, 2));
        }

        if(anim.GetBool("isStrongAttackAvailable") && Input.GetKeyDown(KeyCode.Mouse1))
        {
            strongAttack();
        }
	}

    void lightAttack(int value)
    {
        if (value == 0)
        {
            anim.SetTrigger("isAttack1");
        }
        else
            anim.SetTrigger("isAttack2");

        anim.SetBool("isStrongAttackAvailable", true);
    }

    void strongAttack()
    {
        anim.SetTrigger("isAttackStrong");
    }

    void hitEvent()
    {
        weapon.gameObject.GetComponent<WeaponHit>().weaponHit();
    }
}
