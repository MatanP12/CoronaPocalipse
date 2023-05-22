using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private MasksCollector m_ScoreManager;
    [SerializeField] private Camera m_MainCam;
    [SerializeField] private GameObject m_WaterGun;
    [SerializeField] private GameObject m_GrenadeRef;
    [SerializeField] private GameObject m_GrenadeFather;
    [SerializeField] private GameObject m_lifeMonitor;
    [SerializeField] private GameObject m_GameManager;
    [SerializeField] private float m_hurtFactor = 2;


    private Ray m_ray;
    private RaycastHit m_hit;
    private float m_throwPower = 5F;
    private float KeyDownTime = 0;
    private bool m_isHeldDown = false;
    private float m_KeyDownLengh;
    private float m_currentLife = 100f;
    public int m_enemiesAround = 0;

    private bool m_isDead = false;

    public bool m_isInGame;

    void Start()
    {
        initPlayer();
    }

    public void initPlayer()
    {
        m_currentLife = 100f;
        m_lifeMonitor.GetComponent<ProgressBarCircle>().BarValue = m_currentLife;
        m_enemiesAround = 0;
        this.transform.position = Vector3.zero;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyDownTime = Time.time;
            m_isHeldDown = true;
            if (m_isInGame)
            {
                if (Physics.Raycast(m_MainCam.transform.position, m_MainCam.transform.forward, out m_hit, Mathf.Infinity))
                {
                    Debug.Log("hit:" + m_hit.collider.name);
                    if (m_hit.collider.GetComponent<PlayerMove>())
                    {
                        m_hit.transform.GetComponent<PlayerMove>().Move();
                    }
                    else
                    {
                        shootGun();
                        if (m_hit.collider.name.Contains("Zombie"))
                        {
                            m_hit.collider.GetComponent<ZombieController>().Die();
                        }
                    }
                }
                else
                {
                    if (m_isInGame)
                    {
                        shootGun();
                    }
                }
            }
            else
            {
                if (Physics.Raycast(m_MainCam.transform.position, m_MainCam.transform.forward, out m_hit, Mathf.Infinity))
                {
                    Debug.Log("Hit" + m_hit.collider.gameObject.name);
                    if (m_hit.collider.gameObject.GetComponent<Button>())
                    {
                        m_hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }
        }

        if (m_isHeldDown && !Input.anyKey)
        {
            float KeyDownDuration = Time.time - KeyDownTime;
            if (KeyDownDuration > 1)
            {
                throwGrenade();
            }
            m_isHeldDown = false;
        }

        if (m_enemiesAround > 0)
        {
            m_GameManager.GetComponent<GameManager>().Alert(true);
        }
        else
        {
            m_GameManager.GetComponent<GameManager>().Alert(false);
        }
        
    }

    private void shootGun()
    {
        m_WaterGun.GetComponent<ParticleSystem>().Play();

    }

    private void throwGrenade()
    {
        GameObject currentZombie = Instantiate(m_GrenadeRef, this.transform.position, Quaternion.identity, m_GrenadeFather.transform);
        currentZombie.GetComponent<Rigidbody>().velocity = transform.TransformDirection(m_MainCam.transform.forward * m_throwPower); 
    }
    void OnCollisionEnter(Collision other)
    {    
        if (other.gameObject.name.Contains("Mask"))
        {
            other.collider.transform.parent.gameObject.SetActive(false);
            m_GameManager.GetComponent<GameManager>().AddScore();
        }
    }

    public void GotAttacked()
    {
        m_currentLife -= m_hurtFactor;
        m_lifeMonitor.GetComponent<ProgressBarCircle>().BarValue = m_currentLife;
        if (m_currentLife <= 0)
        {
           FinishGame();
        }
    }


    private void FinishGame()
    {
        m_GameManager.GetComponent<GameManager>().GameOver(false);
        m_isDead = true;
    }
}

