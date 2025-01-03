// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using TMPro;
using UnityEngine;

/// <summary>
/// Manages in-game text UI elements, such as interaction prompts and tips.
/// </summary>
public class UIIngameText : MonoBehaviour
{
    [Header("In Game")]

    /// <summary>
    /// The GameObject containing the in-game text UI.
    /// </summary>
    public GameObject inGameTextUI;

    /// <summary>
    /// The TextMeshProUGUI component for displaying interaction prompts.
    /// </summary>
    public TextMeshProUGUI interactionText;

    /// <summary>
    /// The TextMeshProUGUI component for displaying tips.
    /// </summary>
    public TextMeshProUGUI tipText;

    // Static variable to hold the text to be displayed.
    private static string _text = string.Empty;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        inGameTextUI.SetActive(true);
    }

    /// <summary>
    /// Called once per frame. Updates the interaction text.
    /// </summary>
    private void Update()
    {
        interactionText.text = _text;
    }

    /// <summary>
    /// Sets custom text to be displayed in the interaction prompt.
    /// </summary>
    /// <param name="text">The text to be displayed.</param>
    public static void SetCustomText(string text)
        => UIIngameText._text = text;

    /// <summary>
    /// Clears the text displayed in the interaction prompt.
    /// </summary>
    public static void ClearText() => _text = string.Empty;

    /// <summary>
    /// Sets the interaction text to display a message with the specified key code and interaction.
    /// </summary>
    /// <param name="keyCode">The key code to be displayed.</param>
    /// <param name="interaction">The interaction to be displayed.</param>
    public static void SetInteractionText(KeyCode keyCode, string interaction)
        => _text = $@"Press ""{keyCode}"" to {interaction}";
}
