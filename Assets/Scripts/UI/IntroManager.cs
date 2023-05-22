using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Canvas m_Canvas1;
    [SerializeField] private Canvas m_Canvas2;

    private float time = 0;
    private bool m_didSwitch = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Canvas2.enabled = false;
    }

    // Update is called once per frame
    public void switchCanvas()
    {
        Debug.Log("changing canvas");
        m_Canvas1.enabled = false;
        m_Canvas2.enabled = true;
        m_didSwitch = true;
    }
}
