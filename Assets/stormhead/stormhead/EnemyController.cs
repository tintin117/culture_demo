using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Transform player;
    private GameObject sprite;
    private GameObject sprite2;

    void Start()
    {
<<<<<<< HEAD
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //sprite = GameObject.Find("Magnetic_sprite1");
        // sprite2 = GameObject.Find("Magnetic_sprite2");
       // sprite.SetActive(false);
=======
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GameObject.Find("Magnetic_sprite1");
        // sprite2 = GameObject.Find("Magnetic_sprite2");
        sprite.SetActive(false);
>>>>>>> main
        // sprite2.SetActive(false);
    }

    void Update()
    {
        // Check if player is within range
<<<<<<< HEAD
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //if (distanceToPlayer < 5f) // Adjust the value as needed
        //{
        //    sprite.SetActive(true);
        //    // Move towards the player
        //    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //}
        //else {
        //        sprite.SetActive(false);
        //}
=======
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 5f) // Adjust the value as needed
        {
            sprite.SetActive(true);
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else {
                sprite.SetActive(false);
        }
>>>>>>> main
    }
}
