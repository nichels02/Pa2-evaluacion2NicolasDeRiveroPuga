using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    private Rigidbody2D myRB;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private float limitSuperior;
    [SerializeField] private float limitInferior;
    private float distance=0;
    [SerializeField] private TMP_Text texto;
    [SerializeField] private AudioClip clipEnemigo;
    [SerializeField] private AudioClip clipCaramelo;
    private AudioSource audio;
    [SerializeField] private Transform elTransform;

    public int player_lives = 4;
    public int player_points = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (up == KeyCode.None) up = KeyCode.UpArrow;
        if (down == KeyCode.None) down = KeyCode.DownArrow;
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    private void Update()
    {
        distance = distance + Time.deltaTime;

        if (distance >= 1)
        {
            player_points = player_points + (int)distance;
            distance = 0f;
            texto.text = ("Points: " + player_points);
        }
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audio = other.GetComponent<AudioSource>();
        Debug.Log(audio.clip.name);
        audio.PlayOneShot(audio.clip);
        if (other.tag == "Candy")
        {
            audio.PlayOneShot(clipCaramelo);
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
        }


        if (other.tag == "Enemy")
        {
            elTransform.position = new Vector3(-5, 0, 0);
            audio.PlayOneShot(clipEnemigo);
            EnemyGenerator.instanceEnemy.vida(other.gameObject.GetComponent<EnemyControler>(),this);
        }
    }


    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        if (inputMovement.y == 1 && transform.position.y < limitSuperior)
        {
            myRB.velocity = new Vector2(0f, speed);
        }
        else if (inputMovement.y == -1 && transform.position.y > limitInferior)
        {
            myRB.velocity = new Vector2(0f, -speed);
        }
        else
        {
            myRB.velocity = Vector2.zero;
        }
    }
}
