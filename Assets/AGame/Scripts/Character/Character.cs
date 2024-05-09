using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] protected float health = 0f;
    [SerializeField] protected float damage = 0f;
    [SerializeField] protected float range = 0f;
    [SerializeField] protected float level = 0f;
    [SerializeField] CharType charType;

    [SerializeField] protected Collider[] targetInRange;

    protected string currentAnim = "";
    public bool isDead { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void TakeDamage()
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Die");
            OnDead();
        }
    }

    public void MergeCharacter()
    {

    }

/*    public Character FindClosetEnemy()
    {
        targetInRange = Physics.OverlapSphere(transform.position, range, characterMask);
        Collider nearestEnemy = null;
        if (targetInRange.Length == 0)
        {
            return null;
        }
        else
        {
            float minimumDistance = Mathf.Infinity;

            foreach (Collider enemy in targetInRange)
            {
                if (enemy == this.GetComponent<Collider>())
                {
                    continue;
                }
                if (enemy.GetComponent<Character>().isDead)
                {
                    continue;
                }
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null)
            {
                return nearestEnemy.GetComponent<Character>();
            }
            else
            {
                return null;
            }
        }
    }*/

    public void LookAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    public void OnMove()
    {
        ChangeAnim("move");
        Debug.Log("Move");
    }

    public void OnAttack()
    {
        ChangeAnim("attack");
        Debug.Log("Attack");
    }

    public void OnDead()
    {
        isDead = true;
        ChangeAnim("die");
        Debug.Log("Die");
    }
}
