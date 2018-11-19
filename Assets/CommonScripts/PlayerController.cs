using UnityEngine;
using System.Collections;
using MagicalFX;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Animator))]
public class PlayerController : Character {
    public float mana = 100;
    public float maxMana = 100;
    public float manaRestore = 0.5f;
    public float[] skillCost = new float[] { 10, 15 };
    public float armor; 
    public float maxArmor;
    public float maxDamage;
    public float speed;
    public GameObject skill1;
    public float skill1_Cooldown;
    public GameObject skill2;
    public GameObject tow;
    public Camera cam;
    public ParticleSystem attackEffect;
    private CharacterController player;
    private ConsumableInventory consumableInventory;
    int floorMask;
    public float skill1_Timer;
    public AudioClip[] Audios;
    public AudioSource audioSource;
    public GameObject deadUI;
    public GameObject hudCanvas;
    public HUDCanvas script;
    public Camera camera;
    public bool Attacking { get { return attacking; } }
    //public GameObject joystick;
    //private FixedJoystick moveJoystick;


    protected override void Awake()
    {
        base.Awake();
        floorMask = LayerMask.GetMask("Floor");
        player = GetComponent<CharacterController>();
        skill1_Timer = skill1_Cooldown;
        skill1.GetComponentInChildren<FX_HitSpawner>().attacker = gameObject;
        skill2.GetComponent<Explosion>().attacker = gameObject;
        audioSource = GetComponent<AudioSource>();
        script = hudCanvas.GetComponent<HUDCanvas>();
        consumableInventory = GetComponent<ConsumableInventory>();
        //moveJoystick = joystick.GetComponent<FixedJoystick>();
    }



    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isActive) return;
        /*if (Input.GetKeyDown("j") && !attacking)
        {
            Move(new Vector3());
            Attack();
        }
        else if(Input.GetKeyDown("k") && !attacking && skill1_Timer <= 0)
        {
            Move(new Vector3());
            Skill2();
        }
        else if (Input.GetKeyDown("l") && !attacking && skill1_Timer <= 0)
        {
            Move(new Vector3());
            Skill1();
        }
        else if (Input.GetKeyDown("n") && !attacking && skill1_Timer <= 0)
        {
            Move(new Vector3());
            skill1_Timer = skill1_Cooldown;
            //Instantiate(tow, transform.position + transform.forward, transform.rotation);
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
          
            if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
            {
                print(floorHit.point);
                Instantiate(tow, floorHit.point,transform.rotation);
            }
        }
        else if (!attacking && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.ResetTrigger("Attack1");
            Vector3 goFront = cam.transform.forward;
            goFront.y = 0;
            goFront = Vector3.Normalize(goFront);
            Vector3 goRight = cam.transform.right;
            goRight.y = 0;
            goRight = Vector3.Normalize(goRight);
            Vector3 moveDir = Vector3.Normalize(goRight * Input.GetAxis("Horizontal") + goFront * Input.GetAxis("Vertical"));
            Move(speed * moveDir);
        }
        else
        {
            Move(new Vector3());
        }*/
        skill1_Timer -= Time.deltaTime;

        mana += manaRestore * Time.deltaTime;
        if (mana > maxMana) mana = maxMana;
    }
    void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.tag);
        if(other.gameObject.tag == "Pickup")
        {
            PlayerInventory playerInventory = gameObject.GetComponent<PlayerInventory>();
            Rotate rotate = other.gameObject.GetComponent<Rotate>();
            consumableInventory.addToInventory((ConsumableData)rotate.getItem());
            //playerInventory.addItemToInventory(rotate.getItemId());
            Destroy(other.gameObject);
            audioSource.clip = Audios[0];
            audioSource.Play();
        }
    }
    public void Move(Vector3 velocity)
    {
        player.transform.forward =Vector3.Lerp(velocity, player.transform.forward,2.0f*Time.deltaTime);
        player.SimpleMove(velocity);
    }

    public override void Attack()
    {
        if(target != null) TurnToTarget();
        base.Attack();
    }

    protected static void PositionCast(Vector3 position, GameObject skill)
    {
        Instantiate(skill, position, skill.transform.rotation);
    }
    
    public bool Skill1Ready()
    {
        if (attacking || skill1_Timer > 0 || skillCost[0] > mana) return false;
        else return true;
    }

    public bool Skill2Ready()
    {
        if (attacking || skill1_Timer > 0 || skillCost[1] > mana) return false;
        else return true;
    }

    public bool Skill3Ready()
    {
        if (attacking || skill1_Timer > 0) return false;
        else return true;
    }

    public void Skill1(Vector3 position)
    {
        //Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit floorHit;
        //if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
        //{
        //    PositionCast(floorHit.point,skill1);
        //}
        if (attacking) return;

        if(Vector3.Distance(position, transform.position) < 1 && target != null && Vector3.Distance(transform.position, target.transform.position) <= 5)
        {
            position = target.transform.position;
        }

        if (mana >= 10)
        {
            PositionCast(position, skill1);
            mana -= 10;
            skill1_Timer = skill1_Cooldown;
        }

    }

    public void Skill2()
    {
        if (mana >= 10)
        {
            attacking = true;
            mana -= 10;
            skill1_Timer = skill1_Cooldown;
            animator.SetTrigger("Attack2");
            PositionCast(transform.position, skill2);
            audioSource.clip = Audios[3];
            audioSource.Play();
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    public void Skill3(Vector3 position)
    {
        position.y += 50;
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(position, new Vector3(0, -1, 0), out hit, 100f))
        {
            if (hit.collider.tag == "Floor")
            {
                Instantiate(tow, hit.point, transform.rotation);
            }
            else
            {
                //print(hit.collider.tag);
                return;
            }
        }
    }

    protected override void AnimMove(float speed)
    {
        base.AnimMove(speed);
        animator.SetFloat("Speed",speed);
    }

    protected override void AnimAttack()
    {
        base.AnimAttack();
        animator.SetTrigger("Attack1");
        attackEffect.Play();
    }

    protected override void AnimDie()
    {
        base.AnimDie();
        animator.SetTrigger("Death");
    }
    
    private void playAudio(string audio){
        if(audio == "attack")
        {
            audioSource.clip = Audios[1];
            audioSource.Play();
        }
    }
    public override void TakeDamage(GameObject attacker, int type, float amount)
    {
        base.TakeDamage(attacker, type, amount);
        audioSource.clip = Audios[2];
        audioSource.Play();
    }

    public void DeathUI()
    {
        audioSource.clip = Audios[4];
        audioSource.Play();
        deadUI.SetActive(true);
        script.onGameOver();
    }

    public void FinishAttack(){
        attacking = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
