using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject playMenu;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void LevelComplete()
    {
        GameManager.Instance.levelComplete = true;
        Invoke("LevelCompleteInvoke", 0.7f);
    }

    public void LevelCompleteInvoke()
    {
        playMenu.SetActive(false);
        levelCompleteMenu.SetActive(true);
    }

    public void LevelFailed()
    {
        GameManager.Instance.levelFailed = true;
        playMenu.SetActive(false);
        levelFailedMenu.SetActive(true);
    }



    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}

