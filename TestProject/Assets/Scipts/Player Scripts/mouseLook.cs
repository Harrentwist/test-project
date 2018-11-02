using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    [SerializeField] private float horSens;

	// Use this for initialization
	void Start ()
    {
        horSens = 6.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float horInput = Input.GetAxis("Mouse X") * horSens;
        transform.Rotate(new Vector3(0, horInput, 0));
	}
}
