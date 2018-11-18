using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillMagic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject character;
    private PlayerController player;

    public GameObject circle;
    private Image img;
    private bool isDown;

    void Start()
    {
        player = character.GetComponent<PlayerController>();
        circle.SetActive(false);
        img = GetComponent<Image>();
        isDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.Skill2Ready()) return;
        isDown = true;
        circle.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!player.Skill2Ready() || !isDown) return;
        isDown = false;
        circle.SetActive(false);
        player.Move(new Vector3());
        player.Skill2();
        
    }

    void Update()
    {
        if (!player.Skill2Ready())
        {
            img.color = Color.gray;
        }
        else
        {
            img.color = Color.white;
        }
    }
}
