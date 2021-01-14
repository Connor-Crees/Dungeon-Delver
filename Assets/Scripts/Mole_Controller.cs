using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mole_Controller : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomMovement());
        Level_Controller.AddEnemy("Mole");
        health = maxHealth;
        healthBarLocalScale = healthBar.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3(health / maxHealth * healthBarLocalScale.x, healthBarLocalScale.y, healthBarLocalScale.z);

        if (health <= 0.0f)
        {
            Level_Controller.RemoveEnemy("Mole");
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
        int rand = Random.Range(-5, 4);
        animator.SetInteger("Direction", rand);
        switch (rand)
        {
            case 1:
                direction = new Vector2(0, 1);
                break;
            case 2:
                direction = new Vector2(1, 0);
                break;
            case 3:
                direction = new Vector2(0, -1);
                break;
            case 4:
                direction = new Vector2(-1, 0);
                break;
            default:
                direction = new Vector2(0, 0);
                break;

        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(RandomMovement());
    }
}
