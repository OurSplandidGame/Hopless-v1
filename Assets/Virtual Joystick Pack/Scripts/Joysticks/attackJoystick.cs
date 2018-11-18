using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class attackJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    public GameObject character;
    private PlayerController player;

    private Image img;

    void Start()
    {
        player = character.GetComponent<PlayerController>();
        img = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player.Attacking) return;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(player.Attacking) return;
        player.Move(new Vector3());
        player.Attack();
    }

    void Update()
    {
        if(player.Attacking)
        {
            img.color = Color.gray;
        } else
        {
            img.color = Color.white;
        }
    }
}
