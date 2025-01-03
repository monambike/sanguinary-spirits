// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class FadeHelper
{
    public static IEnumerator OverlayFadeIn(Graphic graphicToFade, float duration) => Fade(FadeType.FadeIn, duration, graphicToFade);

    public static IEnumerator OverlayFadeOut(Graphic graphicToFade, float duration) => Fade(FadeType.FadeOut, duration, graphicToFade);

    /// <summary>
    /// Performs the fade in or fade out effect.
    /// </summary>
    private static IEnumerator Fade(FadeType fadeType, float fadeDuration, Graphic graphicToFade)
    {
        // Determine if it's fade in or fade out.
        var targetedTransparency = fadeType == FadeType.FadeIn ? 1.0f : 0.0f;

        // Initialize variables for the fade loop.
        float elapsedTime = 0;
        Color color = graphicToFade.color;
        float startTransparency = color.a;

        // Fade loop.
        while (elapsedTime < fadeDuration)
        {
            // Add delta time to elapsed time.
            elapsedTime += Time.deltaTime;

            // Calculate transparency based on the fade type.
            float currentTransparency = Mathf.Lerp(startTransparency, targetedTransparency, elapsedTime / fadeDuration);
            color.a = currentTransparency;

            // Apply color and wait for next frame.
            graphicToFade.color = color;
            yield return null;
        }

        // Ensure final transparency is set correctly.
        color.a = targetedTransparency;
        graphicToFade.color = color;
    }

    /// <summary>
    /// Enumeration that defines the types of fade effects.
    /// </summary>
    public enum FadeType
    {
        /// <summary>
        /// Indicates a fade-in effect.
        /// </summary>
        FadeIn,

        /// <summary>
        /// Indicates a fade-out effect.
        /// </summary>
        FadeOut
    }
}