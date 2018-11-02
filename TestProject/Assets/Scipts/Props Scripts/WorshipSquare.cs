using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorshipSquare : MonoBehaviour {
    public GameObject particleSuccess;
    public GameObject particleFail;

    public obelState state { get; set; }

    private obeliskTypes objType = obeliskTypes.square;
    private bool result;
    private GameObject particle;

    void Start()
    {
        state = gameObject.GetComponent<obelState>();
    }

    void Update()
    {
        if (particle != null && !state.isActivated)
        {
            Destroy(particle);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!result)
            state.isActivated = false;
    }

    private void activate()
    {
        float fwdMove = Input.GetAxis("Vertical");
        float strafeMove = Input.GetAxis("Horizontal");
        if (fwdMove == 0 && strafeMove == 0)
        {
            if (!state.isActivated)
            {
                result = activationManager.activate(objType);
                if (result)
                {
                    particle = Instantiate(particleSuccess, gameObject.transform);
                }
                else
                    Instantiate(particleFail, gameObject.transform);
                state.isActivated = true;
            }
        }
    }
}
