using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float speed; // The speed at which the bullet moves
    private bool hit; // A flag to check if the bullet has hit something
    private float direction; // The direction in which the bullet moves (left or right)
    private float BulletLife; // The time that the bullet has existed

    private BoxCollider2D boxCollider; // The box collider component attached to the bullet
    private Animator anim; // The animator component attached to the bullet

    // Start is called before the first frame update
    private void Awake()
    {
        // Get the components from the bullet
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the bullet has hit something, stop updating
        if (hit) return;

        // Move the bullet based on the speed and direction
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        // Increment the bullet life
        BulletLife += Time.deltaTime;

        // If the bullet has existed for more than 5 seconds, deactivate it
        if (BulletLife > 5) gameObject.SetActive(false);
    }

    // When the bullet collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the hit flag to true
        hit = true;
        print(collision.tag);
        if(collision.tag == "Enemy")
        {collision.GetComponent<EnemyBehaviour>().TakeDamage(20);
        }

        // Disable the box collider
        boxCollider.enabled = false;

        // Trigger the Shoot animation
        anim.SetTrigger("Shoot");
    }

    // Set the direction of the bullet
    public void SetDirection(float _direction)
    {
        // Reset the bullet life
        BulletLife = 0;

        // Set the direction
        direction = _direction;

        // Activate the bullet
        gameObject.SetActive(true);

        // Reset the hit flag
        hit = false;

        // Enable the box collider
        boxCollider.enabled = true;

        // Change the scale of the bullet based on the direction
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Deactivate the bullet
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
