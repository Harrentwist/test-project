using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InteractableSquare : InteractableObject
{
    InteractableSquare() : base(InteractableObjectType.square)
    {

    }

    public GameObject player;

    private PlayerMovement movement;
    private bool isActivationFailed = false;

    void Start()
    {
        GameManager.instance.OnWrongOrder.AddListener(ResetParticles);
        Debug.Log(particle);
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (movement.IsPlayerMoving == false && IsActivated == false)
        {
            Activate();
            if (IsActivated == false)
            {
                IsActivated = true;
                isActivationFailed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(isActivationFailed)
        {
            IsActivated = false;
            isActivationFailed = false;
        }
    }
}
