using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    public int damage;
    [SerializeField] int health = 100;
    [SerializeField] GameObject bullet;
    [SerializeField] private Transform bulletRelease;
    [SerializeField] Animator anim;
    bool dying = false;
    bool canShot = true;
    Transform Player;
    // private EnemyPatrol 

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }


    private void Update()
    {
        if(Vector3.Distance(transform.position,Player.position) <= range && canShot)
        {
            canShot = false;
            Invoke("Shoot", attackCooldown);
        }

        if(Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, bulletRelease.position, Quaternion.identity,transform);
        canShot = true;
    }


    public void TakeDamage(int dmg)
    {
        if (dying) { return; }
        health -= dmg;
        print("taking dmg");
        if (health <= 0)
        {
            TMP_Text killsText = GameObject.Find("Kill Counter").GetComponent<TMP_Text>();
            int kills = int.Parse(killsText.text);
            kills++;
            killsText.text = kills.ToString();
            dying = true;
            //play animation
            anim.SetTrigger("Die");
            Invoke("Die", 0.5f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    
}

