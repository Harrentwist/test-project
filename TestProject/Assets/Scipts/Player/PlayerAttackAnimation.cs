using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimation : MonoBehaviour
{
    public Transform weapon;

    private Animator anim;

	void Awake ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack(Random.Range(0, 2));
        }

        if(anim.GetBool("isStrongAttackAvailable") && Input.GetKeyDown(KeyCode.Mouse1))
        {
            StrongAttack();
        }
	}

    void LightAttack(int value)
    {
        if (value == 0)
        {
            anim.SetTrigger("isAttack1");
        }
        else
            anim.SetTrigger("isAttack2");

        anim.SetBool("isStrongAttackAvailable", true);
    }

    void StrongAttack()
    {
        anim.SetTrigger("isAttackStrong");
    }

    void HitEvent()
    {
        weapon.gameObject.GetComponent<Weapon>().WeaponHit();
    }

    void EndHitEvent()
    {
        weapon.gameObject.GetComponent<Weapon>().EndWeaponHit();
    }

}
