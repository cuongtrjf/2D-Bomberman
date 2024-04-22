using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinState()
    {
        int aliveCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }
        if (aliveCount <= 1)
        {
            Invoke(nameof(NextRound), 3f);
        }
    }

    private void NextRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
