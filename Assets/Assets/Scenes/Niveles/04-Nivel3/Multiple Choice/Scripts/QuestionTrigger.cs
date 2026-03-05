using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public PuzzleCollectible rewardObject;
    public Question questionToAsk;
    
    public bool solved;
    
    QuestionManager qm;

    void Start()
    {
        qm = FindObjectOfType<QuestionManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MC_Player>() && !solved)
        {
            qm.LoadQuestion(questionToAsk, this);
        }
    }
}
