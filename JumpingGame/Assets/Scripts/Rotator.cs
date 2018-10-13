using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float rotateSpeed;
    public Vector3 axis;
	void Update () {
        transform.Rotate(axis *rotateSpeed* Time.deltaTime);
	}
}
