using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillLighting : Joystick
{
    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
    public GameObject character;
    private PlayerController player;
    public GameObject smallCircle;
    public GameObject largeCircle;
    private Circle circle;

    private Vector3 moveDir;
    private Image img;
    private bool isDown;

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        player = character.GetComponent<PlayerController>();
        smallCircle.SetActive(false);
        largeCircle.SetActive(false);
        smallCircle.transform.localScale *= 2;
        largeCircle.transform.localScale *= 2;
        img = transform.GetChild(0).GetComponent<Image>();
        circle = smallCircle.GetComponent<Circle>();
        isDown = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!player.Skill2Ready() || !isDown) return;
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        moveCircle();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!player.Skill2Ready()) return;
        isDown = true;
        smallCircle.SetActive(true);
        largeCircle.SetActive(true);
        smallCircle.transform.position = player.transform.position;
        OnDrag(eventData);
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!player.Skill2Ready() || !isDown) return;
        isDown = false;
        smallCircle.SetActive(false);
        largeCircle.SetActive(false);
        player.Skill1(smallCircle.transform.position);
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
        moveDir = (goRight * Horizontal + goFront * Vertical) * 5;
        circle.RelativePos = moveDir;
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