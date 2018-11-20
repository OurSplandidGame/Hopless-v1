using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : AiCharacter
{
    public GameObject dropObj;
    public ItemData[] dropItems;
    protected override void AnimAttack()
    {
        base.AnimAttack();
        animator.SetTrigger("Attack");
    }

    protected override void AnimDie()
    {
        if (dropItems.Length > 0)
        {
            GameObject drop = Instantiate(dropObj, transform.position, transform.rotation);
            Rotate rotate = drop.GetComponent<Rotate>();
            int index = Random.Range(0, dropItems.Length);
            rotate.updateItemInfo(dropItems[index]);
        }

        base.AnimDie();
        animator.SetTrigger("Death");
    }


    protected override void AnimMove(float speed)
    {
        base.AnimMove(speed);
       
        animator.SetFloat("Speed", speed);
    }


}
