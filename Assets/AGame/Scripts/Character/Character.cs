using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] LayerMask playerTile;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] CharLevel charLevel;
    [SerializeField] CharType charType;
    [SerializeField] protected float turnSpeed = 5f;
    [SerializeField] protected float chaseRange = 10f;

    public LayerMask characterLayer;

    private NavMeshAgent navMeshAgent;
    protected Transform target;
    protected bool isTargetWithinRange;

    protected string currentAnim = "";
    public bool isDead { get; set; } = false;

    protected int maxHealth = 100;
    protected int currentHealth;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    protected virtual void Start()
    {
        isDead = false;
        health = 100;
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        GameObject hb = Instantiate(healthBarPrefab, transform);
        hb.transform.localPosition = new Vector3(0, 2, 0);
        healthBar = hb.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    protected virtual void Update()
    {
        if (isDead) return;
    }

    protected void ChangeAnim(string animName)
    {
        if (anim == null) return;

        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(animName))
            {
                if (!string.IsNullOrEmpty(currentAnim))
                {
                    anim.ResetTrigger(currentAnim);
                }
                currentAnim = animName;
                anim.SetTrigger(currentAnim);
            }
            else
            {
                currentAnim = "";
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            OnDead();
        }
    }

    public void OnDead()
    {
        isDead = true;
        ChangeAnim("die");
        healthBar.gameObject.SetActive(false);
        Destroy(gameObject, .1f);
    }

    public void OnWin()
    {
        ChangeAnim("win");
    }

    public int GetHealth()
    {
        return health;
    }

    public CharLevel GetLevel()
    {
        return charLevel;
    }

    public CharType GetCharType()
    {
        return charType;
    }

    public void FindClosestTarget()
    {
        Character[] enemies = FindObjectsOfType<Character>();
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Character enemy in enemies)
        {
            if (enemy == this || enemy.tag == this.tag)
            {
                continue;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
        isTargetWithinRange = (target != null && closestDistance <= chaseRange);
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            anim.SetBool("isMoving", true);
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void LookAtTarget()
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

            if (targetDistance < chaseRange)
            {
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {
        ChangeAnim("attack");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    protected virtual void OnNewGame()
    {

    }
}
