// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEngine;

/// <summary>
/// Handles the game screen functionality.
/// </summary>
public class SceneGame : MonoBehaviour
{
    public Color fogColor = new(10, 10, 10, 255);

    public Color ambientColor = new(10, 10, 10);

    private void Start()
    {
        RenderSettings.fogColor = fogColor;
        RenderSettings.ambientLight = ambientColor;
    }
}
