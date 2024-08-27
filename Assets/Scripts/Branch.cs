using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Branch : MonoBehaviour
{
    public int branchId = 0;
    public bool leftBranch = true;

    public List<Bird> sittingBirds = new List<Bird>();

    public void Start()
    {
        changeBirdPos();
    }

    public void changeBirdPos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform selectedChild = transform.GetChild(i);
            if (selectedChild.GetComponent<Bird>() != null)
            {
                sittingBirds.Add(selectedChild.GetComponent<Bird>());

                if (leftBranch == false)
                {
                    selectedChild.GetComponent<Bird>().moveBirdTo(new Vector3((5f) - (2.5f * i), 1.64f, 0f),180f);

                }
                else
                {
                    selectedChild.GetComponent<Bird>().moveBirdTo(new Vector3((-5f) + (2.5f * i), 1.64f, 0f),0f);
                }
                    
            }
        }
    }



    private void OnMouseDown()
    {
        GameManager.Instance.CheckSelectBrach(this.transform);
    }

    public void CheckForMatch()
    {
        if (transform.childCount != 4)
            return;

        bool _match = true;

        birdType _firstBirdType = transform.GetChild(0).GetComponent<Bird>().birdType;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Bird>().birdType != _firstBirdType)
            {
                _match = false;
                return;
            }
                
        }

        if(_match == true)
        {
            GameManager.Instance.CheckForEndGame();
            Destroy(gameObject,0.6f);
        }
    }
}
