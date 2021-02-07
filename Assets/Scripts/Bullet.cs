using UnityEngine;
using System.Collections;
// T5 Needed to manipulate the UI
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    // T5 define default speed
    private float speed = 30;

    // T5 access to the rigidbody2d to move the bullets
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {

        // T5 Get reference to the bullets Rigidbody, initialize the bullets 
        // when they come on the screen
        rigidBody = GetComponent<Rigidbody2D>();

        // T5 When the ball is created move it upwards (up)
        // (0,1) at the desired speed (velocity)
        rigidBody.velocity = Vector2.up * speed;
    }

    // T5 Called every time a ball collides with something (a wall)
    // the object it hit is passed as a parameter
    private void OnTriggerEnter2D(Collider2D col)
    {
        // T5 If the thing the Bullet hits; a wall destroys the bullet
        // using the tag name Wall,
        if(col.tag == "Wall")
        {
            // destroy the bullet
            Destroy(gameObject);
        }
    }

    // T5 Called when the Game Object isn't visible anynore
    //  when the bullet leaves the screen area, destroy it
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
} 
