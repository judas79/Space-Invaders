﻿using UnityEngine;
using System.Collections;
// T5 Needed to manipulate the UI
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    // T5 define default speed
    private float speed = 30;

    // T5 access to the rigidbody2d to move the bullets(to enable shooting the bullets)
    private Rigidbody2D rigidBody;

    // T6 provide image of the aliens exploding
    // to replace the alien with the alien exploding, by referencing a sprite
    public Sprite explodedAlienImage;

    // Use this for initialization
    void Start () {

        // T5 Get reference to the bullets Rigidbody, initialize the bullets 
        // when they come on the screen
        rigidBody = GetComponent<Rigidbody2D>();

        // T5 When the bullet is created move it upwards (up)
        // (at the desired speed (velocity)
        rigidBody.velocity = Vector2.up * speed;
    }

    // T5 Called every time a ball collides with something (a wall)
    // the object it hit is passed as a parameter
    private void OnTriggerEnter2D(Collider2D col)
    {
        // T5 If the thing the Bullet hits; like, a wall destroys the bullet
        // using the tag name Wall,
        if(col.tag == "Wall")
        {
            // destroy the bullet
            Destroy(gameObject);
        }

        // T6 Handle other situations, when bullet hits other items
        // When it hits the alien.
        if (col.tag == "Alien")
        {
            // T6 call the sound manger instance, play the exploding alienDies sound file
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);

            // T6 increase the score, when an alien dies, call:
            IncreaseTextUIScore();

            // T6 When shot, change regular alien image to an exploded alien image
            // refer to our collision item and get our sprite renderer, 
            // to dynamically change the sprite stored with this game object, on the screen
            // and replace it with the exploded alien, image, in storage
            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            // T6 destroy our, no longer needed, bullet
            Destroy(gameObject);

            // T6 we want the collision  game object, which is the explosion, at this time,
            // to show on the screen for a half second, so we can see it before it is destroyed
            DestroyObject(col.gameObject, 0.5f);
        }

        // T6 If Alien Bullet hits Shield destroy both them
        // refer to the fields using its tag, yes shield is spelled incorrectly
        if(col.tag == "Sheild")
        {
            // destroy bullet
            Destroy(gameObject);

            // destroy shield
            DestroyObject(col.gameObject);
        }

    }

    // T5 Called when the Game Object isn't visible anynore
    //  when the bullet leaves the screen area, destroy it
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // T6 function to be called, when alien shot, to increace the score
    // Increases the score the the text UI name passed
    void IncreaseTextUIScore()
    {
        // T6 all aliens killed score, is the same for each
        // find the score text UI component, using the name Score, in the heirachy
        // get the component; its a text component
        var textUiComp = GameObject.Find("Score").GetComponent<Text>();

        // T6 get the string that is stored inside of our text window 
        // to properly increment the score, by converting the text to int
        int score = int.Parse(textUiComp.text);

        // T6 increase the score by 10 every time when you shoot an alien
        score += 10;

        // T6 update the Score component on the screen, with the new score amount
        // refer to score int, and convert it to string for display
        textUiComp.text = score.ToString();
    }

    // Update is called once per frame
    void Update () {
	
	}
} 
