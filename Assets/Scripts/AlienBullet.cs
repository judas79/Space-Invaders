﻿using UnityEngine;
using System.Collections;

public class AlienBullet : MonoBehaviour {

    // T6 access to the rigidbody2d to move the AlienBullet(to enable shooting the alien bullets)
    private Rigidbody2D rigidBody;

    // T6 when moving anything, we always need a place, that holds the speed var,
    // for that object. it is public, to be able to set it in the 'inspector' outside of here
    public float speed = 30;

    // T6 Exploded Ship Image
    // so when an alien bullet hits my spaceShip it explodes it
    public Sprite explodedShipImage;

    // T6 Use this for initialization
    // This start section is essentially the same as the Bullet.cs Start section 
    // so we will copy that and paste here
    void Start() {

        // T6 Get reference to the bullets Rigidbody, initialize the bullets 
        // when they come on the screen
        rigidBody = GetComponent<Rigidbody2D>();

        // T6 When the bullet is created, move it downwards (down)
        // at the desired speed (velocity)
        rigidBody.velocity = Vector2.down * speed;
    }

    // T6 Called every time a ball collides with something
    // the object it hits, is passed as a Collider2D parameter
    void OnTriggerEnter2D(Collider2D col)
    {
        // T6 If Bullet hits a wall destroy bullet
        if (col.tag == "Wall")
            Destroy(gameObject);

        // T6 If Bullet hits a wall destroy bullet
        // // the object it hits, is passed as a Collider2D parameter
        if (col.gameObject.tag == "Player")
        {
            // T6 call the sound manger instance, play the exploding spaceship explosion sound file
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);

            // T6 Change from regulars SpaceShip to exploded ship image
            // reference our collision and get our SpriteRenderer and assign it the exploded ship Image
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            // T6 detroy our alienBullet
            Destroy(gameObject);

            // keep the exploded ship image on the screem for a half second.
            DestroyObject(col.gameObject, 0.5f);
        }

        // T6 If Alien Bullet hits Shield destroy both AlienBullet and the section of the shield it hits.
        // refer to the fields using its tag, 
        if (col.tag == "Sheild")
        {
            // T6 destroy the alien bullet
            Destroy(gameObject);

            // T6 destroy the portion of the sheild
            DestroyObject(col.gameObject);
        }
    }   
    // T6 Called when the Game Object alienbullet isn't visible anynore
    //  when the bullet leaves the screen area, destroy it
    void OnBecameInvisible()
    {
        Destroy(gameObject);

    }   


    /* Update is called once per frame NOT USED
    void Update () {
	} */

}
