using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour {

    public GameObject attacker;
    public float range;
    public float damage;
    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        Character script = attacker.GetComponent<Character>();
        List<GameObject> targets = script.GetEnemies();
        foreach (GameObject e in targets)
        {
            if (e == null) continue;

            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist <= range)
            {
                Character targetScript = e.GetComponent<Character>();
                float actualDMG = damage * Mathf.Lerp(1f, 0.5f, dist / range);
                // print("===============Damage: " + actualDMG);
                targetScript.TakeDamage(attacker, 0, actualDMG);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
