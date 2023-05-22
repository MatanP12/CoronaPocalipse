using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Canvas m_WinPopup;
    [SerializeField] private GameObject[] m_EnemyMaker;
    [SerializeField] private Camera m_MainCam;
    [SerializeField] private GameObject m_mainPlayer;
    [SerializeField] private MasksCollector m_ScoreManager;
    [SerializeField] private GameObject m_MasksToCollect;
    [SerializeField] private Canvas m_MainUI;
    public bool m_isInGame = true;
    void Start()
    {
        InitGame();
    }




    public void GameOver(bool m_isWin)
    {
        m_isInGame = false;
        foreach (GameObject currEnemyMaker in m_EnemyMaker)
        {
            foreach (Transform currZombie in currEnemyMaker.transform)
            {
                currZombie.GetComponent<ZombieController>().Die();
            }
            currEnemyMaker.SetActive(false);
        }       
        if (m_isWin)
        {
            m_WinPopup.GetComponentInChildren<UnityEngine.UI.Text>().text = "You did it!! You Survived!!";
        }
        else
        {
            m_WinPopup.GetComponentInChildren<UnityEngine.UI.Text>().text = "Too bad... you are now one of them.";
        }
        m_WinPopup.gameObject.SetActive(true);
        m_mainPlayer.GetComponent<Player>().m_isInGame = false;

    }
    private void InitGame()
    {
        m_isInGame = true;
        m_WinPopup.gameObject.SetActive(false);
        foreach (GameObject currEnemyMaker in m_EnemyMaker)
            currEnemyMaker.SetActive(true);
        m_mainPlayer.GetComponent<Player>().initPlayer();
        m_ScoreManager.initScoreManager();
        foreach (Transform currMask in m_MasksToCollect.transform)
            currMask.gameObject.SetActive(true);
        m_mainPlayer.GetComponent<Player>().m_isInGame = true;

    }
    public void playAgain()
    {
        InitGame();
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void AddScore()
    {
        m_ScoreManager.increasePoints();
    }

    public void Alert(bool i_isAlert)
    {
        m_MainUI.transform.GetChild(0).gameObject.SetActive(i_isAlert);
    }
}
