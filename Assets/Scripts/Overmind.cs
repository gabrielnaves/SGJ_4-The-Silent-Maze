using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overmind : MonoBehaviour {

    static public Overmind instance { get; private set; }

    public GameObject loseText;
    public GameObject winText;
    public GameObject pausedText;
    public DrumEnemy drumEnemy;

    Vector3 playerStartingPos;
    Quaternion playerStartingRotation;
    float elapsedTime;

    bool gameEnded;

    void Awake() {
        instance = this;
        gamePaused = false;
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
        if (Input.GetKeyDown(KeyCode.P) && !gameEnded) {
            TogglePause();
        }
        else if (gameEnded) {
            elapsedTime += Time.unscaledDeltaTime;
            if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && elapsedTime > 1.5f) {
                gameEnded = false;
                Time.timeScale = 1;
                elapsedTime = 0f;
                Player.instance.transform.position = playerStartingPos;
                Player.instance.transform.rotation = playerStartingRotation;
                loseText.SetActive(false);
                winText.SetActive(false);
                drumEnemy.Start();
            }
        }
    }

    public bool gamePaused { get; private set; }
    void TogglePause() {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0f : 1f;
        pausedText.SetActive(gamePaused);
    }
}
