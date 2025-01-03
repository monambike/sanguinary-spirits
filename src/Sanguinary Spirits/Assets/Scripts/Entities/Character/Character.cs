// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the behavior of the character sound.
/// </summary>
public class Character : MonoBehaviour
{
    // Reference to the FirstPersonController script.
    public FirstPersonController firstPersonController;

    public float currentLife = 100f;

    public const float maxLife = 100f;

    private const float minLife = 0f;

    [Header("Voice")]

    /// <summary>
    /// Audio source for voice sounds like breathing.
    /// </summary>
    public AudioSource voiceSource;

    /// <summary>
    /// Sound clip for breathing.
    /// </summary>
    public AudioClip breathing;

    /// <summary>
    /// Sound clip for pain.
    /// </summary>
    public AudioClip pain;

    [Header("Heartbeat")]

    public AudioSource heartbeatSource;

    /// <summary>
    /// Sound clip for heartbeat.
    /// </summary>
    public AudioClip heartbeat;

    [Header("Footsteps")]

    /// <summary>
    /// Audio source for footsteps sounds.
    /// </summary>
    public AudioSource footstepsSource;

    /// <summary>
    /// Sound clip for footsteps.
    /// </summary>
    public AudioClip footsteps;

    [Header("Damage")]

    public Image DamageIndicator;

    public bool queuePainSound = false;

    private void Start()
    {
        DamageIndicator.gameObject.SetActive(true);
    }

    private void Update()
    {
        CalculateDamageIndicatorAlpha();

        // Update sound effects.
        Breathing();
        Footsteps();
        Heartbeat();

        if (currentLife < minLife)
            // Load the Game Over scene.
            SceneManager.LoadScene($"{Scenes.Death}");
    }

    private void CalculateDamageIndicatorAlpha()
    {
        // Getting player's life inversely proportional transparency alpha percentage.
        float alphaPercentage = 1 - (currentLife / maxLife);

        DamageIndicator.color = new Color(1f, 1f, 1f, alphaPercentage);
    }

    private void Heartbeat()
    {
        heartbeatSource.clip = heartbeat;
        heartbeatSource.enabled = currentLife < 60f;
        heartbeatSource.pitch = (currentLife / 100f) switch
        {
            <= 0.1f => 1.60f, // Less or equal to 10%.
            <= 0.3f => 1.40f, // Less or equal to 30%.
            _ => 1.15f // More than 30%.
        };
    }

    /// <summary>
    /// Handle character breathing sound.
    /// </summary>
    private void Breathing()
    {
        if (!queuePainSound)
        {
            // Enable breathing sound if player is sprinting or has remaining sprint duration.
            voiceSource.enabled = firstPersonController.isSprinting || firstPersonController.sprintRemaining < firstPersonController.sprintDuration;
            voiceSource.clip = breathing;

            // If player is sprinting, make the breathing sound faster.
            if (firstPersonController.isSprinting)
                voiceSource.pitch = 1.5f;
            // Otherwise, play breathing sound at normal speed.
            else
                voiceSource.pitch = 1.0f;
        }
    }

    /// <summary>
    /// Handle character footsteps sound.
    /// </summary>
    private void Footsteps()
    {
        footstepsSource.clip = footsteps;

        // Enable footsteps sound if player is walking.
        footstepsSource.enabled = firstPersonController.isWalking;

        // If player is sprinting, make the footsteps sound faster.
        if (firstPersonController.isSprinting)
            footstepsSource.pitch = 1.5f;
        // Otherwise, play footsteps sound at normal speed.
        else
            footstepsSource.pitch = 1.0f;
    }

    public void Damage(float damageToInflict)
    {
        currentLife = currentLife >= minLife ? currentLife - damageToInflict : minLife;
        StartCoroutine(PainSound());
    }

    private IEnumerator PainSound()
    {
        queuePainSound = true;
        voiceSource.clip = pain;
        voiceSource.Play();
        while (voiceSource.isPlaying) yield return null;
        queuePainSound = false;
    }
}
