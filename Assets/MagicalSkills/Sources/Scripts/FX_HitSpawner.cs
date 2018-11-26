using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MagicalFX
{
	public class FX_HitSpawner : MonoBehaviour
	{

        public GameObject attacker;
        public float range = 3;
        public float damage = 120;
		public GameObject FXSpawn;
		public bool DestoyOnHit = false;
		public bool FixRotation = false;
        public bool OnlyHitFloor = true;
		public float LifeTimeAfterHit = 1;
		public float LifeTime = 0;

	
		void Start ()
		{
		
		}
	
		void Spawn ()
		{
			if (FXSpawn != null) {
				Quaternion rotate = this.transform.rotation;
				if (!FixRotation)
					rotate = FXSpawn.transform.rotation;
				GameObject fx = (GameObject)GameObject.Instantiate (FXSpawn, this.transform.position, rotate);
				if (LifeTime > 0)
					GameObject.Destroy (fx.gameObject, LifeTime);
			}
			if (DestoyOnHit) {
			
				GameObject.Destroy (this.gameObject, LifeTimeAfterHit);
				if (this.gameObject.GetComponent<Collider>())
					this.gameObject.GetComponent<Collider>().enabled = false;

			}
		}
	
		void OnTriggerEnter (Collider other)
		{
            if (!OnlyHitFloor || other.gameObject.tag == "Floor")
            {
                Character script = attacker.GetComponent<Character>();
                List<GameObject> targets =  script.GetEnemies();
                foreach(GameObject e in targets)
                {
                    if (e == null) continue;

                    float dist = Vector3.Distance(transform.position, e.transform.position);
                    if (dist <= range)
                    {
                        Character targetScript = e.GetComponent<Character>();
                        float actualDMG = damage * Mathf.Lerp(1f, 0.5f, dist / range);
                        //print("++++++++++++Damage: " + actualDMG);
                        targetScript.TakeDamage(attacker, 0, actualDMG);
                    }
                }
                Spawn();
            }
		}
	
		void OnCollisionEnter (Collision collision)
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
            Spawn();
        }
    }
}