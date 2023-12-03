using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ForceFieldController : MonoBehaviour
{
    public GameObject forceFieldPrefab; // Assign the force field prefab in the Unity Editor
    public float forceStrength = 10.0f; // Adjust the strength of the force field as needed
    private GameObject sprite;
    void Start()
    {
        sprite = GameObject.Find("Magnetic_sprite");
        sprite.SetActive(false);
        print(sprite);
    }
    void Update()
    {
        //CreateForceField();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        

        if (Input.GetButton("Fire1"))
        {
            CreateForceField(horizontalInput, verticalInput);
        }
        //if (Input.GetKeyUp(KeyCode.E))
        //{
        //    DisableSprite();
        //}
    }

    void CreateForceField(float horizontal, float vertical)
    {
        // Get the direction the character is facing
        //Vector3 forceFieldDirection = transform.forward;

        // Instantiate the force field prefab
        Invoke("EnableAndDisableSprite", 0f);
        EnableAndDisableSprite(horizontal, vertical);

        //GameObject forceField = Instantiate(forceFieldPrefab, transform.position, Quaternion.identity);

        // Apply a force to all nearby enemies
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100.0f); // Adjust the radius as needed
        print(hitColliders);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                print(hitCollider.gameObject);
                Rigidbody enemyRigidbody = hitCollider.GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    // Calculate force direction towards the player
                    Vector3 forceDirection = transform.position - hitCollider.transform.position;

                    // Apply the force
                    enemyRigidbody.AddForce(forceDirection.normalized * forceStrength, ForceMode.Impulse);
                }
            }
        }

        // Destroy the force field after a certain time (you can adjust the time as needed)

    }
    void EnableAndDisableSprite(float horizontal, float vertical)
    {
        Vector3 offset = new Vector3(horizontal, vertical, 0);
        sprite.SetActive(true); // Enable the sprite
        sprite.transform.position = transform.position + offset; ;

        // Schedule the sprite to be set inactive after a short delay (you can adjust the time as needed)
        Invoke("DisableSprite", 1.0f);
    }

    void DisableSprite()
    {
        sprite.SetActive(false); // Disable the sprite
    }
}
