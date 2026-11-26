using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    public AudioClip pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerSpells spells = other.GetComponent<PlayerSpells>();
        if (spells == null) return;

        spells.hasIce = true;

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        Destroy(gameObject);
    }
}

