using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	void Start () {
		
	}
	

	void Update () 
	{
		transform.Rotate(45 * Time.deltaTime, 0, 0);
	}
}
