﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDCanvas : MonoBehaviour {

    public GameObject target;
    public GameObject deadUI;
    public GameObject BagUI;
    public GameObject SurvivalUI;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool gameOver = false;

    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = target.GetComponent<PlayerController>();
        healthSlider.value = player.health;
        SurvivalUI.SetActive(true);
        BagUI.SetActive(true);
        deadUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (healthSlider.value == 0)
        {
            SurvivalUI.SetActive(false);
            BagUI.SetActive(false);
            damageImage.color = new Color(50, 50, 50, 0.3f);

            if (gameOver && Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("GameStart");
            }
        }
    }

    public void onGameOver(){
        gameOver = true;
    }
}
