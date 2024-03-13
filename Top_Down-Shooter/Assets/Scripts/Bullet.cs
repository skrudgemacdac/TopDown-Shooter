using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        //TODO: ��������� �������

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            DestroyBullet();
        }    
        
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void DestroyBullet() 
    {
        Destroy(gameObject);
    }
}
