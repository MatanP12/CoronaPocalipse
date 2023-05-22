using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject m_Player;
    public void Move()
    {
        m_Player.transform.position = this.transform.position - this.transform.position.y * Vector3.up;
    }
}