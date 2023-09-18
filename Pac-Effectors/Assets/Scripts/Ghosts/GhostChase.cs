using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable() // after chase duration expires
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.scared.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availabDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availabDirection.x, availabDirection.y, 0.0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availabDirection;
                    minDistance = distance;
                }

                this.ghost.movement.SetDirection(direction);
            }
        }
    }
}
