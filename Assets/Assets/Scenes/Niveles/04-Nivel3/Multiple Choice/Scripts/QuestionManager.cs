using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    //Elementos de UI (asignar por inspector)
    public TMP_Text questionText;
    public TMP_Text[] answersTexts;
    public GameObject questionCanvas;
    public TMP_Text feedbackText;
    public Animator feedbackAnim;
    

    //El Manager va a cargar estas variables en LoadQuestion
    //Se trata del indice de la respuesta correcta y del trigger que contiene la pregunta que se esta haciendo
    int currentCorrectAnswer;
    QuestionTrigger currentTrigger;

    //Variable del jugador (se necesita para evitar que se mueva una vez que empieza la pregunta)
    MC_Player player;

    // Start is called before the first frame update
    void Start()
    {
        //Buscamos al jugador, y apagamos el Canvas de las preguntas (por las dudas)
        player = FindObjectOfType<MC_Player>();
        questionCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //A esta funcion la va a llamar cada trigger que el jugador toque 
    //Recibe por parametro la pregunta a realizar y el trigger que la contiene
    public void LoadQuestion(Question q, QuestionTrigger t)
    {
        //Activamos el Canvas de preguntas, y bloqueamos al player para que no se pueda mover
        questionCanvas.SetActive(true);
        player.canMove = false;
        
        //Hacemos que la pregunta aparezca en el zocalo correspondiente en la UI
        questionText.text = q.question;

        //Lo mismo para las respuestas posibles, recorremos el array y asignamos cada respuesta a su Text
        for(int i = 0; i < answersTexts.Length; i++)
        {
            answersTexts[i].text = q.possibleAnswers[i];
        }

        //Finalmente guardamos la respuesta correcta y el Trigger para mas tarde
        currentCorrectAnswer = q.correctAnswer;
        currentTrigger = t;
    }

    //A esta funcion la llaman los botones de cada respuesta (ver OnClick de cada Button)
    //Recibe por parametro un indice, y cada boton tiene el suyo (de 0 a 3, 4 numeros en total)
    //Entonces si clickeamos el primer boton, pasa por parametro 0
    public void AnswerQuestion(int answerIndex)
    {
        //Ahora que la pregunta fue respuesta (bien o mal) se apaga el Canvas y el jugador se puede mover nuevamente
        questionCanvas.SetActive(false);
        player.canMove = true;

        //Si el indice que cargamos antes con la respuesta correcta es el mismo que el que nos pasa el boton que clickeamos, esta bien
        if(currentCorrectAnswer == answerIndex)
        {
            //Reproducimos una animacion de feedback y cambiamos el texto
            feedbackAnim.Play("Correct Answer");
            feedbackText.text = "�Correcto!";
            currentTrigger.solved = true; //Marcamos al trigger como solved asi no puede volver a hacer esta misma pregunta
            if (currentTrigger.rewardObject != null)
{
    currentTrigger.rewardObject.Activate();
}
        }
        else
        {
            //Reproducimos una animacion de feedback y cambiamos el texto
            feedbackAnim.Play("Incorrect Answer");
            feedbackText.text = "�Incorrecto!";
        }
    }
}
