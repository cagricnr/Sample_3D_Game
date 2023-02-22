using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    [SerializeField]
    GameObject focusObject;

    Vector3 camDistance;

    void Start()
    {
        camDistance = transform.position - focusObject.transform.position; //kamera ile fokus objesi arasindaki uzakligi ald?k
    }




    private void LateUpdate()
    {
        if (Player.isFallen) //buradan ulasabilmek icin isFallen static tan?mland?
        {
            return;
        }
        //kamera islemlerinde kullanilir.
        transform.position = focusObject.transform.position +camDistance;
    }
}
