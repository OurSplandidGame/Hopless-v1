using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject obj;
    public GameObject effect;
    public uint max = 7;
    public uint interval = 5;
    public float probability = 0.7f;
    public uint burst = 1;

    public float rangeMin = 0;
    public float rangeMax = 20;

    int number;
	// Use this for initialization
	void Start () {
        number = 0;
	}

    float timer = 0;
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0;
            GameObject[] arr = GameObject.FindGameObjectsWithTag(obj.tag);

            number = arr.Length;
            if (number >= max) return;
            float factor = ((float)max - number) / max * probability;

            if(Random.Range(0.0f, 1.0f) <= factor)
            {
                uint tmp = burst;
                uint numTry = 100;
                while(tmp != 0 && numTry != 0)
                {
                    
                    float dist = Mathf.Sqrt(Random.Range(0f, 1f)) * rangeMax;
                    float rot = Random.Range(0f, 1f) * 2 * Mathf.PI;
                    Vector3 pos = new Vector3(dist * Mathf.Sin(rot), 50, dist * Mathf.Cos(rot));
                    pos += transform.position;

                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(pos, new Vector3(0, -1, 0), out hit, 100f))
                    {
                        if (hit.collider.tag == "Floor")
                        {
                            if(effect != null)
                                Instantiate(effect, hit.point, transform.rotation);
                            Instantiate(obj, hit.point, transform.rotation);
                            tmp--;
                        }
                        else
                        {
                            numTry--;
                        }
                    } else numTry--;
                }

            }
        }
    }
}
