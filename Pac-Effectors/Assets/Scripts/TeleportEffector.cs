using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffector : MonoBehaviour
{
    public GameObject otherTeleportEffector;
    public float startingForceMagnitude;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            startingForceMagnitude = otherTeleportEffector.GetComponent<AreaEffector2D>().forceMagnitude;
            otherTeleportEffector.GetComponent<AreaEffector2D>().forceMagnitude = -startingForceMagnitude;

            Invoke("ResetOtherTeleporter", .9f);
        }
    }

    private void ResetOtherTeleporter()
    {
        otherTeleportEffector.GetComponent<AreaEffector2D>().forceMagnitude = startingForceMagnitude;
    }
}
