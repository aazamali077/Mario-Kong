using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives;
    private int score;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives= 3;
        score= 0;
    }

    public void levelComplete()
    {
        score += 1000;
    }

    public void LevelFailed()
    {
        lives--;
        if (lives <= 0)
        {
            NewGame();
        }
        else
        {
             
        }
    }
}
