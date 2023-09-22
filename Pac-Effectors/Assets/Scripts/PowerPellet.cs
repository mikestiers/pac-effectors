using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;
    public AudioClip powerMunch;

    protected override void Eat()
    {
        AudioManager.singleton.PlaySoundEffect(powerMunch);
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            Eat();
    }
}
