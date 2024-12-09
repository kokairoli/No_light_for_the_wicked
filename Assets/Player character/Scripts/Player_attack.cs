using UnityEngine;

public class Player_attack : MonoBehaviour
{

    private float timeToAttack;
    public float startTimeToAttack;

    public Transform attackPos;
    public LayerMask whatAreEnemies;
    public float attackRange;

    public Animator animator;

    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToAttack <= 0) {

            animator.SetBool("isAttacking", false);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatAreEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponentInParent<Enemy>().takeDamage(damage);
                }
                timeToAttack = startTimeToAttack;
                animator.SetBool("isAttacking", true);
            }


        }
        else
        {
            timeToAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
