using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int AsteroidCount = 1;
    // public int AsteroidsLeft = 0;
    public GameObject asteroid;

    // public List<GameObject> asteroids;

    public int lives { get; set; }
    public int score { get; set; }

    [SerializeField] private GameObject canvasList;
    public Text scoreText;
    [SerializeField] private List<GameObject> livesList = new List<GameObject>();
    [SerializeField] private GameObject life;
    private int index;
    private float offset = 70;
    public PlayerControls player;
    private Vector3 bottomLeft;
    private Vector3 topRight;
    private Vector3 pos;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioSource explode;
    private string levelText = "LEVEL ";
    private string completeText = " COMPLETE";
    private int levelNum;

    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
        SetLives(5);
        levelNum = 0;

        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 0.0f));

        for (index = 0; index < lives; index++)
        {

            GameObject go = Instantiate(life, life.transform.position + new Vector3(index * offset, canvas.renderingDisplaySize.y, 0), life.transform.rotation, canvasList.transform) as GameObject;
            livesList.Add(go);
        }
        AsteroidSpawnInitial();


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
        Invoke("AsteroidSpawnInitial", 4f);

        GameObject.FindWithTag("DoNotDestroy").GetComponent<SaveScript>().levels = this.levelNum;
    }

    public void CompleteBannerOn()
    {
        levelNum += 1;
        levelComplete.GetComponent<Text>().text = levelText + levelNum + completeText;
        levelComplete.SetActive(true);
        
    }

    public void CompleteBannerOff()
    {
        levelComplete.SetActive(false);
        levelNum += 1;
    }

    public void AsteroidSpawnInitial()
    {
        float size = 0;

        for (int i = 0; i < AsteroidCount; i++)
        {
            bool asteroidSpawn = false;
            while (!asteroidSpawn)
            {
                pos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0);
                if ((pos - transform.position).magnitude < 5)
                {
                    continue;
                }
                else
                {
                    size = Random.Range(0.35f, 1.5f);
                    AsteroidSpawn(pos, size);

                    asteroidSpawn = true;
                }
            }
            // AsteroidsLeft = AsteroidCount;
        }
    }

    public void AsteroidSpawn(Vector2 position, float size)

    {
        float impulse = 0;

        GameObject newAsteroid = Instantiate(asteroid, position, Quaternion.identity);
        newAsteroid.transform.localScale = new Vector3(1f,Random.Range(0.25f,1f),1f) * size;
        newAsteroid.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        newAsteroid.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * Random.Range(10f, 50f));

        impulse = (Random.Range(0f, 90f) * Mathf.Deg2Rad) * newAsteroid.GetComponent<Rigidbody2D>().inertia;
        newAsteroid.GetComponent<Rigidbody2D>().AddTorque(impulse, ForceMode2D.Impulse);

        newAsteroid.GetComponent<AsteroidControl>().size = size;
        // asteroids.Add(newAsteroid);

        // return newAsteroid;
    }

}
