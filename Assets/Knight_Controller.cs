using System.Collections;
using UnityEngine;

public class Knight_Controller : MonoBehaviour
{
    public float movespeed = 5f;   // Speed of character variable.
    public Rigidbody2D rigidBody;  // Instance of the Rigid Body class.
    public Animator anim;          // Reference to the animation components.

    Vector2 movement;
    private void Start()
    {
        anim = GetComponent<Animator>();

        bool North = false;
        bool South = false;
        bool East = false;
        bool West = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Takes the input of WASD or the Arrow Keys, and translates into a
        movement.y = Input.GetAxisRaw("Vertical");   // x and y movement.

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        int isDiagonal = x * y != 0 ? 0 : 1; // Ternary Operator to check if a diagnal movement as present, and cast the vector to an int.

        transform.position += new Vector3(x, y, 0) * isDiagonal * Time.deltaTime * movespeed;

        if(movement.x > 0)
        {
            setDirection("East");
        } else if(movement.x < 0)
        {
            setDirection("West");
        }
        if (movement.y > 0)
        {
            setDirection("North");
        } else if (movement.y < 0)
        {
            setDirection("South");
        }

    }
    void setDirection(string direction)
    {
        anim.SetBool("East", false);
        anim.SetBool("West", false);
        anim.SetBool("North", false);
        anim.SetBool("South", false);

        anim.SetBool(direction, true);
    }
}