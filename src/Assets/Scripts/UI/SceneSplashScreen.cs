// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls the splash screen fade effect and transitions to a new scene.
/// </summary>
public class SplashScreenFade : MonoBehaviour
{
    [Header("Fade")]

    /// <summary>
    /// The scene to load after the splash screen.
    /// </summary>
    public Scenes SceneToLoad;

    /// <summary>
    /// The image used for the fade effect.
    /// </summary>
    public Image fadeImage;

    /// <summary>
    /// Duration of the fade-in effect in seconds.
    /// </summary>
    public float fadeInTime = 5.0f;

    /// <summary>
    /// Duration of the fade-out effect in seconds.
    /// </summary>
    public float fadeOutTime = 5.0f;

    /// <summary>
    /// Duration to wait before starting the fade-out effect.
    /// </summary>
    public float waitTimeDuration = 5.0f;

    /// <summary>
    /// Indicates whether to use a loading screen transition.
    /// </summary>
    public bool loadingTransition = true;

    [Header(UnityInspector.Debug)]

    /// <summary>
    /// Indicates whether the splash screen sequence is playing.
    /// </summary>
    [SerializeField] private bool _playing = false;

    /// <summary>
    /// Indicates whether the fade-in effect is playing.
    /// </summary>
    [SerializeField] private bool _playingFadeIn = false;

    /// <summary>
    /// Indicates whether the wait time is active.
    /// </summary>
    [SerializeField] private bool _playingWaitTime = false;

    /// <summary>
    /// Indicates whether the fade-out effect is playing.
    /// </summary>
    [SerializeField] private bool _playingFadeOut = false;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartTheGame());
    }

    /// <summary>
    /// Controls the sequence of fade-in, wait time, and fade-out effects.
    /// </summary>
    private IEnumerator StartTheGame()
    {
        _playing = true;

        // Set fade out object as active.
        fadeImage.gameObject.SetActive(_playing);

        // Fade In.
        yield return StartCoroutine(FadeIn());

        // Waiting Time.
        if (!_playingFadeIn)
            yield return StartCoroutine(WaitTime());

        // Fade Out.
        if (!_playingWaitTime)
            yield return StartCoroutine(FadeOut());

        // Loading the next scene.
        if (loadingTransition) LoadingHelper.LoadScene(SceneToLoad);
        else SceneManager.LoadScene($"{SceneToLoad}");

        // Resetting the starting flag.
        _playing = false;
    }

    /// <summary>
    /// Waits for a specified duration before proceeding.
    /// </summary>
    private IEnumerator WaitTime()
    {
        _playingWaitTime = true;
        yield return new WaitForSeconds(waitTimeDuration);
        _playingWaitTime = false;
    }

    /// <summary>
    /// Handles the fade-in effect, gradually making the image transparent.
    /// </summary>
    private IEnumerator FadeIn()
    {
        _playingFadeIn = true;

        yield return FadeHelper.OverlayFadeOut(fadeImage, fadeInTime);

        _playingFadeIn = false;
    }

    /// <summary>
    /// Handles the fade-out effect, gradually making the image opaque.
    /// </summary>
    private IEnumerator FadeOut()
    {
        _playingFadeOut = true;

        yield return FadeHelper.OverlayFadeIn(fadeImage, fadeOutTime);

        _playingFadeOut = false;
    }
}
