﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overmind : MonoBehaviour {

    static public Overmind instance { get; private set; }

    public GameObject loseText;
    public GameObject winText;

    Vector3 playerStartingPos;
    Quaternion playerStartingRotation;

    bool gameEnded;

    void Awake() {
        instance = this;
    }

    void Start() {
        playerStartingPos = Player.instance.transform.position;
        playerStartingRotation = Player.instance.transform.rotation;
    }

    public void LoseGame() {
        loseText.SetActive(true);
        EndGame();
    }

    public void WinGame() {
        winText.SetActive(true);
        EndGame();
    }

    void EndGame() {
        Time.timeScale = 0;
        gameEnded = true;
    }

    void LateUpdate() {
        if (gameEnded) {
            if (Input.anyKeyDown) {
                gameEnded = false;
                Time.timeScale = 1;
                Player.instance.transform.position = playerStartingPos;
                Player.instance.transform.rotation = playerStartingRotation;
                loseText.SetActive(false);
                winText.SetActive(false);
            }
        }
    }
}
