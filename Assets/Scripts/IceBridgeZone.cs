using UnityEngine;
using UnityEngine.InputSystem;

public class IceBridgeZone : MonoBehaviour
{
    public GameObject bridge;
    public ParticleSystem iceEffect;
    public AudioClip castSound;

    bool playerInside;
    bool used;
    PlayerSpells spells;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        spells = other.GetComponent<PlayerSpells>();
        playerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
    }

    void Update()
    {
        if (used || !playerInside || spells == null || !spells.hasIce) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            used = true;
            bridge.SetActive(true);

            if (iceEffect != null) iceEffect.Play();
            if (castSound != null)
                AudioSource.PlayClipAtPoint(castSound, transform.position);
        }
    }
}