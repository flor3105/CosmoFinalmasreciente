using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestionSO")]
public class Question : ScriptableObject
{
    public string question;

    [TextArea(1,5)]
    public string[] possibleAnswers;

    public int correctAnswer;
}
