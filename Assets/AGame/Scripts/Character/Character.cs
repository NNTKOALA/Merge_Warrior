using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] LayerMask playerTile;
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] CharLevel charLevel;
    [SerializeField] CharType charType;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    public Transform target;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    //Transform target;

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
        CheckOnTile();

/*        currentHealth = maxHealth;
        GameObject hb = Instantiate(healthBarPrefab, transform);
        hb.transform.localPosition = new Vector3(0, 2, 0);
        healthBar = hb.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);*/
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }

/*        if (isDead)
        {
            enabled = false;
        }
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }*/
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(animName))
            {
                Debug.Log("play anim " + animName);

                anim.ResetTrigger(currentAnim);
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
        isProvoked = true;
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

    public bool CheckOnTile()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * .2f, Color.red);

        RaycastHit hit;

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down * 2f, out hit, 0.4f, playerTile);
    }

    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
