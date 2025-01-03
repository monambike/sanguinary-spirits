// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the behavior the aracnide enemy in the game, 
/// </summary>
public class Aracnide : MonoBehaviour
{
    /// <summary>
    /// Reference to the player GameObject.
    /// </summary>
    public Character player;

    [Header("Audio")]
    /// <summary>
    /// AudioSource for the bite sound.
    /// </summary>
    public AudioSource biteAudioSource;
    /// <summary>
    /// AudioClip for the bite sound.
    /// </summary>
    public AudioClip biteAudioClip;

    /// <summary>
    /// Distance threshold for triggering actions.
    /// </summary>
    public float proximityThreshold = 3.0f;

    [Header("Enemy Behavior")]

    /// <summary>
    /// Base damage given to the player on attack.
    /// </summary>
    public float baseDamage = 20f;

    /// <summary>
    /// Determines if the Aracnide should attack the player.
    /// </summary>
    public bool canAttackPlayer = true;
    /// <summary>
    /// Determines if the Aracnide should chase the player.
    /// </summary>
    public bool canChasePlayer = true;

    /// <summary>
    /// Aracnide hitbox chase.
    /// </summary>
    public AracnideHitboxChase aracnideHitboxChase;

    [Header(UnityInspector.Debug)]

    /// <summary>
    /// Flag to check if the bite sound is currently playing.
    /// </summary>
    [SerializeField] private bool _isPlayingSound;

    /// <summary>
    /// Determines if the Aracnide is chasing the player.
    /// </summary>
    [SerializeField] private bool _isChasingPlayer;

    /// <summary>
    /// Reference to the NavMeshAgent component.
    /// </summary>
    private NavMeshAgent _aracnideNavMeshAgent;

    private void Start()
    {
        // Get the NavMeshAgent component.
        _aracnideNavMeshAgent = GetComponent<NavMeshAgent>();

        // Set the bite sound clip.
        biteAudioSource.clip = biteAudioClip;
    }

    private void FixedUpdate()
    {
        // Check if chasing the player is enabled.
        if (canChasePlayer && aracnideHitboxChase.onChaseRadius)
            // Set Aracnide's destination to the player's position.
            _aracnideNavMeshAgent.SetDestination(player.transform.position);
    }

    /// <summary>
    /// Coroutine to play the bite sound.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayMonsterBiteSound()
    {
        _isPlayingSound = true;
        biteAudioSource.Play();
        // Wait until the bite sound finishes playing.
        yield return new WaitForSeconds(biteAudioSource.clip.length + 1.0f);
        _isPlayingSound = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if attacking the player is enabled.
        if (canAttackPlayer)
        {
            StartCoroutine(PlayMonsterBiteSound());

            player.Damage(baseDamage);
        }
    }
}
