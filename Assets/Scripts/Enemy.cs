using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform footprintContainer;
    public AudioClip[] footstepSounds;
    public AudioSource footstepSource;
    public float footstepTime = 0.5f;

    float elapsedTime;
    bool move;
    NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate() {
        CheckNoiseLevel();
        if (move)
            MoveTowardsPlayer();
    }

    void CheckNoiseLevel() {
        int noiseLevel = Player.instance.noiseLevel;
        if (noiseLevel > 0)
            move = true;
    }

    void MoveTowardsPlayer() {
        agent.SetDestination(Player.instance.transform.position);
        UpdateFootsteps();
    }

    void UpdateFootsteps() {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime > footstepTime) {
            PlayFootstepSound();
            elapsedTime = 0;
        }
    }

    void PlayFootstepSound() {
        // TODO
    }
}
