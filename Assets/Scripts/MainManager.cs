using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int BestScore = 0;
    private new string name;
    private new string BestName;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        SetBest();
        BestText.text = $"Best Score: {BestScore} Name: {BestName}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void SetBest()
    {
        string path = Application.persistentDataPath + "/name.txt";
        if (File.Exists(path))
        {
            name = File.ReadAllText(path);
        }
        path = Application.persistentDataPath + "/bname.txt";
        if (File.Exists(path))
        {
            BestName = File.ReadAllText(path);
        }
        else
        {
            BestName = name;
        }
        path = Application.persistentDataPath + "/score.txt";
        if (File.Exists(path))
        {
            string temp;
            temp = File.ReadAllText(path);
            System.Int32.TryParse(temp, out BestScore); 
        }
    }

    void saveHighScore()
    {
        File.WriteAllText(Application.persistentDataPath + "/score.txt", BestScore.ToString());
        File.WriteAllText(Application.persistentDataPath + "/bname.txt", BestName);
    }
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > BestScore)
        {
            BestScore = m_Points;
            BestName = name;
            BestText.text = $"Best Score: {BestScore} Name: {BestName}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        saveHighScore(); 
    }
}
