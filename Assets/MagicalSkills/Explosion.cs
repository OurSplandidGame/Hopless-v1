using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public GameObject explosion;
    public GameObject attacker;
    public float clear = 3f;
    public float delay = 1.5f;
    public float damage = 100f;
    public float range = 3f;

    float timer = 0;
    private bool hited;
	// Use this for initialization
	void Start () {
        hited = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(timer >= delay && hited == false)
        {
            hited = true;
            explosion.SetActive(true);
            Character script = attacker.GetComponent<Character>();
            List<GameObject> targets = script.GetEnemies();
            foreach (GameObject e in targets)
            {
                if (e == null) continue;
                float dist = Vector3.Distance(transform.position, e.transform.position);
                if (dist <= range)
                {
                    if (e == null) continue;
                    Character targetScript = e.GetComponent<Character>();
                    float actualDMG = damage * Mathf.Lerp(1f, 0.3f, dist / range);
                    targetScript.TakeDamage(attacker, 0, actualDMG);
                }
            }
        }
        if(timer >= clear)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
	}
}
