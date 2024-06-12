// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the start screen functionality, including fading out and transitioning to the game scene.
/// </summary>
public class StartScreen : MonoBehaviour
{
    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing start menu sounds.
    /// </summary>
    public AudioSource startMenuSound;

    [Header("Fade")]

    /// <summary>
    /// The image used for the fade-out effect.
    /// </summary>
    public Image fadeOverlay;

    /// <summary>
    /// Duration of the fade-out effect in seconds.
    /// </summary>
    public float fadeOutTime = 1.5f;

    /// <summary>
    /// Duration of the waiting time after fade out effect.
    /// </summary>
    public float afterFadeWait = 2;

    [Header(UnityInspector.Debug)]

    /// <summary>
    /// Indicates whether the game is starting.
    /// </summary>
    [SerializeField] private bool _starting = false;

    // Holder of the image color to use in fade out.
    private Color _fadeOutImageColor;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        Color transparentColor;
        // Storing the image color to use in fade out.
        transparentColor = _fadeOutImageColor = fadeOverlay.color;

        // Making the image transparent.
        transparentColor.a = 0;
        fadeOverlay.color = transparentColor;

        // Making the image inactive and placing it in the scene.
        fadeOverlay.gameObject.SetActive(_starting);

    }

    /// <summary>
    /// Called once per frame. Checks for any key input to start the game.
    /// </summary>
    private void Update()
    {
        if (Input.anyKey)
        {
            if (!_starting)
            {
                // Flagging that we are starting the game.
                _starting = true;

                // Starting the routine to start the game.
                StartCoroutine(StartTheGame());
            }
        }
    }

    /// <summary>
    /// Initiates the start sequence, including fading out and loading the game scene.
    /// </summary>
    private IEnumerator StartTheGame()
    {
        // Playing button sound effect.
        startMenuSound.Play();

        // Fade out animation.
        fadeOverlay.gameObject.SetActive(_starting);
        yield return StartCoroutine(FadeHelper.OverlayFadeIn(fadeOverlay, fadeOutTime));

        // Waiting time.
        yield return new WaitForSeconds(afterFadeWait);

        // Loading the scene.
        LoadingHelper.LoadScene(Scenes.Game);

        // Resetting the starting flag.
        _starting = false;
    }
}
