using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAnimal : AiCharacter
{
    public GameObject dropObj;
    public ItemData[] dropItems;

    public GameObject skill;
    public int SkillDamage;

    public GameObject sun;
    //public AudioClip[] Audios;
    private AudioSource audioSource;

    public Spawner sp1;
    public Spawner sp2;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        skill.GetComponent<MagicalFX.FX_SpawnDirection>().attacker = gameObject;
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isActive)
        {
            sp1.enabled = false;
            sp2.enabled = false;
            return;
        }
        if(target == null)
        {
            sp2.enabled = true;
            sp1.burst = 3;
            sp2.burst = 1;
        }
        else
        {
            sp1.burst = 2;
            sp2.enabled = false;
        }
        if (target != null  && !attacking)
        {
            if(Random.Range(0.0f, 1000.0f) <= 6f)
                animator.SetTrigger("Attack");
        }
    }
    protected override void AnimAttack()
    {
       
        base.AnimAttack();


        animator.SetTrigger("Skill1");

    }

    protected override void AnimDie()
    {
        if (dropItems.Length > 0)
        {
            GameObject drop = Instantiate(dropObj, transform.position, transform.rotation);
            Rotate rotate = drop.GetComponent<Rotate>();
            int index = Random.Range(0, dropItems.Length - 1);
            rotate.updateItemInfo(dropItems[index]);
        }
        base.AnimDie();
        sun.GetComponent<Sun>().Dawn();
        animator.SetTrigger("Death");
    }


    protected override void AnimMove(float speed)
    {
        base.AnimMove(speed);
        animator.SetFloat("Speed", speed);
    }


    protected void Skill()
    {
        
        Vector3 t = target.transform.position;
        t.y = 0;
        Vector3 o = transform.position;
        o.y = 0;
        Quaternion dir = Quaternion.LookRotation(t - o);
        Instantiate(skill, transform.position, dir);

    }


    //Animation Event
    protected void SkillDamageTarget()
    {
        attacking = false;
        if (!isActive) return;
        if (target != null)
        {
            if (debug) print("target is not null");
            if (InAttackRange(target.transform.position))
            {
                if (debug) print("target is in range");
                Character script = target.GetComponent<Character>();
                script.TakeDamage(this.gameObject, 1, SkillDamage);
            }
            audioSource.Play();
        }
    }
}
