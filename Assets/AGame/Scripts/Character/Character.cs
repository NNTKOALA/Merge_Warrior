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
    [SerializeField] public float turnSpeed = 5f;
    [SerializeField] float chaseRange = 10f;

    public LayerMask characterLayer;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private bool isTargetWithinRange;

    protected string currentAnim = "";
    public bool isDead { get; set; } = false;

/*    protected int maxHealth = 100;
    protected int currentHealth;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;*/

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isDead = false;
        health = 100;
        navMeshAgent = GetComponent<NavMeshAgent>();

        /*        currentHealth = maxHealth;
                GameObject hb = Instantiate(healthBarPrefab, transform);
                hb.transform.localPosition = new Vector3(0, 2, 0);
                healthBar = hb.GetComponent<HealthBar>();
                healthBar.SetMaxHealth(maxHealth);*/
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isDead) return;

        FindClosestTarget();
        if (target != null && isTargetWithinRange)
        {
            MoveToTarget();
            LookAtTarget();
        }
    }

    protected void ChangeAnim(string animName)
    {
        if (anim == null) return; 

        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(animName))
            {
                Debug.Log("play anim " + animName);

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
        //healthBar.SetHealth(currentHealth);
        if (health <= 0)
        {
            Debug.Log("Die");
            OnDead();
        }
    }

    public void OnDead()
    {
        isDead = true;
        ChangeAnim("die");
        Debug.Log("Die");
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

    public bool CheckOnTile(Vector3 tilePosition)
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(tilePosition + Vector3.up, Vector3.down, out hit, 2f, characterLayer);

        return isHit;
    }

    public void UpdateHealthBarColor(Color color)
    {
/*        if (healthBar != null)
        {
            healthBar.SetColor(color);
        }*/
    }

    public void FindClosestTarget()
    {
        EnemyTile[] enemies = FindObjectsOfType<EnemyTile>();
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyTile enemy in enemies)
        {
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
            navMeshAgent.SetDestination(target.position);
        }
    }

    public void LookAtTarget()
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);

            target.LookAt(target);

            if (targetDistance < chaseRange)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }
    }

    void Attack(bool isActive)
    {
        Debug.Log("Attack");
    }
}
