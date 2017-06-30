using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour {

    public GameObject astroid;
    public Vector3 spawnValues;
    public int astroidCount;
    public float startWait;
    public float spawnWait;

    public GUIText scoreText;
    public int score = 0;

    public GUIText endText;
    private bool gameEnded;

	// Use this for initialization
	void Start () {
        gameEnded = false;
        endText.gameObject.SetActive(false);
       StartCoroutine ( SpawnWaves());
	}
	
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        for(int i = 0; i < astroidCount; i++) { 

        Vector3 spawnAt = new Vector3( Random.Range(spawnValues.x, -spawnValues.x),
                spawnValues.y,
                spawnValues.z);
            Instantiate(astroid, spawnAt, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        endText.gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene level = SceneManager.GetActiveScene();
                SceneManager.LoadScene(level.name);
            }
        }
	}

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score : " + score;
    }

}
