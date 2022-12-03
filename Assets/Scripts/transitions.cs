using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitions : MonoBehaviour
{
    public Animator anim;
    public bool exitPressed = false;
    public GameObject messageBox;
    public GameObject gameOver;
    public GameObject pauseBox;

    public void transition()
    {
        StartCoroutine(transitionEnum());
    }

    public void exitToMenu()
    {
        exitPressed = true;
        StartCoroutine(exitMenu());
    }

    public void exitGame()
    {
        StartCoroutine(exit());
    }

    IEnumerator exitMenu()
    {
        messageBox.SetActive(false);
        pauseBox.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        exitPressed = false;
        SceneManager.LoadScene("HomeMenu");
    }

    IEnumerator exit()
    {
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    IEnumerator transitionEnum()
    {
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("GameScene");
    }
}
