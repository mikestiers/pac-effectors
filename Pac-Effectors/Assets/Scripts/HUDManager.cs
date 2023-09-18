using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text score;
    public Text lives;
    public Text winLose;

    private void Update()
    {
        score.text = $"Score\n {FindObjectOfType<GameManager>().score}";
        lives.text = $"Lives: {FindObjectOfType<GameManager>().lives}";
        if (FindObjectOfType<GameManager>().lives <= 0)
        {
            winLose.text = "You lost!\nContinue?";
            winLose.enabled = true;
        }
        else if (!FindObjectOfType<GameManager>().LevelCleared())
        {
            winLose.text = "Way to go, champ!";
            winLose.enabled = true;
        }
        else
            winLose.enabled = false;
    }
}
