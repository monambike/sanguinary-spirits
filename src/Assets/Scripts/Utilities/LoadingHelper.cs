// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEngine.SceneManagement;

/// <summary>
/// Helper class for managing scene loading in Unity.
/// </summary>
public static class LoadingHelper
{
    /// <summary>
    /// The name of the scene to load after the loading screen.
    /// Default is set to the Menu scene.
    /// </summary>
    public static Scenes SceneToLoad = Scenes.Menu;

    /// <summary>
    /// Loads the loading screen scene.
    /// </summary>
    public static void LoadScene() => SceneManager.LoadScene($"{Scenes.Loading}");

    /// <summary>
    /// Sets the scene to be loaded after the loading screen and then loads the loading screen.
    /// </summary>
    /// <param name="scene">The name of the scene to load.</param>
    public static void LoadScene(Scenes scene)
    {
        // Save the scene to be loaded.
        SceneToLoad = scene;

        // Invoke the loading screen.
        LoadScene();
    }
}
