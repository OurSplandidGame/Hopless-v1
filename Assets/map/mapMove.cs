using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMove : MonoBehaviour {

    public Transform characterTransfer;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - characterTransfer.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = offset + characterTransfer.position;
	}
}
