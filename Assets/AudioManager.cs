using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")] 
    [SerializeField] private AudioSource pickupSoundEffect;
    [SerializeField] private AudioSource dropSoundEffect;

    public void PlayPickupSound() {
        pickupSoundEffect.Play();
    }

    public void PlayDropSound() {
        dropSoundEffect.Play();
    }
}
