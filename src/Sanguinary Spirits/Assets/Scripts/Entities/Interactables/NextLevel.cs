// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [Header(UnityInspector.Interaction)]

    /// <summary>
    /// The key used to interact with the door.
    /// </summary>
    public KeyCode interactKey = KeyCode.E;

    private void OnTriggerStay(Collider other)
    {
        // While player is nearby the door and presses the interact key, tries to open the door.
        if (Input.GetKeyDown(interactKey)) LoadingHelper.LoadScene(Scenes.Win);
    }

    private void OnTriggerEnter(Collider other)
    {
        UIIngameText.SetCustomText("Exit");
    }

    private void OnTriggerExit(Collider other)
    {
        // Clearing the interaction text outside the collision box.
        UIIngameText.ClearText();
    }
}
