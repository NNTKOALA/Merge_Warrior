using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] LayerMask playerTile;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] protected int attackRange;
    [SerializeField] CharLevel charLevel;
    [SerializeField] CharType charType;
    [SerializeField] protected float turnSpeed = 5f;
    [SerializeField] protected float chaseRange = 10f;

    protected float attackCooldown = 1f;
    protected float lastAttackTime = 0f;

    public LayerMask characterLayer;

    protected NavMeshAgent navMeshAgent;
    protected Transform target;
    protected bool isTargetWithinRange;

    protected string currentAnim = "";
    public bool isDead { get; set; } = false;
    public bool isAttack { get; set; } = false;

    public bool isEnemy;

    public int currentHealth;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;

    protected virtual void Start()
    {
        isDead = false;
        isAttack = false;
        OnIdle();
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentHealth = health;
        GameObject hb = Instantiate(healthBarPrefab, transform);
        hb.transform.localPosition = new Vector3(0, 2, 0);
        healthBar = hb.GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(health);
        if (gameObject.tag == "Enemy")
        {
            healthBar.SetHealthBarColor(Color.red);
        }
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (!GameManager.Instance.isFighting)
        {
            return;
        }
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
        if (health < 0)
        {
            OnDead();
        }
    }

    protected virtual void OnIdle()
    {
        isAttack = false;
        anim.SetBool("isMoving", false);
    }

    protected virtual void OnAttack()
    {
        isAttack = true;
        ChangeAnim("attack");
    }

    public void OnDead()
    {
        ChangeAnim("die");
        healthBar.gameObject.SetActive(false);
        isDead = true;
        Destroy(gameObject, .2f);
    }

    public void OnWin()
    {
        ChangeAnim("win");
        GameManager.Instance.WinGame();
    }

    public void OnLose()
    {
        GameManager.Instance.LoseGame();
    }

    public int GetHealth()
    {
        return health;
    }

    protected virtual void FindClosestEnemy()
    {
        Character[] enemies = FindObjectsOfType<Character>();
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (Character enemy in enemies)
        {
            if (enemy == this || enemy.tag == this.tag)
            {
                continue;
            }

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestTarget = enemy.transform;
                minDistance = distanceToEnemy;
            }
        }

        target = closestTarget;
    }

/*    protected virtual void LookAtTarget(Vector3 target)
    {
        anim.SetBool("isMoving", false);
        transform.LookAt(target);
    }*/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public virtual void OnNewGame()
    {

    }

    public void StartBattle()
    {
        GameManager.Instance.isFighting = true;
    }

    public void ResetBattle()
    {
        GameManager.Instance.isFighting = false;
    }
}
