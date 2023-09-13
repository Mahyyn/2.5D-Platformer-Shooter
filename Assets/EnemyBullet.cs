using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float LifeTime;
    [SerializeField] float BulletSpeed;
    int damage;
    void Start()
    {
        Destroy(gameObject, LifeTime);
        damage = GetComponentInParent<EnemyBehaviour>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = -BulletSpeed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().DamageTaken(damage);
            Destroy(gameObject);
        }
    }

}
