// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the interaction and animation of a door in the game.
/// </summary>
public class Door : MonoBehaviour
{
    [Space(UnityInspector.SpaceDefault)]

    /// <summary>
    /// Indicates whether the door is locked.
    /// </summary>
    public bool Locked = true;

    [Header(UnityInspector.Interaction)]

    /// <summary>
    /// The key used to interact with the door.
    /// </summary>
    public KeyCode interactKey = KeyCode.E;

    /// <summary>
    /// The transform of the player.
    /// </summary>
    public Transform playerTransform;

    /// <summary>
    /// The text displayed for interaction prompts.
    /// </summary>
    public TextMeshProUGUI interactionText;

    [Header(UnityInspector.Sound)]

    /// <summary>
    /// The audio source for playing door sounds.
    /// </summary>
    public AudioSource doorAudioSource;

    /// <summary>
    /// The audio clip played when the door is unlocked.
    /// </summary>
    public AudioClip doorUnlockedClip;

    /// <summary>
    /// The audio clip played when the door is locked.
    /// </summary>
    public AudioClip doorLockedClip;

    [Header(UnityInspector.Animation)]

    /// <summary>
    /// The animator component for the door animations.
    /// </summary>
    private Animator animator;

    [Header(UnityInspector.Debug)]

    /// <summary>
    /// Indicates whether the door is open.
    /// </summary>
    [ReadOnly][SerializeField] private bool _isOpen = false;

    /// <summary>
    /// Indicates whether the animation is not running.
    /// </summary>
    [ReadOnly][SerializeField] private bool _isAnimationNotRunning;

    /// <summary>
    /// Indicates whether an attempt to open the door is in progress.
    /// </summary>
    [ReadOnly][SerializeField] private bool _tryingToOpen = false;

    /// <summary>
    /// Tracks the number of attempts to open the door.
    /// </summary>
    [ReadOnly][SerializeField] private int _tries = 0;

    /// <summary>
    /// Initializes the door's animator component.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Checks if the player is within the trigger area and attempts to open the door on key press.
    /// </summary>
    /// <param name="other">The collider of the object that is in the trigger area.</param>
    private void OnTriggerStay(Collider other)
    {
        // While player is nearby the door and presses the interact key, tries to open the door.
        if (Input.GetKeyDown(interactKey)) TryOpenDoor();
    }

    /// <summary>
    /// Updates the interaction text when the player enters the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger area.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Updating door interactive text.
        UpdateDoorInteractiveText();
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
    /// Updates the state of the animation status each frame.
    /// </summary>
    private void Update()
    {
        _isAnimationNotRunning = IsAnimationNotRunning("DoorClose", "DoorOpen");
    }

    /// <summary>
    /// Attempts to open or close the door based on its current state.
    /// </summary>
    private void TryOpenDoor()
    {
        // Trigger open or close animation if door is unlocked.
        if (!Locked)
        {
            if (IsAnimationNotRunning("DoorClose", "DoorOpen") && !_tryingToOpen)
            {
                // Add one to the open door try counter.
                _tries++;

                // Signaling that it is being tried to open the door at the moment.
                _tryingToOpen = true;

                // Playing door open/close animation.
                animator.Play(_isOpen ? "DoorClose" : "DoorOpen");

                // Playing unlocked door clip sound.
                doorAudioSource.clip = doorUnlockedClip;
                doorAudioSource.Play();

                // Inverting door state.
                _isOpen = !_isOpen;

                // Updating door interactive text according to new state.
                UpdateDoorInteractiveText();

                // When the animation ends, will signal that we are not trying to open the door at the moment.
                StartCoroutine(EndTransitionAfterAnimation());
            }
        }
        else
        {
            // Triggering locked door clip sound.
            doorAudioSource.clip = doorLockedClip;
            doorAudioSource.Play();

            // Showing the door is locked.
            UIIngameText.SetCustomText("Door Locked!");
        }
    }

    /// <summary>
    /// Updates the interaction text based on the door's state.
    /// </summary>
    private void UpdateDoorInteractiveText() => UIIngameText.SetCustomText(_isOpen ? "Close" : "Open");

    /// <summary>
    /// Coroutine that ends the transition after the door animation finishes.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator EndTransitionAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        _tryingToOpen = false;
    }

    /// <summary>
    /// Checks if the specified animations are not running.
    /// </summary>
    /// <param name="animations">The animations to check.</param>
    /// <returns>True if none of the specified animations are running, otherwise false.</returns>
    private bool IsAnimationNotRunning(params string[] animations)
    {
        if (animator == null) return false;

        foreach (var animation in animations)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(animation) && stateInfo.normalizedTime < 1)
                return false;
        }

        return true;
    }
}
