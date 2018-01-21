using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform startingPos;

    public GameObject footprintPrefab;
    public Transform footprintContainer;

    public float footstepTime = 0.5f;

    public float[] speeds;
    public AudioClip[] regularSounds;

    NavMeshAgent agent;
    AudioSource source;
    bool move;
    bool flipFootprint;
    float elapsedTime;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        CheckNoiseLevel();
        Move();
        UpdateSound();
    }

    void CheckNoiseLevel() {
        int noiseLevel = Player.instance.noiseLevel;
        move = noiseLevel > 0;
    }

    void Move() {
        if (move) {
            agent.SetDestination(Player.instance.transform.position);
            agent.speed = speeds[Player.instance.noiseLevel];
        }
        else {
            agent.SetDestination(startingPos.position);
            agent.speed = speeds[1];
        }
        UpdateFootsteps();
    }

    void UpdateFootsteps() {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime > footstepTime) {
            InstantiateFootprint();
            elapsedTime = 0;
        }
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

    void UpdateSound() {
        if (!source.isPlaying) {
            source.clip = regularSounds[Random.Range(0, regularSounds.Length)];
            source.Play();
        }
    }
}
