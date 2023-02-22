using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ground_Spawner : MonoBehaviour
{
    // zeminlerin rastgele konumlarda oyuncu ilerledikce olusmasini istedigim icin, oyuncuya child object olusturup bu scripti atadim.
    [SerializeField]
    GameObject newTile;
    [SerializeField]
    GameObject diamond;


    public void createTile()
    {
        Vector3 direction;
        if (Random.Range(0,2)==0)
        {
            direction = Vector3.back;
        }
        else
        {
            direction=Vector3.right;
        }
        newTile = Instantiate(newTile, newTile.transform.position + direction, newTile.transform.rotation); //uc uca zemin ekleyecek
        Vector3 spawnVector = new Vector3(0, 0.8f, 0);
        diamond = Instantiate(diamond, newTile.transform.position + spawnVector, newTile.transform.rotation);

        //newTile pozisyonunda collectible spawnlanacak
    }

    private void Start()
    {
        for (int i = 1; i <=15; i++)
        {
            createTile();
        }
    }
}
