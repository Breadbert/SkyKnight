using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;   // Speed of character variable.
    public Rigidbody2D rigidBody;  // Instance of the Rigid Body class.
    public Animator anim;          // Reference to the animation components.
    public Sprite north;           // The sprite variables, which will be utilized to render the sprites later on. 
    public Sprite south;
    public Sprite east;
    public Sprite west;

    Vector2 movement;

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
    }
}
    enum Direction
    {
        North,
        East,
        South,
        West
    }