using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDCanvas : MonoBehaviour
{

    public GameObject boss;
    public GameObject target;
    public GameObject deadUI;
    public GameObject BagUI;
    public GameObject SurvivalUI;
    public GameObject BossUI;
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider bossHealthSlider;

    public Image damageImage;
    public float flashSpeed = 1f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool gameOver = false;

    private PlayerController player;
    private BossAnimal bossScript;

    // Use this for initialization
    void Start()
    {
        player = target.GetComponent<PlayerController>();
        bossScript = boss.GetComponent<BossAnimal>();

        healthSlider.maxValue = player.maxHealth;
        healthSlider.value = player.health;

        manaSlider.maxValue = player.maxMana;
        manaSlider.value = player.mana;

        bossHealthSlider.maxValue = bossScript.maxHealth;
        bossHealthSlider.value = bossScript.health;
        
        SurvivalUI.SetActive(true);
        BagUI.SetActive(true);
        deadUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null && bossScript != null)
        {
            //bossHealthSlider.value = Mathf.Lerp(bossHealthSlider.value, bossScript.health, flashSpeed * Time.deltaTime);
            //print(bossScript.health);
            bossHealthSlider.value = bossScript.health;
            if (Vector3.Distance(target.transform.position, boss.transform.position) < 10 && bossHealthSlider.value > 0)
            {
                BossUI.SetActive(true);
            }
            else
            {
                BossUI.SetActive(false);
            }
        }


        if (healthSlider.value > player.health)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColor;
        }
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        healthSlider.value = player.health;
        manaSlider.value = player.mana;
        if (healthSlider.value == 0)
        {
            SurvivalUI.SetActive(false);
            BagUI.SetActive(false);
            BossUI.SetActive(false);
            damageImage.color = new Color(50, 50, 50, 0.3f);

            if (gameOver && Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("GameStart");
            }
        }
    }

    public void onGameOver()
    {
        gameOver = true;
    }
}
