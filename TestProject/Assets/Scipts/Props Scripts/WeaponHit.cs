using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    public GameObject particleSuccess;
    public GameObject particleFail;

    public bool isHit { get; set; }
    public obelState state { get; set; }

    private bool result;
    private obeliskTypes objType = obeliskTypes.jar;
    private GameObject particle;

    void Start()
    {
        isHit = false;
    }

    void Update()
    {
        if (particle != null && !result)
        {
            Destroy(particle);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        activate(collision);
    }

    private void activate(Collision collision)
    {
        if (isHit)
        {
            state = collision.gameObject.GetComponent<obelState>();
            if (collision.gameObject.tag == "Vase" && !state.isActivated)
            {
                result = activationManager.activate(objType);
                if (result)
                {
                    particle = Instantiate(particleSuccess, collision.transform);
                    state.isActivated = true;
                }
                else
                    Instantiate(particleFail, collision.transform);
            }
            isHit = false;
        }
    }

    public void weaponHit()
    {
        isHit = true;
    }


}
