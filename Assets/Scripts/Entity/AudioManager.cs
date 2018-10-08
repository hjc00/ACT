using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] atkSound;
    public AudioClip hitSound;
    private EntityController entityController;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetEntityController(EntityController p)
    {
        entityController = p;
    }

    public void PlayAtkSound()
    {
        audioSource.clip = atkSound[Random.Range(0, atkSound.Length - 1)];
        audioSource.Play();
    }

    public void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }
}
