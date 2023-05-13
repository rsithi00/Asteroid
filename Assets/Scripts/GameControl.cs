using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int AsteroidCount = 1;
    public GameObject asteroid;

    public List<GameObject> asteroids;

    public int lives { get; set; }
    public int score { get; set; }

    [SerializeField] private GameObject canvasList;
    public Text scoreText;
    [SerializeField] private List<GameObject> livesList = new List<GameObject>();
    [SerializeField] private GameObject life;
    private int index;
    private float offset = 50;
    public PlayerControls player;
    private Vector3 bottomLeft;
    private Vector3 topRight;
    private Vector3 pos;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioSource explode;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
        SetLives(5);

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));

        for (index = 0; index < lives; index++)
        {

            GameObject go = Instantiate(life, life.transform.position + new Vector3(index * offset, canvas.renderingDisplaySize.y, 0), life.transform.rotation, canvasList.transform) as GameObject;
            livesList.Add(go);
        }

        AsteroidSpawn();


    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void Death(PlayerControls player)
    {
        GameObject.FindWithTag("DoNotDestroy").GetComponent<SaveScript>().score = this.score;
        explode.Play();
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            Destroy(livesList[livesList.Count - 1].gameObject);
            livesList.RemoveAt(livesList.Count - 1);
            SetLives(lives - 1);
            Respawn();
        }
    }

    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        Invincible();
        Invoke("ActivatePlayerCollider", 3f);
    }

    public void AddPoints(int score)
    {
        SetScore(this.score + score);
    }

    public void Invincible()
    {
        player.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void ActivatePlayerCollider()
    {
        player.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void LevelComplete()
    {
        player.gameObject.SetActive(false);
        CompleteBannerOn();
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().angularVelocity = 0;


        AsteroidCount += 2;
        Invoke("CompleteBannerOff", 3.9f);
        Invoke("Respawn", 4f);
        Invoke("AsteroidSpawn", 4f);

    }

    public void CompleteBannerOn()
    {
        levelComplete.SetActive(true);
    }

    public void CompleteBannerOff()
    {
        levelComplete.SetActive(false);
    }

    public void AsteroidSpawn()
    {

        for (int i = 0; i < AsteroidCount; i++)
        {
            bool asteroidSpawn = false;
            while (!asteroidSpawn)
            {
                pos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);
                if ((pos - transform.position).magnitude < 3)
                {
                    continue;
                }
                else
                {
                    GameObject newAsteroid = Instantiate(asteroid, pos, Quaternion.identity);
                    newAsteroid.transform.localScale = Vector3.one * Random.Range(0.35f, 1.25f);
                    asteroids.Add(newAsteroid);
                    asteroidSpawn = true;
                }
            }
        }
    }

}
