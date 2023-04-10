using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMotorOmerHocam : MonoBehaviour
{
	// public int rotateDirection;
	public Vector2 speed = new Vector2();
    public float rotationRadius;
    public float rotateSpeed; 
    public float rotationStartAngle;


    // dairesel hareket icin
    private Vector3 ballCenter;
    private float _angle,radianAngle;
    private Vector3 position;


    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    public AudioClip clip;
    private AudioSource sourc;

    void Start()
    {
        radianAngle = 0f;
        sourc = GetComponent<AudioSource>();
    	rigidbody2d = GetComponent<Rigidbody2D>();
		ballCenter.x = transform.position.x;
		ballCenter.y = transform.position.y;

        // ballCenter.x = transform.position.x;
        // ballCenter.y = transform.position.y;
        // ballCenter.z = transform.position.z;

        // ballCenter = transform.position;

        position = rigidbody2d.position;

    }


        void Update()
    {
       
    }
    // Update is called once per frame
    void FixedUpdate()

    {
    	            

        if (rotateSpeed==0){
          //  Vector3 position = rigidbody2d.position;
        // Returns the length of this vector 
        position.x = position.x + speed.x * Time.deltaTime;
        position.y = position.y + speed.y * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        }  
         if(rotateSpeed != 0)
        {
            _angle += rotateSpeed * Time.deltaTime/10;
            radianAngle = (rotationStartAngle * Mathf.PI)/180;
        	float x =  rotationRadius * Mathf.Cos(radianAngle) - Mathf.Cos(radianAngle +_angle)* rotationRadius;
                        //Debug.Log("rotationStartAngle " + Mathf.Cos(rotationStartAngle) );

        	float y = Mathf.Sin(radianAngle +_angle)* rotationRadius - rotationRadius * Mathf.Sin(radianAngle);
            var offset = new Vector3(x, y ,0) ;
            position = ballCenter + offset;
        	rigidbody2d.MovePosition(position);

        }
  //     if (rotationStartAngle!=0){
    //    	_angle += rotateSpeed * Time.deltaTime;
      //  	float x = Mathf.Cos(_angle+1.3f)* rotationRadius;
        //	float y = rotationRadius- Mathf.Sin(_angle+1.3f)* rotationRadius;
          //  var offset = new Vector3(x, y ,0) ;
           // position = ballCenter + offset;
        //	rigidbody2d.MovePosition(position);
       // }
    }

    void CircularMovement(){

    }

      void OnCollisionEnter2D(Collision2D collision)
    {
       
            if (collision.collider.tag == "woodWall")
            {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                if (rotateSpeed==0){
            	Vector2 contactVectorNormal = collision.contacts[0].normal;
            	float magnt = speed.magnitude;
            	Vector2 newDirection = Vector2.Reflect(speed.normalized, contactVectorNormal);
            	speed.x = magnt * newDirection.x;
        		speed.y = magnt * newDirection.y;
            }else{
                
                rotateSpeed = -1*rotateSpeed;
                rotationStartAngle = 90 - (radianAngle);
                
                //Debug.Log("rotateSpeed: " + rotateSpeed);
            }
            sourc.PlayOneShot(clip);
            }
        
        
            if(collision.collider.tag == "player")
            {
               // var speed = lastVelocity.magnitude;
               // Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                // rigid.velocity = direction * Mathf.Max(speed, 0f);
            }
        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "woodWall")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(),false);
        }
            
    }
}
