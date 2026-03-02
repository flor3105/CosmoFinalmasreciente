using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public PuzzleCollectible rewardObject;
    //Guardamos las preguntas en archivos de tipo ScriptableObjects, cada trigger tiene la suya
    public Question questionToAsk;
    
    //Booleano para evitar volver a hacer una pregunta que ya fue contestada correctamente
    public bool solved;
    
    QuestionManager qm;

    // Start is called before the first frame update
    void Start()
    {
        //Buscamos al Manager
        qm = FindObjectOfType<QuestionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Le pasa la pregunta al Manager solo si lo toca el jugador y ademas la pregunta no fue contestada correctamente antes
        //Tambien se pasa a si mismo por parametro, para que despues el Manager marque a solved true si ya esta
        if(other.GetComponent<MC_Player>() && !solved)
        {
            qm.LoadQuestion(questionToAsk, this);
        }
    }
}
