using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeZombies : MonoBehaviour
{
    [SerializeField] private GameObject zombieRef;
    private List<GameObject> m_ZombieList;


    // Start is called before the first frame update
    void Start()
    {
        m_ZombieList = new List<GameObject>();
        MakeZombie();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ZombieList.Count < 3)
        {
            if(Time.fixedTime%5 == 0)
            {
                MakeZombie();
            }
        }
    }

    private void MakeZombie()
    {
        float randPos = Random.Range(-0.5f, 0.5f);
        Vector3 zombiePos = new Vector3(randPos, 0, randPos);
        GameObject currentZombie = Instantiate(zombieRef, this.transform.position + zombiePos, Quaternion.identity, this.transform);
        m_ZombieList.Add(currentZombie);
    }

    public void RemoveZombie(GameObject i_ZombieToRemove)
    {
        m_ZombieList.Remove(i_ZombieToRemove);
    }
}
