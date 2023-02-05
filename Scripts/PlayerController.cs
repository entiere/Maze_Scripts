using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // controls
    public KeyCode left;
    public KeyCode right;

    // range attack variables
    public GameObject bullet;
    public Transform shootPoint;
    private float timeBtwShoot;
    public float startTimeBtwShoot;
    public float laserKnockback;

    // movement variables
    public float speed;
    public int DamageEntity;
    public float spriteScale;

    private Rigidbody2D rb;
    
    public float pointRadius;
    public LayerMask platform;
    public LayerMask player;

    private float timeBtwAttack;
    private int bonus=0;
    public float startTimeBtwAttack;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask otherPlayer;
    public GameObject enemy;
    public int damage;
    public int coin;
    public Transform enemyBloodSplatterPoint;
    public float meleeAttackKnockback;

    public GameObject bloodEffect;
    public Transform bloodEffectPoint;
    public GameObject deathEffect;

    public float flashTime;
    private bool flash;
    private float savedTime;

    public GameObject scoreScreen;

    private Animator anim;

    public virtual void InflictDamage(int dmg, int bns, int price, string personBeaten)
    {
        CollectCoin.TheCoin-=price;
        bonus += bns * damage;
        var d = dmg * (damage + bonus);
        GameObject.Find(personBeaten).GetComponent<PlayerController>().setFlashTrue();
        MakeBlood(enemyBloodSplatterPoint);
        if (personBeaten == "Player2")
        {
            FindObjectOfType<GameManager>().DamageP2(d);
            var dir = Mathf.Sign(enemy.transform.position.x - gameObject.transform.position.x);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(dir * (meleeAttackKnockback+50), meleeAttackKnockback+50);
        }
        else
        {
            FindObjectOfType<GameManager>().DamageP1(d);
        }
    }  

    public virtual void GetDamege4() => InflictDamage(4, 0, 1, "Player2");

    public virtual void GetDamege7() => InflictDamage(7, 0, 2, "Player2");

    public virtual void GetBonus3on2() => InflictDamage(1, 3, 2, "Player2");

    public virtual void GetBonus1on5() => InflictDamage(1, 5, 5, "Player2");

    public virtual void GetSave5()  => InflictDamage(0, 1, 3, "Player2");

    public virtual void GetEntity()  => InflictDamage(DamageEntity, 0, 0, "Player1");

    public virtual void KillHero() => InflictDamage(100, 0, 0, "Player1");

    void Start()
    {
        if(GameObject.Find("ScoreScreen") != null)
        {
            scoreScreen = GameObject.Find("ScoreScreen");
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
    }

    void Update()
    {
        
        if (flash && (int)GetComponent<SpriteRenderer>().color.b == 1)
            GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0.3f);
        else if (flash && Time.time - savedTime > flashTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            flash = false;
        }

        // directional movement
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // shooting card
    
        if(timeBtwShoot > 0) // if can't shoot
        {
            timeBtwShoot -= Time.deltaTime; // decrease delay
            
        }

        // melee
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
            bool playerHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, otherPlayer);
            if (playerHit)
            {
                if (enemy.tag == "Player1")
                {
                    InflictDamage(1, 0, 0, "Player1");
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        // change facing direction based on movement
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(spriteScale, spriteScale, 1);
        } else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-spriteScale, spriteScale, 1);
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    public void MakeBlood(Transform point)
    {
        Instantiate(bloodEffect, point.position, point.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            flash = true;
            savedTime = Time.time;
            Instantiate(bloodEffect, bloodEffectPoint.position, bloodEffectPoint.rotation);
            if(enemy.transform.position.x < gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(laserKnockback+10, laserKnockback+10);
            } else if(enemy.transform.position.x > gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-laserKnockback-10, laserKnockback+10);
            }
        }
        if(collision.gameObject.tag == "Boundary" && !scoreScreen.activeSelf)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            if (gameObject.tag == "Player1")
            {
                FindObjectOfType<GameManager>().DamageP1(FindObjectOfType<GameManager>().P1Health);
            } else if(gameObject.tag == "Player2")
            {
                FindObjectOfType<GameManager>().DamageP2(FindObjectOfType<GameManager>().P2Health);
            }
        }
    }

    public void setFlashTrue()
    {
        flash = true;
        savedTime = Time.time;
    }        
}