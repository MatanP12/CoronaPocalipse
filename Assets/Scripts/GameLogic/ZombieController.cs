using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] float m_hurtDistance = 0.7f;
    [SerializeField] float m_alertDistance = 1.5f;

    private GameObject m_Player;
    private Animator m_ThisAnimator;
    private RaycastHit m_ZombieHit;
    private Ray m_ZombieRay;
    private bool m_isPlayerFound;
    private bool m_isCloseToPlayer = false;
    private float m_playerDistance = 100;
    private bool m_isDied = false;
    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        this.m_ThisAnimator = this.GetComponent<Animator>();
        m_isPlayerFound = false;
        m_ThisAnimator.SetFloat("MoveSpeed", 0f);
    }

    private void Update()
    {
        //Debug.Log("Time: " + Time.fixedTime);
        if (!m_isDied)
        {
            if (Time.fixedTime % 1 == 0 && !m_isPlayerFound)
            {
                LookForPlayer();
            }
            if (m_isPlayerFound)
            {
                m_playerDistance = Vector3.Distance(this.transform.position, m_Player.transform.position);
                if (m_playerDistance > m_hurtDistance)
                {
                    MoveTowardsPlayer();
                }
                else
                {
                    AttackPlayer();
                }
            }
            if (m_playerDistance <= m_alertDistance && !m_isCloseToPlayer)
            {
                AddToPlayerCloseEnemy();
            }
            else if (m_playerDistance > m_alertDistance && m_isCloseToPlayer)
            {
                RemoveFromPlayerCloseEnemy();
            }
        }

    }

    private void AddToPlayerCloseEnemy()
    {
        m_Player.GetComponent<Player>().m_enemiesAround++;
        m_isCloseToPlayer = true;
        Debug.Log("trying tp add zpmbie, zombies: " + m_Player.GetComponent<Player>().m_enemiesAround + " distance: " + m_playerDistance);

    }

    private void RemoveFromPlayerCloseEnemy()
    {
        m_isCloseToPlayer = false;
        m_Player.GetComponent<Player>().m_enemiesAround--;
        Debug.Log("trying tp remove zpmbie, zombies: " + m_Player.GetComponent<Player>().m_enemiesAround);
        
       
    }
    private void MoveTowardsPlayer()
    {
        Vector3 PlayerLocation = m_Player.transform.position;
        PlayerLocation.y = this.transform.position.y;
        this.transform.LookAt(PlayerLocation);
        float step = 0.2f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, PlayerLocation, step);
    }
    private void LookForPlayer()
    {
        m_ZombieRay = new Ray(transform.position, m_Player.transform.position - this.transform.position);
        if (Physics.Raycast(m_ZombieRay, out m_ZombieHit, 5f))
        {
            if (m_ZombieHit.collider.name == m_Player.name)
            {
                Vector3 PlayerLocation = m_Player.transform.position;
                PlayerLocation.y = this.transform.position.y;
                this.transform.LookAt(PlayerLocation);
                m_isPlayerFound = true;
                m_ThisAnimator.SetFloat("MoveSpeed", 4f);

            }
        }
    }

    private void AttackPlayer()
    {
        m_ThisAnimator.SetFloat("MoveSpeed", 0f);
        m_ThisAnimator.SetBool("Attack", true);
        this.m_Player.GetComponent<Player>().GotAttacked();
        this.m_isPlayerFound = false;
    }
    public void Die()
    {
        m_isDied = true;
        this.m_ThisAnimator.SetBool("Dead", true);
        this.transform.parent.GetComponent<MakeZombies>().RemoveZombie(this.gameObject);
        if (m_isCloseToPlayer)
        {
            RemoveFromPlayerCloseEnemy();
        }
        Destroy(this.gameObject, 0.75f);
    }
}

