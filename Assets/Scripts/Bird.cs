using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Bird : MonoBehaviour
{
    public birdType birdType;
    Vector3 Destination = Vector3.zero;
    float yRot;
    Animator animator;

    private void Start()
    {
        ChangeBirdColor();
        animator = GetComponent<Animator>();    
    }

    void ChangeBirdColor()
    {
        switch (birdType)
        {
            case birdType.blueBird:
                ChangeColor(Color.blue);
                break;
            case birdType.RedBird:
                ChangeColor(Color.red);
                break;
            case birdType.GreenBird:
                ChangeColor(Color.green);
                break;
            default:
                ChangeColor(Color.blue);
                break;
        }
    }

    public void moveBirdTo(Vector3 _destination,float _yRot)
    {
        Destination = _destination;
        yRot = _yRot;
        StartCoroutine("moveObjectEnum");
    }

    public IEnumerator moveObjectEnum()
    {
        Vector3 Origin = transform.localPosition;
        float totalMovementTime = 0.5f;
        float currentMovementTime = 0f;
        if(animator != null)
            animator.SetBool("fly", true);
        while (Vector3.Distance(transform.localPosition, Destination) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(Origin, Destination, currentMovementTime / totalMovementTime);
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(0f, yRot, 0f);
        if(animator != null)
            animator.SetBool("fly", false);
    }


    void ChangeColor(Color _color)
    {
        GetComponent<SpriteRenderer>().color = _color;
    }

    public void birdSelected()
    {
        ChangeColor(Color.black);
    }

    public void birdDeselected()
    {
        ChangeBirdColor();
    }

}

