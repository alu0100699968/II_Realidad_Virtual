using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Caballo : MonoBehaviour
{

    public double speed = 1.5; //Velocidad de movimiento del caballo
    public double spacing = 1.0; //Espacio que se moverá el caballo
    public Animation animacion;

    // Use this for initialization
    void Start()
    {
        animacion = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si no se muestra la animación de caminar, se muestra la animación 'idle'
        //Si la animación de caminar se está reproduciendo, el caballo debe moverse también
        if(!animacion.IsPlaying("Horse_Walk"))
		   animacion.CrossFade("Horse_Idle");
		else
			transform.position += transform.forward * Time.deltaTime;
    }

    //Si el caballo está siendo observado por la cámara de Cardboard, activamos su movimiento
	public void SetGazedAt(bool gazedAt) {
		animacion.CrossFade ("Horse_Walk");
	}
}