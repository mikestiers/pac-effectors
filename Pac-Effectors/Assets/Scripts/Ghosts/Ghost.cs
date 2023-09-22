using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostOrigin origin { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostScared scared { get; private set; }
    public GhostBehaviour initialBehaviour;
    public AudioClip ghostEaten;
    public AudioClip pacmanEaten;
    public Transform target; // pacman
    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.origin = GetComponent<GhostOrigin>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.scared = GetComponent<GhostScared>();
    }

    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.scared.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.origin != this.initialBehaviour)
            this.origin.Disable();
        else if (this.initialBehaviour != null)
            this.initialBehaviour.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.scared.enabled)
            {
                AudioManager.singleton.PlaySoundEffect(ghostEaten);
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
                AudioManager.singleton.PlaySoundEffect(ghostEaten);
            }
        }
    }
}
