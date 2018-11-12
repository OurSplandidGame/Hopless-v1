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

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void AnimAttack()
    {
       
        base.AnimAttack();

        float num = Random.Range(0.0f, 5.0f);
        if(num <= 1.0f){
            animator.SetTrigger("Attack");
        }
        else{
            animator.SetTrigger("Skill1");
           
        } 
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

        GameObject player = GameObject.Find("Player");
        Instantiate(skill, player.transform.position, skill.transform.rotation);

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
