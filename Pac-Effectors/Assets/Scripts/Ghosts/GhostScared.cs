using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScared : GhostBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled = false; //normal body and eyes switch off
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
                Eaten();
        }
    }

    private void Eaten()
    {
        this.eaten = true;

        Vector3 position = this.ghost.origin.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.origin.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = true;
        this.white.enabled = true;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1) // avoid patrolling back and forth
            {
                index++; // just pick the next one instead of random or wrap back to zero

                if (index >= node.availableDirections.Count)
                    index = 0;
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
