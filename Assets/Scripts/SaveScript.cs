using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public int lives { get; private set; }
    public int score { get; private set; }

    public Text livesText;
    public Text scoreText;

    [SerializeField] private GameObject canvasList;
    [SerializeField] private GameObject[] livesList;
    [SerializeField] private GameObject life;
    private int index;

    private float offset = 50;
    // Start is called before the first frame update
    void Start()

    {
        SetScore(0);
        SetLives(5);
        livesList = new GameObject[lives];
        for (index = 0; index < lives; index++)
        {

            GameObject go = Instantiate(life, life.transform.position + new Vector3(index * offset, 1080, 0), life.transform.rotation, canvasList.transform) as GameObject;
            livesList[index] = go;
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Death();
        }
    }

    void Death()
    {
        if(index>0)
        {
            Destroy(livesList[index-1].gameObject);
            index--;
        }
            

    }


    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

}
