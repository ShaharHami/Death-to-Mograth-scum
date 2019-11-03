using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<Text>();
        livesText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLife(int updateBy)
    {
        lives += updateBy;
        livesText.text = lives.ToString();
    }
}
