using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator instanceEnemy;
    public List<GameObject> Enemy = new List<GameObject>();
    private float time_to_create = 4f;
    private float actual_time = 0f;
    private float limitSuperior;
    private float limitInferior;
    public List<GameObject> actual_Enemy = new List<GameObject>();
    [SerializeField] TMP_Text livess;

    void Awake()
    {
        if (instanceEnemy != null && instanceEnemy != this)
        {
            Destroy(this.gameObject);
        }
        instanceEnemy = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        actual_time += Time.deltaTime;
        if (time_to_create <= actual_time)
        {
            GameObject enemy = Instantiate(Enemy[Random.Range(0, Enemy.Count)],
            new Vector3(transform.position.x, Random.Range(limitInferior, limitSuperior), 0f), Quaternion.identity);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);
            actual_time = 0f;
            actual_Enemy.Add(enemy);
        }
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -(bounds.y * 0.9f);
        limitSuperior = (bounds.y * 0.9f);
    }
    /*
    public void ManageCandy(CandyController candy_script, PlayerMovement player_script = null)
    {
        if (player_script == null)
        {
            Destroy(candy_script.gameObject);
            return;
        }
        int point = player_script.player_points;
        int points_changer = candy_script.lifeChanges;
        point += points_changer;
        print(point);
        pointss.text = "Points: " + point;
        player_script.player_points = point;
        Destroy(candy_script.gameObject);
    }

    */

    public void vida(EnemyControler candy_script, PlayerMovement player_script = null)
    {
        if (player_script == null)
        {
            Destroy(candy_script.gameObject);
            return;
        }
        int lives = player_script.player_lives;
        int live_changer = candy_script.lifeChanges;
        lives -= live_changer;
        print(lives);
        livess.text = "Lifes: " + lives;
        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        player_script.player_lives = lives;
        Destroy(candy_script.gameObject);
    }
}
