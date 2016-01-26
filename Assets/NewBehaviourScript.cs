using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

	private int mover = 0; //Variable donde se almacena el tipo de movimiento a realizar (adelante o atrás)
	Animation animacion; //Conjunto de animaciones del personaje
    float jumpSpeed = 5.0f; //Velocidad de salto
	BoxCollider coll; //Box collider del personaje
	bool agacha = false; //Booleano para saber si el personaje está agachado o no

	// Use this for initialization
	void Start ()
	{
		animacion = GetComponent<Animation> ();
		coll = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Comprobación de teclas pulsadas para ejecutar el movimiento deseado y mostrar
		//la animación correspondiente.
		mover = 0;
		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.LeftControl)) {
			animacion.CrossFade ("crouchwalk1");
			mover = 1;
			Crawl ();
		} else if (Input.GetKey (KeyCode.W)) {
			animacion.CrossFade ("walkneutral");
			mover = 1;
		} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.LeftControl)) {
			animacion.CrossFade ("crouchwalk1");
			mover = 2;
			Crawl ();
		} else if (Input.GetKey (KeyCode.S)) {
			animacion.CrossFade ("walkneutral");
			mover = 2;
		} else if (Input.GetKey (KeyCode.LeftControl)) {
			animacion.CrossFade ("crouchidle");
			Crawl ();
		}


		// Vuelve a dejar el tamaño del Box Collider del personaje a su tamaño por defecto
		if (agacha && !Input.GetKey (KeyCode.LeftControl)) {
			coll.center = new Vector3(coll.center.x, coll.center.y * 2, coll.center.z);
			coll.size = new Vector3 (coll.size.x, coll.size.y * 2, coll.size.z);
			agacha = false;
		}

		//Comprobación de teclas para rotar al personaje
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (0, -90 * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (0, 90 * Time.deltaTime, 0);
		}

		//Si se pulsa espacio se debe de saltar
		if (Input.GetKey (KeyCode.Space)) {
            transform.position += transform.up * jumpSpeed * Time.deltaTime;
			if(!animacion.IsPlaying("jump") && !Input.GetKeyDown (KeyCode.Space))
				animacion.CrossFade ("jump");
			animacion.CrossFade("inair");
        }


        //Si no hay ninguna tecla pulsada se reproduce la animación "idle"
		if (!Input.anyKey)
			animacion.CrossFade ("idle");

		//En este switch se realiza el movimiento del personaje en función
		//del valor de la variable mover, modificada en el primer if
		switch (mover) {
		case 1:
			transform.position += transform.forward * Time.deltaTime;
			break;
		case 2:
			transform.position -= transform.forward * Time.deltaTime;
			break;
		default:
			break;
		}

	}

	//Método que reduce el tamaño del box collider del personaje cuando este se agacha
	//Invocado cuando se pulsa la tecla 'Control Izquierdo'
	void Crawl() {
		if (!agacha) {
			coll.center = new Vector3 (coll.center.x, coll.center.y / 2, coll.center.z);
			coll.size = new Vector3 (coll.size.x, coll.size.y / 2, coll.size.z);
			agacha = true;
		}
	}
}