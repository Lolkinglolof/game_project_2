using UnityEngine;

public class LayerIgnoreEnemies : MonoBehaviour
{
    [SerializeField] private LayerMask whatToCollideWith; // Layer mask to define objects enemies should collide with normally

    private void Start()
    {
        // Assuming your enemy GameObject has the "EnemieLayers" Layer assigned
        // No need to change the layer here in the script
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the "WallBoundry" tag and ignore it
        if (collision.gameObject.CompareTag("block"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider);
            return; // Exit the function after ignoring collision
        }

        // Check if the collided object's layer is in the "whatToCollideWith" LayerMask
        if ((whatToCollideWith.value & (1 << collision.gameObject.layer)) != 0)
        {
            // This is an object the enemy should collide with (in allowed collision list)
            // Handle normal collision logic here (e.g., bounce off wall)
        }
        else
        {
            // This is an object the enemy should potentially ignore (not a wall or in allowed collision list)
            // Handle additional logic here (optional)
            //  - You could destroy the object or play a sound effect
        }
    }
}