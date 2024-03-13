using UnityEngine;
using CodeMonkey;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform startShotPoint;
    public Transform firePoint;
    public Joystick joystick;
    public GameObject fire;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controlType == Player.ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0) && player.controlType == Player.ControlType.PC)
            {
                Shoot();
            }
            else if (player.controlType == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                //Shoot();
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, startShotPoint.transform.position, transform.rotation);  
        Instantiate(fire, firePoint.position, transform.rotation, player.transform); 
    }
}
