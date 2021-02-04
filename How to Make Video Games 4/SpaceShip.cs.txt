using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {

    // T4 when moving anything, we always need a place, that holds the speed var,
    // for that object. it is public, to be able to set it in the 'inspector' outside of here
    public float speed = 30;

    // T4 Holds a reference to Bullet objects, spacechip shoots bullets
    public GameObject theBullet;

    // T4 everytime you use rigidbody you will need FixedUpdate to move it
    void FixedUpdate()
    {
        // spaceship will be moving on a horrizontal plane, get users horizontal inputs
        float horzMove = Input.GetAxisRaw("Horizontal");

        // T4 find out if the spaceship component, rigidBody is moving, right or left
        // and set the velocity using vectors (Vetor2 for 2d) to do the moving, only the horizontal moving so y='0',
        // multiplied times our speed var, to define how quicly the spaceship moves horizontaly
        GetComponent<Rigidbody2D>().velocity = new Vector2(horzMove, 0) * speed;
    }

    /* T4 we will not, Use this for initialization
    void Start () {
	
	}
    */
	
	// T4 Update is called once per frame
    // called every time a bullet is fired by click to spacebar, and sound by spacebar click
	void Update ()
    {
	    // T4 check the input to see if a specific button has been pressed(the spacebar)
        // Jump is represented by the spacebar
        if(Input.GetButtonDown("Jump"))
        {
            // Fire a bullet, with the cooresponding sound

        }
	}
}
