// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public bool canSleep = false;

    [Header(UnityInspector.Interaction)]

    [Header("Fade")]

    /// <summary>
    /// The image used for the fade-out effect.
    /// </summary>
    public Image fadeOutImage;

    [Header(UnityInspector.Debug)]

    private bool _isSleeping = false;

    /// <summary>
    /// The key used to interact with the bed.
    /// </summary>
    public KeyCode interactKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TryToSleep()
    {
        if (canSleep && !_isSleeping)
        {
            StartCoroutine(Sleep());
        }
        else
        {
            // Showing the door is locked.
            UIIngameText.SetCustomText(@"""I don't feel it's safe to sleep here.""");
        }
    }

    private IEnumerator Sleep()
    {
        _isSleeping = true;

        yield return new WaitForSeconds(2f);

        _isSleeping = false;
    }

    /// <summary>
    /// Updates the interaction text when the player enters the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger area.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Updating door interactive text.
        UpdateBedInteractiveText();
    }

    /// <summary>
    /// Clears the interaction text when the player exits the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that exited the trigger area.</param>
    private void OnTriggerExit(Collider other)
    {
        // Clearing the interaction text outside the collision box.
        UIIngameText.ClearText();
    }

    /// <summary>
    /// Updates the interaction text based on the door's state.
    /// </summary>
    private void UpdateBedInteractiveText() => UIIngameText.SetCustomText("Sleep");
}
