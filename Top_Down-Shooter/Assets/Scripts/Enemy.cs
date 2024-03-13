using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform player;
    private Player _player;
    private GameObject physics;
    private Rigidbody2D rb;
    public CapsuleCollider2D capsuleCollider;
    public float moveSpeed = 200f;
    private Vector2 movement;
    public int health;
    public int damage;
    public float normalSpeed;
    public SkeletonAnimation skeletonAnimation;
    public string currentAnimation;
    public AnimationReferenceAsset attack, idle, walking, die;
    public Transform bloodPoint;
    public Transform bloodPoint2;
    public Blood blood;
    public GameObject blood2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = moveSpeed;
    }

    void Update()
    {
        if (health > 0) 
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }

        else if (health <= 0)
        {
            SetCharacterState("Die");
            gameObject.layer = 0;
            Destroy(rb);
            Destroy(capsuleCollider);
            gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 0f, 0.0000000000000001f);
            //blood.transform.position = blood.transform.position + new Vector3(0f, 0f, 0.000000000000001f);
        }
    }

    public void FixedUpdate()
    {
        moveCharacter(movement);
    }

    public void moveCharacter(Vector2 direction) 
    {
        if (health > 0)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0 && health > 0)
            {
                SetCharacterState("Attack_1");
                other.GetComponent<Player>().ChangeHealth(damage);
                SetCharacterState("Blood");
            }
        }
        else 
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        SetCharacterState("Run");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(blood, bloodPoint.position, transform.rotation);
        Instantiate(blood2, bloodPoint2.position, transform.rotation);
    }

    public void OnEnemyAttack()
    {
        _player.ChangeHealth(damage);
        timeBtwAttack = startTimeBtwAttack;
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
        if (state.Equals("Run") && health > 0)
        {
            SetAnimation(walking, true, 1f);
        }
        else if (state.Equals("Attack_1") && health > 0)
        {
            SetAnimation(attack, true, 1f);
        }
        else if (state.Equals("Die") && health <= 0)
        {
            SetAnimation(die, false, 1f);
        }
    }
}
