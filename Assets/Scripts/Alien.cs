﻿using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

    // T7 speed and rigidbody is on all the scripts, except for the soundmanager script.
    // access to the rigidbody2d to move the AlienBullet(to enable shooting the alien bullets)
    private Rigidbody2D rigidBody;

    // T7 when moving anything, we always need a place, that holds the speed var,
    // for that object. it is public, to be able to set it in the 'inspector' outside of here
    // slowed don from 30 to 10, so aliens don't travel down, too quickly
    public float speed = 10;

    // T7 setup to change the starting alien sprite images to change to the alternative alien imagine, 
    // to give the illusion of movement
    public Sprite startImage;

    // T7 alien alternative sprite image
    public Sprite altImage;

    // T7 another way to use the sprite renderer to change the images, between the default alien image and alernative alien image
    private SpriteRenderer spriteRenderer;

    // T7 float var to hold the amount of time(wait) before the alien sprite image changes
    public float secBeforeSpriteChange = 0.5f;

    // T7 reference to our alien bullets (GameObject), being shot
    public GameObject alienBullet;

    // T7 to stager the time between aliens firing, so they are not all firing at once, but stagered instead
    // Minimum time to wait before firing, and maximum amount of time between firing, 
    // this will be tweaked and a randomizer used
    public float minFireRateTime = 1.0f;
    public float maxFireRateTime = 3.0f;

    // T7 Base firing wait time
    public float baseFireWaitTime = 3.0f;

    // T7 reference the Exploded Ship sprite Image
    // needs to be public so we can drag and drop it inside
    public Sprite explodedShipImage;


    // Use this for initialization
    void Start () {

        // T7 Get reference to the alien Rigidbody, initialize the rigidBody for the Alien 
        // when they come on the screen
        rigidBody = GetComponent<Rigidbody2D>();

        // T7 initialze and Set the starting direction and speed for the aliens, this moves them
        // the x axis going right a the speed of 1, y set to 0 and multiplied by our speed, which is 10
        rigidBody.velocity = new Vector2(1, 0) * speed;

        // T7 Access the SpriteRenderer component so we are able to change that component(alien images)
        spriteRenderer = GetComponent<SpriteRenderer>();

        // T7 Call changeAlienSprite () to cycle the Alien sprites, we will need a function to call this
        // also need a time delay between the sprite image changes, we will return an IEnumerator
        // We will use StartCoroutine to call our function(ChangeAlienSprite())
        StartCoroutine(ChangeAlienSprite());

        // T7 initialize when an alien is first created on screen
        // Defines a random fire wait time for each Alien, 
        // the first time an alien shoots will be randomized using our two variables between 1 min and 3 max
        baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);


    }

    // T7 we will make a few functions to have our aliens go right, then after hitting the wall,
    // go down one and then go left, and repeat, unless detting destroyed or hitting the bottom wall.

    // T7 Turn in oposite direction, when hitting a wall,
    // be passed the direction to keep the logic out of the method, right direction passed in will equal one
    // and -1 will define going in the left direction, then we change the velocity for the new current direction
    void Turn(int direction)
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
    }

    // T7 Move down after hitting wall
    // the position, of our alien, (vector2) will be the current position for our alien is
    // change our position downwards using position.y = -1
    // then set our position back for our alien, to our current position
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    // T7 Switch direction on collision with a Wall, by calling Turn and MoveDown
    // OnCollision is called anything hits anything else.
    // if the thing it collided with is named as lefWall, then call Turn and pass in 1 to go right
    // then MoveDown one, if the name equals RightWall then pass -1 to go left, then MoveDown one
    // if a spaceships Bullet hits an alien, make the alien death explosion sound, destroy the alien gameObject
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }
        if (col.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }
        if (col.gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            Destroy(gameObject);
        }
    }


    // T7 Used to change the current sprite and play sounds, returns IEnumerator
    public IEnumerator ChangeAlienSprite()
    {
        // cycle back and forth between the alien images until destroyed
        while (true)
        {
            // if it is the default image, change it to the alternative image
            if(spriteRenderer.sprite == startImage)
            {
                spriteRenderer.sprite = altImage;

                // T7 play the sound for the alien moving, commented out at end of tutorial
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);
            }
            // if its not the default image or the alternative image, 
            // start the alien image, and play the second alien sound
            else
            {
                spriteRenderer.sprite = startImage;

                // play the sound for the alien moving
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }
            // T7 pause for the determined amount of seconds(secBeforeSpriteChange), between alien image changes
            yield return new WaitForSeconds(secBeforeSpriteChange);
        }
    }

    // T7 make aliens fire at random times, if time is greater than the base wait time, enough time that the wait interval has passed,
    // then start firing bullets
    private void FixedUpdate()
    {
        if (Time.time > baseFireWaitTime)
        {
            baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);

            // make an instance of an alienBullet, at the position the alien is at, then call Quaternion
            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }
    // T7 handles what happens when the aliens contact our spaceship(tag = Player)
    // play the ship explosion sound, then show the ship exloding image, 
    // destropy the spaceShip object, but wait a half second
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(gameObject);
            DestroyObject(col.gameObject, 0.5f);
        }
    }


    /* Update is called once per frame  NOT USED
        void Update () {} */

}
