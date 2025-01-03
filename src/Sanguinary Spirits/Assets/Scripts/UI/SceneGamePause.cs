// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEngine;

/// <summary>
/// Manages the pause menu functionality, including pausing and resuming the game.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]

    /// <summary>
    /// The UI element for the pause menu.
    /// </summary>
    public GameObject pauseMenuUI;

    /// <summary>
    /// The player controller script.
    /// </summary>
    public FirstPersonController player;

    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing pause menu sounds.
    /// </summary>
    public AudioSource pauseAudioSource;

    /// <summary>
    /// The audio clip for the pause menu music.
    /// </summary>
    public AudioClip pauseMusic;

    [Header(UnityInspector.Debug)]

    /// <summary>
    /// Indicates whether the game is currently paused.
    /// </summary>
    [ReadOnly][SerializeField] private bool _paused = false;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Setting music to pause audio source.
        pauseAudioSource.clip = pauseMusic;

        // Shows Pause Menu UI only if the game is paused.
        pauseMenuUI.SetActive(_paused);
    }

    /// <summary>
    /// Called once per frame. Checks for input to pause or resume the game.
    /// </summary>
    private void Update()
    {
        // Playing music for pause menu.
        if (_paused)
            pauseAudioSource.UnPause();
        else
            pauseAudioSource.Pause();

        // Check for pause/resume input.
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            PauseResume();
    }

    /// <summary>
    /// Toggles the pause state of the game.
    /// </summary>
    private void PauseResume()
    {
        // Toggle game pause state.
        _paused = !_paused;

        // Show Pause Menu UI only if the game is paused.
        pauseMenuUI.SetActive(_paused);

        // Can only move camera when game is unpaused.
        player.cameraCanMove = !_paused;

        // Time stops when paused.
        Time.timeScale = _paused ? 0f : 1f;

        // Audio pauses when game is paused.
        var audios = FindObjectsOfType<AudioSource>();
        foreach (var audio in audios)
        {
            if (_paused)
                audio.Pause();
            else
                audio.UnPause();
        }
    }
}
