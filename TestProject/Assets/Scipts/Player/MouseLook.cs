using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float horSens;

	void Start ()
    {
        horSens = 6.0f;
	}
	
	void Update ()
    {
        float horInput = Input.GetAxis("Mouse X") * horSens;
        transform.Rotate(new Vector3(0, horInput, 0));
	}
}
