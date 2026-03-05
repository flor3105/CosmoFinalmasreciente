using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text[] answersTexts;
    public GameObject questionCanvas;
    public TMP_Text feedbackText;
    public Animator feedbackAnim;
    CustomTPController playerController;
    TPCameraController cameraController;
    Animator playerAnimator;

    int currentCorrectAnswer;
    QuestionTrigger currentTrigger;

    MC_Player player;

    void Start()
    {
        playerController = FindObjectOfType<CustomTPController>();
        cameraController = FindObjectOfType<TPCameraController>();

        player = FindObjectOfType<MC_Player>();
        playerAnimator = playerController.anim;

        questionCanvas.SetActive(false);
        feedbackText.gameObject.SetActive(false);
    }

    public void LoadQuestion(Question q, QuestionTrigger t)
{
    questionCanvas.SetActive(true);

    playerController.anim.SetBool("Walking", false);
    playerController.anim.SetFloat("Speed", 0);

    player.enabled = false;
    playerController.enabled = false;
    cameraController.enabled = false;

    playerAnimator = playerController.anim;
    playerAnimator.speed = 0;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    questionText.text = q.question;

    for (int i = 0; i < answersTexts.Length; i++)
    {
        answersTexts[i].text = q.possibleAnswers[i];
    }

    currentCorrectAnswer = q.correctAnswer;
    currentTrigger = t;
}

    public void AnswerQuestion(int answerIndex)
    {
        questionCanvas.SetActive(false);
        player.enabled = true;

        playerController.enabled = true;
        cameraController.enabled = true;
        playerAnimator.speed = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        feedbackText.gameObject.SetActive(true);

        if (currentCorrectAnswer == answerIndex)
        {
            feedbackAnim.Play("Correct Answer");
            feedbackText.text = "¡Correcto!";

            currentTrigger.solved = true;

            if (currentTrigger.rewardObject != null)
            {
                currentTrigger.rewardObject.Activate();
            }
        }
        else
        {
            feedbackAnim.Play("Incorrect Answer");
            feedbackText.text = "¡Incorrecto!";
        }

        StartCoroutine(HideFeedback());
    }

    IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2f);
        feedbackText.gameObject.SetActive(false);
    }
}