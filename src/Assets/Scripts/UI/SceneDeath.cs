// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the death sequence, including displaying images and playing death sounds before transitioning to the game over screen.
/// </summary>
public class Death : MonoBehaviour
{
    [Header("Images")]

    /// <summary>
    /// The first image displayed upon death.
    /// </summary>
    public Image deathImage1;

    /// <summary>
    /// The second image displayed upon death.
    /// </summary>
    public Image deathImage2;

    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing death sounds.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The first death sound.
    /// </summary>
    public AudioClip deathSound1;

    /// <summary>
    /// The second death sound.
    /// </summary>
    public AudioClip deathSound2;

    /// <summary>
    /// The sound of the monster bite.
    /// </summary>
    public AudioClip monsterBite;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        // Disables the death images at the start.
        deathImage1.gameObject.SetActive(false);
        deathImage2.gameObject.SetActive(false);

        // Starts playing the death sounds and displaying the images.
        StartCoroutine(PlayDeathSounds());
    }

    /// <summary>
    /// Plays the death sounds and displays the death images before transitioning to the game over screen.
    /// </summary>
    private IEnumerator PlayDeathSounds()
    {
        // Plays the first death sound and displays the first death image.
        audioSource.clip = deathSound1;
        audioSource.Play();
        deathImage1.gameObject.SetActive(true);
        while (audioSource.isPlaying) yield return null;

        // Plays the second death sound and displays the second death image.
        audioSource.clip = deathSound2;
        audioSource.Play();
        deathImage2.gameObject.SetActive(true);
        while (audioSource.isPlaying) yield return null;

        // Plays the monster bite sound.
        audioSource.clip = monsterBite;
        audioSource.Play();
        while (audioSource.isPlaying) yield return null;

        // Loads the game over scene.
        SceneManager.LoadScene($"{Scenes.GameOver}");
    }
}
