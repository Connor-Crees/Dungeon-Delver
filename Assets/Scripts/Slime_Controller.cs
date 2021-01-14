using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime_Controller : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public Animator animator;
    public Rigidbody2D ridigbody_2d;
    public float health;
    public float maxHealth = 100.0f;
    public Transform healthBar;
    Vector3 healthBarLocalScale;
    public GameObject deathRemains;
    public GameObject player;
    bool attackCooldown = false;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomMovement());
        Level_Controller.AddEnemy("Slime");
        health = maxHealth;
        healthBarLocalScale = healthBar.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3(health / maxHealth * healthBarLocalScale.x, healthBarLocalScale.y, healthBarLocalScale.z);

        if (moving == false)
        {
            StartCoroutine(RandomMovement());
        }

        if (Vector2.Distance(player.transform.position, transform.position) < 1.5f && attackCooldown == false)
        {
            StartCoroutine(AttackPlayer());
        }

        if (health <= 0.0f)
        {
            Level_Controller.RemoveEnemy("Slime");
            deathRemains.transform.position = transform.position;
            Instantiate(deathRemains);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        ridigbody_2d.MovePosition(ridigbody_2d.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Arrow"))
        {
            health -= 25.0f;
        }
    }

    IEnumerator RandomMovement()
    {
        moving = true;
        int rand = Random.Range(-5, 4);
        switch (rand)
        {
            case 1:
                direction = new Vector2(0, 1);
                animator.SetBool("Walk", true);
                break;
            case 2:
                direction = new Vector2(1, 0);
                animator.SetBool("Walk", true);
                break;
            case 3:
                direction = new Vector2(0, -1);
                animator.SetBool("Walk", true);
                break;
            case 4:
                direction = new Vector2(-1, 0);
                animator.SetBool("Walk", true);
                break;
            default:
                direction = new Vector2(0, 0);
                animator.SetBool("Walk", false);
                break;

        }
        yield return new WaitForSeconds(1.0f);
        moving = false;
    }

    IEnumerator AttackPlayer()
    {
        attackCooldown = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        player.GetComponent<Player_Controller>().TakeDamage(1);
        yield return new WaitForSeconds(2.0f);
        attackCooldown = false;
    }
}
