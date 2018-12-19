using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InteractableJar : InteractableObject
{
    InteractableJar() : base(InteractableObjectType.jar)
    {

    }

    void Start()
    {
        GameManager.instance.OnWrongOrder.AddListener(ResetParticles);
        GameManager.instance.OnWeaponHit.AddListener(Activate);
    }

    void Update()
    {
        if(IsActivated)
        {
            GameManager.instance.OnWeaponHit.RemoveListener(Activate);
        }
    }

    protected override void ResetParticles()
    {
        base.ResetParticles();
        GameManager.instance.OnWeaponHit.RemoveAllListeners();
        GameManager.instance.OnWeaponHit.AddListener(Activate);
    }
}
