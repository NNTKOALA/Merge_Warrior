using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] LayerMask playerTile;
    [SerializeField] protected int health = 0;
    [SerializeField] protected int damage = 0;
    [SerializeField] protected int range = 0;
    [SerializeField] CharLevel charLevel;
    [SerializeField] CharType charType;

    [SerializeField] protected Collider[] targetInRange;

    protected string currentAnim = "";
    public bool isDead { get; set; } = false;

    private Tile tile;

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
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1f, Color.red);

        RaycastHit hit;

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down * 2f, out hit, 0.4f, playerTile);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Warrior") || other.CompareTag("Archer"))
        {
            Character otherCharacter = other.GetComponent<Character>();
            Character thisCharacter = GetComponent<Character>();

            if (otherCharacter.GetLevel() == thisCharacter.GetLevel() && otherCharacter.GetCharType() == thisCharacter.GetCharType())
            {
                Debug.Log("Merge");
                GameObject newCharacterPrefab = tile.GetCharacterPrefab(thisCharacter.GetCharType(), thisCharacter.GetLevel());
                Instantiate(newCharacterPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Change Position");
                Vector3 temp = other.transform.position;
                other.transform.position = transform.position;
                transform.position = temp;
            }
        }
    }
}
