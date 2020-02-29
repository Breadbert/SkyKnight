using System.Collections;
using UnityEngine;

public class Enemy_Behavior : Enemy
{ // Inherits the stats from Enemy.cs

    public Transform[] waypoints;  // Array of Waypoints.
    public float moveSpeed = 3f;   // Movement speed variable.
    public float AngryMoveSpeed = 3.5f;

    // Index of current waypoint from which Enemy walks to the next one.
    private int waypointIndex = 0;
    private float waitTime;        // Time variable.
    public float initWaitTime;     // Initializes the time variable.
    private Animator anim;         // Reference to the animation components.
    public Rigidbody2D rigidBody;  // Instance of the Rigid Body class.
    public Enemy enemy;            // Instance of an enemy.
    public BattleManager bm;       // Invokes the BattleManager.
    public GameObject Player;      // Instance of a Player.
    private bool allowedToMove = true;
    private bool Angry = false;
    private bool R = false;

    private Vector3 PrevPosition;
    public Vector3 MoveDirection;
    public Vector2 EnemyPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        // Set position of Enemy as position of the first waypoint.
        transform.position = waypoints[waypointIndex].transform.position;
        EnemyPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 PlayerPosition = Player.GetComponent<Knight_Controller>().PlayerPosition; // Position of the player pointer.
        var R = anim.GetBool("Right");
    }

    private void Update()
    {
        waitTime = initWaitTime;
        Move();

        MoveDirection = transform.position - PrevPosition;
        var x = MoveDirection.x;
        MoveDirection.Normalize();

        anim.SetFloat("X", x);
       
        if (Angry == true)
        {
            anim.SetTrigger("Anger");
            if(x > 0 && R == true) 
            {
                anim.SetBool("AngryRight", true);
                AngryMove();
            }
            else
            {
                anim.SetBool("AngryLeft", true);
                AngryMove();
            }
        }

        if (x > 0)
        {
            SetDirection("Right");
            if (Angry == true && x > 0)
            {
                SetAnger("Right");
            }
        }
        else
        {
            SetDirection("Left");
            if (Angry == true && x < 0)
            {
                SetAnger("Left");
            }
        }
        PrevPosition = transform.position;
    }

    private void Move()
    {

        if (waypointIndex <= waypoints.Length - 1)
        {
            if (allowedToMove)
            {
                // Moves the enemy.
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

                // If the waypoint is reached, the index is incremented by 1.
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    if (waitTime <= 0)
                    {
                        waitTime = initWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                        waypointIndex += 1;
                    }
                }
                if (waypointIndex == waypoints.Length) // Loop the patrol sequence.
                {
                    waypointIndex = 0;
                }
            }
        }
    }

    private void AngryMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, AngryMoveSpeed * Time.deltaTime);
    }

    private void SetDirection(string direction)
    {
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);

        anim.SetBool(direction, true);
    }

    void SetAnger(string direction)
    {
        anim.SetBool("AngryRight", false);
        anim.SetBool("AngryLeft", false);

        anim.SetBool(direction, true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Knight_Controller>())
        {
            allowedToMove = false;
            Angry = true;
            if (bm != null)
            {
                bm.StartBattle(enemy);
            }
        }
    }

    void Die(Enemy enemy)
    {
        if (enemy.HP == 0)
        {
            anim.SetTrigger("Die");
        }
    }
}