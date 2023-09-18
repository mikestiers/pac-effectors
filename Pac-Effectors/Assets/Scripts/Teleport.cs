using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignoring Z because pacmman is on a diff z;
        Vector3 position = collision.transform.position;
        position.x = this.destination.position.x;
        position.y = this.destination.position.y;
        collision.transform.position = position;
    }
}
