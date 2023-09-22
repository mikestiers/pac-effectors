using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pellet : MonoBehaviour
{
    public int points = 10;
    public AudioClip munch;
    public Vector3 startingPosition { get; private set; }

    private IEnumerator Start()
    {
        // Wait for a few seconds before setting the starting position.
        yield return new WaitForSeconds(2.0f);

        // Set the starting position after the delay.
        this.startingPosition = transform.position;
        //Debug.Log("Starting Position: " + this.startingPosition);
    }


    protected virtual void Eat()
    {
        AudioManager.singleton.PlaySoundEffect(munch);
        FindObjectOfType<GameManager>().PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            Eat();
    }

    public void ResetAfterEffector(float delay = 0.1f)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false; // Disable physics temporarily.
        }

        StartCoroutine(DelayedReset(delay));
    }

    private IEnumerator DelayedReset(float delay)
    {
        yield return new WaitForSeconds(delay);

        transform.position = startingPosition;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true; // Re-enable physics after resetting position.
        }
    }

}
