using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public int secondsPerDay = 1;
    public GameObject[] trans;
    public GameObject bossObj;
    public AudioClip[] backGroundMuisc;
    private AudioSource audio;
    private float speed;
    // Use this for initialization
    private float savedSpeed;
    private bool day;
    private bool bossFlag;


	void Start () {
        speed = 360.0f / secondsPerDay / 60.0f;
        day = true;
        bossFlag = false;
        audio = GetComponent<AudioSource>();
        audio.clip = backGroundMuisc[0];
        audio.Play();
    }

    //
    public void Dawn()
    {
        transform.rotation = Quaternion.Euler(350, 0, 0);
        speed = savedSpeed;
        savedSpeed = 0;
    }

	// Update is called once per frame
	void Update ()
    {
        float angle;
        Vector3 v;
        transform.rotation.ToAngleAxis(out angle, out v);

        if(angle > 182 && angle < 187 && day)
        {
            print("-----------------------------night---------------------------------");
            audio.clip = backGroundMuisc[1];
            audio.Play();
            GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (GameObject e in spawner)
            {
                e.GetComponent<Spawner>().enabled = false;
            }
            //gg.GetComponent<Spawner>().enable = false;
            GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
            foreach (GameObject e in animals)
            {
                Transform t = e.transform;
                Vector3 pos = new Vector3(t.position.x,t.position.y,t.position.z);
                Destroy(e);
                float f = Random.Range(0f, 0.999f);
                f = Mathf.Pow(f, 4);
                int index = Mathf.FloorToInt(trans.Length * f);
                Instantiate(trans[index], pos,transform.rotation);
                print(t.position);
            }
            day = false;
            savedSpeed = speed;
            speed = 0;
        }
        
        if ((angle > 345 || angle < 5) && !day)
        {
            //If boss dead
            print("-----------------------------day---------------------------------");
            audio.clip = backGroundMuisc[0];
            audio.Play();
            GameObject[] undead = GameObject.FindGameObjectsWithTag("Undead");
            foreach (GameObject e in undead)
            {
                e.GetComponent<Character>().TakeDamage(null, 0, 1000);
            }

            GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");            
            foreach (GameObject e in spawner)
            {
                e.GetComponent<Spawner>().enabled = true;
            }
            day = true;
        }
        //print(angle);
        transform.Rotate(Time.deltaTime* speed, 0, 0);
    }
}
