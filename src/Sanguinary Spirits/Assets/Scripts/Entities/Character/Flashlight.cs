// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEngine;

/// <summary>
/// Controls the behavior of a flashlight in the game, allowing it to be toggled on and off.
/// </summary>
public class Flashlight : MonoBehaviour
{
    /// <summary>
    /// The key used to toggle the flashlight.
    /// </summary>
    public KeyCode flashlightKey = KeyCode.F;

    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The light component representing the flashlight.
    /// </summary>
    public Light lightSource;
    
    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing flashlight sounds.
    /// </summary>
    public AudioSource flashlightAudio;

    /// <summary>
    /// The audio clip played when the flashlight is turned on.
    /// </summary>
    public AudioClip onSound;

    /// <summary>
    /// The audio clip played when the flashlight is turned off.
    /// </summary>
    public AudioClip offSound;

    /// <summary>
    /// Called once per frame. Checks for input to toggle the flashlight.
    /// </summary>
    void Update()
    {
        // Checking if player wants to toggle flashlight and toggles it.
        if (Input.GetKeyDown(flashlightKey))
        {
            // Toggling the flashlight.
            lightSource.enabled = !lightSource.enabled;

            // Depending on the toggle state, choose the appropriate flashlight sound.
            AudioClip flashlightClip = lightSource.enabled ? onSound : offSound;
            flashlightAudio.clip = flashlightClip;

            // Playing the corresponding flashlight sound.
            flashlightAudio.Play();
        }
    }
}
