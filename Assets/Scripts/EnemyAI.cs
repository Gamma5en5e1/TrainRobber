using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float FOVangle = 110f;
    public bool playerInSight;
    public bool InCollider;
    public Vector3 personalLastSighting;
    public bool GoingLastSeen;

    public float DistToSee;


    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private Vector3 lastPlayerSighting;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 previousSighting;


    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    public float DistToAttack;
    public bool stilAttacking;


    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex = 0;


    private bool NotAiming=true;
    Vector3 NearAim;
    Vector3 nextAimPoint;
    public float waitTime;
    private float currentTime;
    public AudioClip shootsound;
    public AudioClip aimsound;
    public AudioSource enemysounds;
    public int AimDistance;
    private bool shot;

    public float EnemyBulletSpeed;
    public GameObject bullet;
    public Transform Enemyfirepoint;

    public int Health = 100;
    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        else if (Health > 0)
        {

            
            if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < DistToSee)
            {
                InCollider = true;
            }
            else
            {
                InCollider = false;
                playerInSight = false;
            }




            if (InCollider)
                FOVCheck();

            if (playerInSight == true)
            {
                lastPlayerSighting = player.transform.position;
            }


            if (playerInSight && DistToAttack >= Vector3.Distance(this.transform.position, player.transform.position) && playerController.currentHealth > 0)
            {
                Attacking();
                Debug.Log("1");
            }


            if (playerInSight && playerController.currentHealth > 0 || GoingLastSeen)
            {
                Chasing();
                Debug.Log("2");
            }
            else
            {
                Patrolling();
                Debug.Log("3");
            }
        }
    }

    void FOVCheck()
    {
        Debug.Log("FOV");
        playerInSight = false;

        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log(angle);
        if (angle <= FOVangle * 0.5f)
        {
            
            RaycastHit hit;

            if (Physics.Raycast(transform.position /*+ transform.up*/, direction.normalized, out hit, DistToSee))
            {
                Debug.Log("Check");
                if (hit.collider.gameObject == player)
                {
                    playerInSight = true;

                    //personalLastSighting = player.transform.position;
                }
            }
        }
    }

    void Attacking()
    {
        if (stilAttacking == false)
        {
            stilAttacking = true;
            Debug.Log("IamAttacking");
            nav.isStopped = true;
            if (NotAiming == true)
            {
                enemysounds.PlayOneShot(aimsound);
                NearAim = new Vector3(player.transform.position.x + Random.Range(-2, 2), player.transform.position.y, player.transform.position.z + Random.Range(-2, 2));
            }
        }
        Aiming();
    }

    void Chasing()
    {
        if (Vector3.Distance(transform.position, lastPlayerSighting) > 1f && playerInSight)
        {
            nav.destination = player.transform.position;
            GoingLastSeen = true;
        }
        else if (!playerInSight)
        {
            nav.destination = lastPlayerSighting;

            if (Vector3.Distance(this.gameObject.transform.position, lastPlayerSighting) < 2)
            {
                Debug.Log("4");
                GoingLastSeen = false;
                chaseTimer = 0f;
            }
            else if (nav.remainingDistance < nav.stoppingDistance+3)
            {
                Debug.Log("5-" + Vector3.Distance(this.gameObject.transform.position, lastPlayerSighting));

                chaseTimer += Time.deltaTime;

                if (chaseTimer > chaseWaitTime || this.gameObject.transform.position == lastPlayerSighting)
                {
                    GoingLastSeen = false;
                    chaseTimer = 0f;
                }
            }

        }

        nav.speed = chaseSpeed;

       
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;

        if ( nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
                patrolTimer = 0f;

            }

        }
        else
            patrolTimer = 0f;
        nav.destination = patrolWayPoints[wayPointIndex].position;
    }

    public void AttackFinished()
    {
        Debug.Log("AttackFinished");
        nav.isStopped = false;
        stilAttacking = false;
    }

    public void DamagePlayer()
    {
        playerController.currentHealth -= 10;
        if (DistToAttack + 1 >= Vector3.Distance(this.transform.position, player.transform.position) && playerInSight)
        {
            playerController.currentHealth -= 10;
            Debug.Log(playerController.currentHealth);
        }
    }

    void Aiming()
    {

        this.transform.LookAt(player.transform.position);
        if (currentTime == 0)
            Shoot();

        if (shot && currentTime< waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
    }

    void Shoot()
    {
        shot = true;
        //playerController.TakeDamage(10);
        GameObject EnemyBullet = Instantiate(bullet, Enemyfirepoint.position, Enemyfirepoint.rotation);
        Rigidbody rb = EnemyBullet.GetComponent<Rigidbody>();
        rb.AddForce(Enemyfirepoint.forward * EnemyBulletSpeed, ForceMode.Impulse);
        NotAiming = true;
        AttackFinished();
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Shot");
        if (other.gameObject.tag=="Bullet")
        {
            Health = Health - 10; 
        }
    }
}
