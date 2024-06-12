// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the display of tips using TextMeshProUGUI.
/// </summary>
public class UITipText : MonoBehaviour
{
    [Header("Hint")]

    public TextMeshProUGUI tipText;

    public HintType hintType;

    /// <summary>
    /// The duration for which the tip text is displayed.
    /// </summary>
    public float displayDuration = 3.0f;

    [Header("Show Once")]

    public bool showOnce = true;

    [ReadOnly][SerializeField] private bool _alreadyShown = false;

    /// <summary>
    /// Variable to hold the text to be displayed.
    /// </summary>
    private string _text = string.Empty;

    private readonly int _fadeTime = 1;

    /// <summary>
    /// Displays a tip text with a fade in and out effect.
    /// </summary>
    private IEnumerator FadeInAndOutText(string str = "")
    {
        // Set the tip text.
        _text = str;
        tipText.text = _text;

        // Fade in effect.
        yield return StartCoroutine(FadeHelper.OverlayFadeIn(tipText, _fadeTime));

        // Display the text for a specified duration.
        yield return new WaitForSeconds(displayDuration);

        // Fade out effect.
        yield return StartCoroutine(FadeHelper.OverlayFadeOut(tipText, _fadeTime));

        _alreadyShown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!showOnce || (showOnce && !_alreadyShown))
            StartCoroutine(FadeInAndOutText(GetHint()));
    }

    private string GetHint()
    {
        return hintType switch
        {
            HintType.Flashlight => $@"Press ""{KeyCode.F}"" to use the Flashlight.",
            HintType.Sprint => $@"Press ""Shift"" to use the Sprint.",
            HintType.Crouch => $@"Press ""Ctrl"" to use the Crouch.",
            HintType.Interact => $@"Press ""{KeyCode.E}"" to Interact.",
            HintType.FindExit => "Find the Exit!",
            _ => throw new System.NotImplementedException(),
        };
    }

    public enum HintType
    {
        Flashlight,
        Sprint,
        Crouch,
        Interact,
        FindExit
    }
}
