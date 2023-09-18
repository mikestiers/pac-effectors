using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable() // after scatter duration expires
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.scared.enabled)
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