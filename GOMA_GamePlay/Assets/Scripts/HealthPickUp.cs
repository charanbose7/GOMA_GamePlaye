using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthRestored = 20;
    AudioSource healthSource;
    // Start is called before the first frame update
    private void Awake()
    {
        healthSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealed =damageable.Heal(healthRestored);
            if (wasHealed)
            {
                if (healthSource)
                    AudioSource.PlayClipAtPoint(healthSource.clip, gameObject.transform.position, healthSource.volume);

                Destroy(gameObject);
            }
                
        }
    }
}
