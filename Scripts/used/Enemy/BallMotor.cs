using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMotor : MonoBehaviour
{
    // public int rotateDirection;
    public Vector2 speed = new Vector2();
    public float rotationRadius;
    public float rotateSpeed;
    public float rotationAngle;

    // dairesel hareket icin
    private Vector3 ballCenter;
    private float _angle;


    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    private gameMotor _motor;
    private music msc;

    void Start()
    {
        if (GameObject.Find("MUSIC") != null)
        {
            msc = GameObject.Find("MUSIC").GetComponent<music>();
        }

        _motor = GameObject.Find("GameMotor").GetComponent<gameMotor>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        //rigidbody2d.mass = 1;
        ballCenter = rigidbody2d.position;
    }


    void FixedUpdate()
    {

        if (!_motor.gamePaused)
        {
            if (rotateSpeed == 0)
            {
                //Vector3 position = gameObject.transform.position;

                //position.x = position.x + speed.x * Time.deltaTime;
                //position.y = position.y + speed.y * Time.deltaTime;

                rigidbody2d.velocity = new Vector2(speed.x,speed.y);
                //rigidbody2d.MovePosition(position);
            }
            else
            {
                Vector3 position = rigidbody2d.position;

                //önce 0 derece baz alınarak topun konumuna göre merkez belirleniyor
                radyanRotation = rotationAngle * Mathf.Deg2Rad;
                float centerX = ballCenter.x - rotationRadius * Mathf.Cos(radyanRotation);
                float centerY = ballCenter.y - rotationRadius * Mathf.Sin(radyanRotation);

                // Debug.Log("Mathf.Cos(rotationAngle) " + Mathf.Cos(radyanRotation));
                // daha sonra belirlenen merkeze hız doğrultusunda delta time lık açılar ekleniyor
                _angle += rotateSpeed * Time.deltaTime;
                position.x = centerX + Mathf.Cos(_angle + radyanRotation) * rotationRadius;
                position.y = centerY + Mathf.Sin(_angle + radyanRotation) * rotationRadius;
                // Debug.Log("x " + position.x);
                rigidbody2d.MovePosition(position);
            }
        }
        else
        {
            rigidbody2d.velocity = Vector2.zero;
        }

    }

    float radyanRotation;

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "woodWall")
        {
            if (rotateSpeed == 0)
            {
                Vector2 contactVectorNormal = collision.contacts[0].normal;
                Vector2 newDirection = Vector2.Reflect(speed, contactVectorNormal);
                speed.x = newDirection.x;
                speed.y = newDirection.y;
            }
            else
            {
                rotateSpeed = -1 * rotateSpeed;
            }
        }

    }

}
