using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {
    public GameObject player;
    public Vector3 RelativePos { get; set; }
    // Use this for initialization
    void Start () {
        transform.position = player.transform.position;
        RelativePos = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
        
        transform.position = player.transform.position + RelativePos;
    }
}
