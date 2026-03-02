using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTPController : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public Animator anim;
    bool fidgetting;
    public Transform headPosition; 
    public List<GameObject> carriedStars = new List<GameObject>();
    public static CustomTPController Instance;
    public Transform cam;
   
    private void Awake()
    {
        Instance = this;
    }

 public void AddStar(GameObject starPrefab)
{
    GameObject nuevaEstrella = Instantiate(
        starPrefab,
        headPosition.position,
        Quaternion.identity,
        headPosition
    );

    // Opcional: offset para que no se superpongan
    nuevaEstrella.transform.localPosition = Vector3.up * carriedStars.Count * 0.5f;

    carriedStars.Add(nuevaEstrella);
}

    public bool HasStars()
    {
        return carriedStars.Count > 0;
    }

   public void RemoveStar()
{
    if (carriedStars.Count == 0) return;

    int ultima = carriedStars.Count - 1;

    Destroy(carriedStars[ultima]);
    carriedStars.RemoveAt(ultima);
}

    
    void Start()
    {
        
    }

    void Update()
    {
       
       if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Estoy apretando la barra espaciadora");
        }
        else
        {
            Debug.Log("No estoy apretando la barra espaciadora");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Aprete la barra espaciadora");
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Solte la barra espaciadora");
        }

        Debug.Log(Input.GetAxis("Vertical"));
        Debug.Log(Input.GetAxisRaw("Vertical"));

        AxisMovement();
        AnimationControls();
    }

    
    void AxisMovement()
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");

    Vector3 input = new Vector3(horizontal, 0f, vertical).normalized;

    if(input.magnitude >= 0.1f)
    {
        // Girar el input según la cámara
        float camYaw = cam.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, camYaw, 0f) * input;

        // Mover el personaje
        transform.position += moveDir * speed * Time.deltaTime;

        // Rotar el personaje hacia donde se mueve
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(moveDir),
                                              rotSpeed * Time.deltaTime);
    }
}

    void AnimationControls()
{
    
    if (Input.GetAxisRaw("Vertical") != 0)
    {
        anim.SetBool("Walking", true);
    }
    else
    {
        anim.SetBool("Walking", false);
    }

    
    anim.SetFloat("Direction", Input.GetAxis("Vertical"));

    
    if (Input.GetKeyDown(KeyCode.Space))
    {
        fidgetting = !fidgetting;
    }
    anim.SetBool("Fidgetting", fidgetting);
}
    
}