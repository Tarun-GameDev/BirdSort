using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform aselectedBrachId;
    public Transform bSelectedBranchId;
    public bool branchSelected = false;

    public bool levelComplete = false;
    public bool levelFailed = false;


    List<Bird> _movableBirds = new List<Bird>();

    public int branchesComplete = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckSelectBrach(Transform _branchId)
    {
        if (branchSelected)
        {
            if(_branchId != aselectedBrachId)
            {
                bSelectedBranchId = _branchId;
                checkForSendBranch();
            }
        }
        else
        {
            aselectedBrachId = _branchId;
            SelectBranch(_branchId);
        }
            

    }

    void SelectBranch(Transform _branchId)
    {
        if (levelComplete || levelFailed)
            return;

        branchSelected = true;

        if (aselectedBrachId.childCount < 1)
            return;

        birdType _firstBirdType = aselectedBrachId.GetChild(aselectedBrachId.childCount - 1).GetComponent<Bird>().birdType;
        for (int i = 0; i < aselectedBrachId.childCount; i++)
        {
            if (aselectedBrachId.GetChild(aselectedBrachId.childCount - (i + 1)).GetComponent<Bird>().birdType == _firstBirdType)
            {
                _movableBirds.Add(aselectedBrachId.GetChild(aselectedBrachId.childCount - (i + 1)).GetComponent<Bird>());
            }
            else
                break;
        }

        for (int i = 0;i <= _movableBirds.Count;i++)
        {
            foreach (Bird _bird in _movableBirds)
            {
                _bird.birdSelected();
            }
        }
        print(aselectedBrachId);
    }

    void checkForSendBranch()
    {
        if ((bSelectedBranchId.childCount + _movableBirds.Count) <= 4)
        {
            foreach (Bird _bird in _movableBirds)
            {
                _bird.transform.parent = bSelectedBranchId;
                
            }
            bSelectedBranchId.GetComponent<Branch>().changeBirdPos();

            //if there are less space to move to another branch than the selected amount then deselect the selected branch
            print("Birds Moved from" + aselectedBrachId + "to" + bSelectedBranchId);

            bSelectedBranchId.GetComponent<Branch>().CheckForMatch();
            
        }
        for (int i = 0; i <= _movableBirds.Count; i++)
        {
            foreach (Bird _bird in _movableBirds)
            {
                _bird.birdDeselected();
            }
        }
        _movableBirds.Clear();
        branchSelected = false;
    }

    public void CheckForEndGame()
    {
        branchesComplete++;
        if(branchesComplete == 3)
        {
            UIManager.instance.LevelComplete();
        }
    }
}

