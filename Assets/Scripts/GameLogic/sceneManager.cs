using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    public float textSpeed = 20f;
    [SerializeField] GameObject m_text;
    [SerializeField] private GameObject[] instractions;
    [SerializeField] private Canvas m_arrowCanvas;
    [SerializeField] private Camera m_mainCamera;

    private Vector3 viewPos;

    // Start is called beforefthe first frame update
    void Start()
    {
        m_text.SetActive(true);
        m_arrowCanvas.enabled = false;
        foreach (GameObject currObj in instractions)
            currObj.SetActive(false);
        viewPos = m_mainCamera.WorldToViewportPoint(m_text.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(viewPos.y);
        if (m_text.transform.position.y < 250)
        {
            Vector3 textPos = m_text.transform.position;
            Vector3 textUp = m_text.transform.TransformDirection(0, 1, 0);
            textPos += textUp * textSpeed * Time.deltaTime;
            m_text.transform.position = textPos;
        }
        else
        {
            m_text.SetActive(false);
            m_arrowCanvas.enabled = true;
            foreach (GameObject currObj in instractions)
            {
                currObj.SetActive(true);
            }
            //FindObjectOfType<GvrEditorEmulator>().enabled = true;
            //FindObjectOfType<GvrHeadset>().enabled = true;

            if (Input.anyKeyDown)
            {
                RaycastHit hit;
                if (Physics.Raycast(m_mainCamera.transform.position,m_mainCamera.transform.forward,out hit,Mathf.Infinity))
                {
                    hit.collider.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                }
            }
        }
    }
}
