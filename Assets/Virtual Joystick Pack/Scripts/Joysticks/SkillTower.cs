using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTower : Joystick
{
    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
    public GameObject character;
    private PlayerController player;
    private ConsumableInventory inv;
    public GameObject smallCircle;
    public GameObject largeCircle;
    private Circle circle;

    private Vector3 moveDir;
    private Image img;
    private bool isDown;
    private bool isReady;

    void Start()
    {
        isReady = false;
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        player = character.GetComponent<PlayerController>();
        inv = character.GetComponent<ConsumableInventory>();
        smallCircle.SetActive(false);
        largeCircle.SetActive(false);
        img = transform.GetChild(0).GetComponent<Image>();
        circle = smallCircle.GetComponent<Circle>();
        isDown = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isReady || !isDown) return;
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        moveCircle();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!isReady) return;
        isDown = true;
        smallCircle.SetActive(true);
        largeCircle.SetActive(true);
        smallCircle.transform.position = player.transform.position;
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!isReady || !isDown) return;
        inv.ConsumeHpPotion(1);
        inv.ConsumeMpPotion(1);
        isDown = false;
        smallCircle.SetActive(false);
        largeCircle.SetActive(false);
        player.Skill3(smallCircle.transform.position);
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void moveCircle()
    {
        Vector3 goFront = player.cam.transform.forward;
        goFront.y = 0;
        goFront = Vector3.Normalize(goFront);
        Vector3 goRight = player.cam.transform.right;
        goRight.y = 0;
        goRight = Vector3.Normalize(goRight);
        moveDir = (goRight * Horizontal + goFront * Vertical) * 9;
        circle.RelativePos = moveDir;
    }

    void FixedUpdate()
    {
        if (!player.Skill3Ready() || inv.HpPotion < 1 || inv.MpPotion < 1)
        {
            isReady = false;
            img.color = Color.gray;
        }
        else
        {
            isReady = true;
            img.color = Color.white;
        }
    }
}