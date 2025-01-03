// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the loading screen functionality by waiting for a specified duration before loading the next scene.
/// </summary>
public class Loading : MonoBehaviour
{
    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        StartCoroutine(WaitLoading());
    }

    /// <summary>
    /// Waits for a specified duration and then loads the next scene.
    /// </summary>
    private IEnumerator WaitLoading()
    {
        // Waits for 2.5 seconds.
        yield return new WaitForSeconds(2.5f);

        // Loads the scene specified in the LoadingHelper.
        SceneManager.LoadScene($"{LoadingHelper.SceneToLoad}");
    }
}
