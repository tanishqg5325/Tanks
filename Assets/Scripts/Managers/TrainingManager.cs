using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour {

    public GameObject m_tank, m_trainer1, m_trainer2;
    public Text m_MessageText;
    public GameObject EndMenu;
    public GameObject TrainAgain;
    public GameObject Compete;
    public GameObject StartMenuUI;
    private bool m_dead_tank, m_dead_trainer1, m_dead_trainer2;

    void Start ()
    {
        Time.timeScale = 0f;
        m_dead_tank = false;
        m_dead_trainer1 = false;
        m_dead_trainer2 = false;
        EndMenu.SetActive(false);
        TrainAgain.SetActive(false);
        Compete.SetActive(false);
        StartMenuUI.SetActive(true);
    }
	
	void Update ()
    {
        m_dead_tank = m_tank.GetComponent<TankHealth>().m_Dead;
        m_dead_trainer1 = m_trainer1.GetComponent<TankHealth>().m_Dead;
        m_dead_trainer2 = m_trainer2.GetComponent<TankHealth>().m_Dead;
        if(!m_dead_tank && m_dead_trainer1 && m_dead_trainer2)
        {
            m_MessageText.text = "You Win!";
            EndMenu.SetActive(true);
            Compete.SetActive(true);
        }
        else if(m_dead_tank)
        {
            m_MessageText.text = "You Lose!";
            EndMenu.SetActive(true);
            TrainAgain.SetActive(true);
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void Training()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Training");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        StartMenuUI.SetActive(false);
    }
}
