// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the behavior when the player wins the game.
/// </summary>
public class Win : MonoBehaviour
{
    [Header("SFX")]
    /// <summary>
    /// The audio source component to play sound effects.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The audio clip for the car door sound.
    /// </summary>
    public AudioClip carDoor;

    /// <summary>
    /// The audio clip for the car running sound.
    /// </summary>
    public AudioClip carRunning;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Starts playing the sound effects sequentially.
    /// </summary>
    private void Start()
    {
        StartCoroutine(PlaySounds());
    }

    /// <summary>
    /// Called once per frame.
    /// Checks for any key press to load the menu scene.
    /// </summary>
    private void Update()
    {
        if (Input.anyKey)
            LoadingHelper.LoadScene(Scenes.Menu);
    }

    /// <summary>
    /// Coroutine to play the sound effects sequentially.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator PlaySounds()
    {
        // Play the car door sound effect.
        audioSource.clip = carDoor;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;

        // Play the car running sound effect.
        audioSource.clip = carRunning;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;
    }
}
