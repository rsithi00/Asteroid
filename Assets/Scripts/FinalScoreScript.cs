using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour
{
    [SerializeField] private Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.score.text = GameObject.FindWithTag("DoNotDestroy").GetComponent<SaveScript>().score.ToString();
    }
}
