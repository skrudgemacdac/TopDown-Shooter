using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ControlType controlType;
    public enum ControlType {PC, Android}

    public float speed;
    public int health;
    public Joystick joystick;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking;
    public string currentState;
    public string currentAnimation;

    private float angle;
    private Vector3 velocityVector;

    public Camera cam;

    public Transform bloodPoint;


    Vector2 mousePos;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacterState(currentState);

        if (controlType == ControlType.PC) 
        {
            joystick.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (health != 0)
        {
            Move();
        }
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name.Equals(currentAnimation)) 
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timescale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("Walking"))
        {
            SetAnimation(walking, true, 1f);
        }
    }

    public void Move() 
    {
        if (controlType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (controlType == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            SetCharacterState("Walking");
        }
        else
        {
            SetCharacterState("Idle");
        }

        moveVelocity = moveInput.normalized * speed;
    }

    public void SetVelocity(Vector3 velocityVector) 
    {
        this.velocityVector = velocityVector;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        rb.velocity = velocityVector * speed;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }

        else 
        {
            Vector2 lookDır = mousePos - rb.position;
            angle = Mathf.Atan2(lookDır.y, lookDır.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
    public void ChangeHealth(int healthValue) 
    {
        health -= healthValue;
    }
}