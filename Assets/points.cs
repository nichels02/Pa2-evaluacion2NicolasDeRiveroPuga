using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class points : MonoBehaviour
{
    //[SerializeField]TextMesh text;
    tm
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Puntaje(int puntaje)
    {
        text.text = "Ponts: "+ puntaje;
    }
}
