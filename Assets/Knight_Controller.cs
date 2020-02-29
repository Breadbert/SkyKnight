using UnityEngine;

public class Knight_Controller : MonoBehaviour
{
    public float movespeed = 5f;   // Speed of character variable.
    public Rigidbody2D rigidBody;  // Instance of the Rigid Body class.
    public Animator anim;          // Reference to the animation components.

    public bool isInCombat = false;
    public Vector2 PlayerPosition;

    Vector2 movement;
    private void Start()
    {
        anim = GetComponent<Animator>();
        isInCombat = false;
        PlayerPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        if (!isInCombat)
        {
            movement.x = Input.GetAxisRaw("Horizontal"); // Takes the input of WASD or the Arrow Keys, and translates into a
            movement.y = Input.GetAxisRaw("Vertical");   // x and y movement.

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            int isDiagonal = x * y != 0 ? 0 : 1; // Ternary Operator to check if a diagnal movement as present, returns 0 or 1.

            transform.position += new Vector3(x, y, 0) * isDiagonal * Time.deltaTime * movespeed;
        }
        else {
            anim.Play("Fight_Anim");
        }

        if(movement.x > 0) // This if/else statement tree decides in the Animator what Idle Direction the character stays.
        {
            SetDirection("East");
        }
        else if(movement.x < 0)
        {
            SetDirection("West");
        }
        if (movement.y > 0)
        {
            SetDirection("North");
        } 
        else if (movement.y < 0)
        {
            SetDirection("South");
        }

    }
    void SetDirection(string direction) // Method which resets all Bools in Animator and sets one to true.
    {
        anim.SetBool("East", false);
        anim.SetBool("West", false);
        anim.SetBool("North", false);
        anim.SetBool("South", false);

        anim.SetBool(direction, true);
    }
}