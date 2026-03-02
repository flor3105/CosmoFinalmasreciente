using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ExecuteAlways nos permite correr el script dentro del editor y no solo en Play mode
[ExecuteAlways]
public class UVLightReveal : MonoBehaviour
{
    public Light spotlight;
    Material objMaterial;

    void Start()
    {
        //Buscamos el MeshRenderer dentro de este mismo objeto y guardamos su material en la variable
        objMaterial = GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        //Chequeamos que la luz este asignada y que sea de tipo spot light (podemos cambiar eso para otras luces)
        if (spotlight != null && spotlight.type == LightType.Spot)
        {
            //Pasamos la posicion y direccion de la luz
            objMaterial.SetVector("_LightPosition", spotlight.transform.position);
            objMaterial.SetVector("_LightDirection", spotlight.transform.forward);

            //Calculamos el coseno de la mitad del angulo de la luz (desde el centro hasta la punta del cono)
            float spotAngleCosine = Mathf.Cos(spotlight.spotAngle * 0.5f * Mathf.Deg2Rad);
            objMaterial.SetFloat("_SpotAngleCosine", spotAngleCosine);

            //Agregamos el rango de la luz
            objMaterial.SetFloat("_LightRange", spotlight.range);
        }
    }
}
