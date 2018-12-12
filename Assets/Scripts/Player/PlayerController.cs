using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int curAmmo = 3;
    public int maxAmmo = 3;
    public int damage = 1;
    public int health = 3;
    public int maxHealth = 3;


    public float reloadTime = 2f;
    public float speed = 10f;

    public bool invulnerable = false;

    public GameObject bombPrefab;
    public Transform dropPoint;

    public Transform[] points; // Top Right Bottom Left

    public Rigidbody2D rb;
    private float nextReload;

    private Vector3 pos;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position; 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(curAmmo > 0)
            {
                DropBomb();
                curAmmo--;
            }
        }

        //Reload bombs automatically
        if(Time.time > nextReload)
        {
            nextReload = Time.time + reloadTime;
            if (curAmmo < maxAmmo)
            {
                curAmmo++;
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {
            pos += Vector3.up;
            dropPoint.position = points[0].position;
        }
        if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {
            pos += Vector3.left;
            dropPoint.position = points[3].position;

        }
        if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {
            pos += Vector3.down;
            dropPoint.position = points[2].position;

        }
        if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {
            pos += Vector3.right;
            dropPoint.position = points[1].position;

        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void DropBomb()
    {
        //Add Sound?
        //DropPoint Position needs to be +1 after the player
        Instantiate(bombPrefab, dropPoint.position, bombPrefab.transform.rotation);
    }

    void Die()
    {
        //Death Animation
        //Trigger check for players from game system script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "powerup") {
            //Invoke powerup Action
        }
    }
}
