using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasksCollector : MonoBehaviour
{
    private int m_numOfMasks;
    [SerializeField] GameObject[] m_masksArray;
    [SerializeField] GameObject m_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        initScoreManager();
    }

    public void initScoreManager()
    {
        m_numOfMasks = 0;
        foreach (GameObject currMask in m_masksArray)
        {
            currMask.SetActive(false);
        }
    }

    public void increasePoints()
    {
        m_masksArray[m_numOfMasks].SetActive(true);
        m_numOfMasks++;
        if (m_numOfMasks == 5)
        {
            m_GameManager.GetComponent<GameManager>().GameOver(true);
        }
    }
}


