using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator anima;
    private int random;
    private float counter;

    private gameMotor _motor;
    private CircleCollider2D redCircleCollider;

    private void Awake()
    {
        anima = GetComponent<Animator>();

    }

    private void Start()
    {
        _motor = GameObject.Find("GameMotor").GetComponent<gameMotor>();

        counter = 0f;
        anima.enabled = false;
        
        string objectTag = gameObject.tag;
        if (objectTag=="redEnemy"){
            GetComponent<CircleCollider2D>().radius = 2.42f;
            Vector2 newOffset = new Vector2(-0.05f,-0.05f);
            GetComponent<CircleCollider2D>().offset = newOffset;
        }
        if (objectTag=="greenBall"){
            GetComponent<CircleCollider2D>().radius = 2.52f;
            Vector2 newOffset = new Vector2(0f,-0.05f);
            GetComponent<CircleCollider2D>().offset = newOffset;
        }
          if (objectTag=="blackBall"){
            GetComponent<CircleCollider2D>().radius = 2.47f;
        }

        random = Random.Range(0, 4);
    }

    private void Update()
    {
        if (!_motor.gamePaused)
        {
            if (counter <= random)
            {
                counter += Time.deltaTime;
            }
            else
            {
                anima.GetComponent<Animator>().enabled = true;
            }
        }
        else
        {
            anima.GetComponent<Animator>().enabled = false;
        }
    }
}
