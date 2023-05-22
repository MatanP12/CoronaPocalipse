using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject m_explosionObject;
    [SerializeField] float explosionTimer;
    [SerializeField] float m_radius;

    private float countDown;
    private bool hasExploded;
    // Start is called before the first frame update
    void Start()
    {
        countDown = explosionTimer;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0 && !hasExploded)
        {
            Explode();
        }
    }
    
    private void Explode()
    {
        GameObject explosinpart = Instantiate(m_explosionObject, transform.position, transform.rotation, this.transform.parent.transform);
        Destroy(explosinpart, 1);

        Collider[] collider = Physics.OverlapSphere(transform.position, m_radius);
        foreach(Collider nearByObj in collider)
        {
            if (nearByObj.gameObject.tag == "Zombie")
            {
                Rigidbody otherRB = nearByObj.GetComponent<Rigidbody>();
                if (otherRB != null)
                {
                    nearByObj.GetComponent<ZombieController>().Die();
                }
            }
            
        }

        hasExploded = true;
        Destroy(this.gameObject);
    }
}
