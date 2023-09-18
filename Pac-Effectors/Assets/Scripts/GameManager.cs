using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public GameObject pelletEffector;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int ghostMultiplier { get; private set; }
    public bool effectorEnabled = false;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
            NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
            this.ghosts[i].gameObject.SetActive(true);

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
            this.ghosts[i].ResetState();

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
            GameOver();
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);

        if (!LevelCleared())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        PelletEaten(pellet);
        EnableEffector();
        CancelInvoke(); // reset ghost multiplier timer if it is still in progress
        StartCoroutine(DisableEffectorAndResetGhostMultiplier(pellet.duration));

        //Invoke(nameof(DisableEffector), pellet.duration);
        //Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private IEnumerator DisableEffectorAndResetGhostMultiplier(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Disable the effector after the delay
        DisableEffector();

        // Reset ghost multiplier after the same delay
        ResetGhostMultiplier();
    }

    private void EnableEffector()
    {
        pelletEffector.SetActive(true);
        effectorEnabled = true;
        Pellet[] pellets = FindObjectsOfType<Pellet>();
        foreach (Pellet pellet in pellets)
            pellet.ResetAfterEffector();
    }

    private void DisableEffector()
    {
        pelletEffector.SetActive(false); // Disable the effector after pellet.duration seconds
        effectorEnabled = false;
        Pellet[] pellets = FindObjectsOfType<Pellet>();
        foreach (Pellet pellet in pellets)
            pellet.ResetAfterEffector();
    }    

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

    public bool LevelCleared()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
                return true;
        }

        return false;
    }
}
