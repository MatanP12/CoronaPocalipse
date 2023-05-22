using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private RaycastHit m_hit;
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out m_hit, Mathf.Infinity))
            {
                Debug.Log("Hit!:" + m_hit.collider.name);
                if(m_hit.collider.gameObject.GetComponent<Button>())
                {
                    m_hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void GoToIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
