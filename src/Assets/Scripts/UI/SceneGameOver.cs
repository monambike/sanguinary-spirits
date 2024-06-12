// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the game over screen functionality, including retrying and returning to the main menu.
/// </summary>
public class GameOver : MonoBehaviour
{
    [Header("Buttons")]

    /// <summary>
    /// The TextMeshProUGUI component for the retry button.
    /// </summary>
    public TextMeshProUGUI retry;

    /// <summary>
    /// The TextMeshProUGUI component for the menu button.
    /// </summary>
    public TextMeshProUGUI menu;

    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing click sounds.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// The click sound to play when buttons are clicked.
    /// </summary>
    public AudioClip clickSound;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        // Sets up the click sound.
        audioSource.clip = clickSound;

        // Unlocks the cursor and makes it visible.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Initiates the retry sequence, including playing a sound and loading the game scene.
    /// </summary>
    public void Retry()
    {
        StartCoroutine(PlaySound());
        LoadingHelper.LoadScene(Scenes.Game);
    }

    /// <summary>
    /// Initiates the return to menu sequence, including playing a sound and loading the main menu scene.
    /// </summary>
    public void Menu()
    {
        StartCoroutine(PlaySound());
        LoadingHelper.LoadScene(Scenes.Menu);
    }

    /// <summary>
    /// Plays a sound with a short delay.
    /// </summary>
    private IEnumerator PlaySound()
    {
        audioSource.Play();

        // Waits for 1 second before continuing.
        yield return new WaitForSeconds(1);
    }
}
