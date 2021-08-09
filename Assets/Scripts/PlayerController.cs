using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Joystick Aim;
    public JoyButton handle;
    Rigidbody rb;
    public float speed;
    public int jumpForce;

    public AudioSource aaudio;
    public AudioClip shoot;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int AfterDeathLoadLevel;

    bool jump;

    float angle;

    public Transform firepoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float AimStat;

    private bool ShootDelay = false;

    public bool AimButtonReleased = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        aaudio = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void LateUpdate()
    {
        healthBar.SetHealth(currentHealth);
        //Debug.Log(AimStat);
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);

        if(AimButtonReleased == false)
        {
            AimStat = Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical);
        }


        if(Aim.Horizontal != 0.0 || Aim.Vertical != 0.0)
            angle = Mathf.Atan2(Aim.Vertical, Aim.Horizontal) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(90.0f - angle, Vector3.up);

        if (AimButtonReleased == true)
        {
            Debug.Log(Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical));
            if (AimStat >= 1 && ShootDelay == false)
            {
                PlayerShoot();
            }
            else if (AimStat <= 1)
            {
                ShootDelay = false;
            }
            AimButtonReleased = false;
        }
                
           
            


        if (Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical) <= 1)
        {
            ShootDelay = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(15);
        }    

        if(currentHealth<=0)
        {
            SceneManager.LoadScene(AfterDeathLoadLevel);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void PlayerShoot()
    {
        aaudio.PlayOneShot(shoot);
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoint.forward * bulletForce, ForceMode.Impulse);
        ShootDelay = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            currentHealth = currentHealth - 10;
        }
    }

    /* void ShootReady()

                 Debug.Log(Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical));
                 if (Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical) >= 1)
                 {
                     PlayerShoot();
                 }
                 else if (Mathf.Abs(Aim.Horizontal) + Mathf.Abs(Aim.Vertical) <= 1)
                 {
                     ShootDelay = false;
                 }
             }
         }
     }
    */
}
 