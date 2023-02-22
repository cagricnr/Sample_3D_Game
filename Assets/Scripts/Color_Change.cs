using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Change : MonoBehaviour
{
    public Color[] colors; //renk degisiminde kullanilacak renkler

    Color firstColor, secondColor; //baslangic ve bitis renkleri

    int startingColor; //ilk rengi secmesi icin olusan index
    int nextColor; // ikinci rengi secmesi icin olusan index

    public Material ground_material; //zemin materyaline unity'den ulasmak icin olusturuldu;
    void Start()
    {
        //ilk renk icin index secimi
        startingColor = Random.Range(0, colors.Length);
        //ikinci renk index secimi
        nextColor = pickSecodColor();

        //ilk rengin olusturulmasi
        firstColor = colors[startingColor];
        //ikinci rengin olusturulmas?
        secondColor = colors[nextColor];

        //baslangic renginin zemine verilmesi
        ground_material.color = firstColor;
        //baslangic renginin background'a verilmesi
        Camera.main.backgroundColor = firstColor;




    }
    //ikinci indexi birinci renkten farkli secer
    int pickSecodColor()
    {
        int changedColor;

        changedColor = Random.Range(0, colors.Length); //ikici rengi olusturdum

        while (changedColor == startingColor)
        {
            changedColor = Random.Range(0, colors.Length);
        }

        return changedColor;
    }

    // Update is called once per frame
    void Update()
    {
        Color difference = ground_material.color - secondColor; //RGB degerleri aras?nda cikarma islemi yapacak ama mutlak deger kullanacagiz

        if (Mathf.Abs(difference.r)+Mathf.Abs(difference.g)+Mathf.Abs(difference.b) < 0.2f)
        {
            secondColor = colors[pickSecodColor()];
        }

        //renk degisimi

        ground_material.color = Color.Lerp(ground_material.color, secondColor, 0.003f); //zeminin rengini birinci renkten ikinci renge 0.003f saniyede rgb'nin degisimini sagliyor

        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, secondColor, 0.002f);
    }
}
