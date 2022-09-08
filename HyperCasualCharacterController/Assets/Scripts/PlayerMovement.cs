using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed = 500;

    private Touch _touch;

    private Vector3 _touchUp;
    private Vector3 _touchDown;

    private bool _dragStarted;
    private bool _isMoving;



    void Start()
    {
        
    }


    void Update()
    {
        if(Input.touchCount > 0) //user touchingg to the screen 
        {
            _touch = Input.GetTouch(0); //we get the first touch (multiple touches will be prevented)
            if(_touch.phase == TouchPhase.Began) //first touch
            {
                _dragStarted = true;
                _isMoving = true;

                _touchDown = _touch.position;
                _touchUp = _touch.position;


            }
 
        }

        if (_dragStarted) //player is touching to the screen
        {
            if(_touch.phase == TouchPhase.Moved) //player is moving its finger and touching to the screen
            {
                _touchDown = _touch.position;
            }

            if(_touch.phase == TouchPhase.Ended) //player lifted their finger and no longer touching to the screen that means player is also not moving anymore
            {
                _touchDown = _touch.position;
                _isMoving = false;
                _dragStarted = false;
            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(),rotationSpeed * Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        
    }

    Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up); //looking for y axis in order to rotate it


        return temp;
    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp = (_touchDown - _touchUp).normalized; //this will calculate the direction of touch it will give vector3 direction to guide player movement
        temp.z = temp.y; //bcs we want to move the character on Z axis
        temp.y = 0; //we need to assign this to 0 bcs we dont want our character move on Y axis
        return temp;        
    }


}
