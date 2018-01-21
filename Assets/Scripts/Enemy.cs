using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public GameObject footprintPrefab;
    public Transform footprintContainer;

    public AudioClip[] footstepSounds;
    public AudioSource footstepSource;

    public float footstepTime = 0.5f;

    float elapsedTime;
    bool move;
    bool flipFootprint;
    NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate() {
        CheckNoiseLevel();
        if (move)
            MoveTowardsPlayer();
        else
            agent.SetDestination(transform.position);
    }

    void CheckNoiseLevel() {
        int noiseLevel = Player.instance.noiseLevel;
        move = noiseLevel > 0;
    }

    void MoveTowardsPlayer() {
        agent.SetDestination(Player.instance.transform.position);
        UpdateFootsteps();
    }

    void UpdateFootsteps() {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime > footstepTime) {
            PlayFootstepSound();
            InstantiateFootprint();
            elapsedTime = 0;
        }
    }

    void PlayFootstepSound() {
        // TODO
    }

    void InstantiateFootprint() {
        GameObject footprint = Instantiate(footprintPrefab);
        footprint.transform.position = new Vector3(transform.position.x, 0.51f, transform.position.z);
        footprint.transform.rotation = Quaternion.Euler(90f, 0f, -transform.rotation.eulerAngles.y);
        footprint.transform.parent = footprintContainer;
        if (flipFootprint)
            footprint.GetComponent<SpriteRenderer>().flipX = true;
        flipFootprint = !flipFootprint;
    }
}
