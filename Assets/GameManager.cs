﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameHasEnded = false;
    public float delay =1f;
    public float amountBalls = 0;
    public void Update()
    {
        if (amountBalls >= 3)
        {
            Debug.Log("winnah winnah chicka dinna");
            WinGame();
        }
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {

        }
        gameHasEnded = true;
        
        Invoke("Restart", delay);
    }

    void Restart()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("DeathScreen");
        Debug.Log("Game Over");
    }
    void WinGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("WinScreen");
    }

}
