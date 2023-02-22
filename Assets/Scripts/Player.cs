using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Vector3 direction;
    [SerializeField]
    float speed;
    public Ground_Spawner ground_spawner;
    public static bool isFallen = true; // oyunun bastan baslamasi gibi islemlerde lazim olacagi icin public ve static. oyuncunun olup olmedigini kontrol edecek.
    public float speed_increment; //oyunu hizlandirarak zorlastirmak icin

    float score; //puani verecek deger
    float incremet = 5f; //puani artiracak deger

    [SerializeField]
    Text scoreText; //puani ekrana yazdiracak text

    [SerializeField]
    Text bestScoreText;



    [SerializeField]
    GameObject restartPanel, playGamePanel;

    [SerializeField]
    Text bestScorePanelText, bestScorePanel2Text;

    //highscore degiskenleri

    int bestScore;



    void Start()
    {
        if (RestartGame.isRestart)
        {
            playGamePanel.SetActive(false);
            isFallen = false;
        }
        //ground_spawner = new Ground_Spawner();
        direction = Vector3.back; //oyun baslayince z ekseninde gitmesini istiyoruz
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestScoreText.text = "Best Score: " + bestScore;
        bestScorePanelText.text = bestScoreText.text;
        bestScorePanel2Text.text = bestScoreText.text;
    }




    void Update()
    {

        if (isFallen)
        { return;
        }
        //hareket islemi yon belirleme
        if (Input.GetMouseButtonDown(0))
        {
            //duz gidiyorsa (z ekseni)
            if (direction.x == 0)
            {
                //sola dogru yon degistir (x ekseni yonunde gitsin)
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.back;
            }
        }

        death();

    }


    void death()
    {
        if (transform.position.y < 0.63f)
        {
            isFallen = true;

            Destroy(gameObject, 2f);
            restartPanel.SetActive(true);


        }
        if (bestScore < (int)score)
        {
            bestScore = (int)score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }


    }
    private void FixedUpdate()
    {
        if (isFallen)
        {
            return; //hareket etme ve puan artis islemleri dursun
        }
        Vector3 move = direction * speed * Time.deltaTime;
        speed += speed_increment * Time.deltaTime; // hizlanma zorlugu neyse her saniye hiza onu ekle. DEGISTIRILECEK *SKORA GORE DUZENLENECEK*
        transform.position += move;

        //Puan islemleri

    }

    private void OnCollisionExit(Collision collision)
    {
        //oyuncu zemini terk ettikten sonra yeni zemin olussun

        if (collision.gameObject.CompareTag("Ground"))
        {
            //destroyTile(collision.gameObject); //üzerini terk etti?im zemini yok et
            if (!isFallen)
            {
                StartCoroutine(destroyTile(collision.gameObject));
            }


            ground_spawner.createTile();
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Diamond"))
        {
            Destroy(collision.gameObject);
            score += incremet * speed;
            scoreText.text = "Score: " + ((int)score).ToString();
        }
    }

    IEnumerator destroyTile(GameObject targetTile)
    {

        yield return new WaitForSeconds(0.1f);
        targetTile.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(0.3f);
        targetTile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        targetTile.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        targetTile.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        targetTile.GetComponent<MeshRenderer>().enabled = true;
        Destroy(targetTile);
    }

    public void startGame()
    {
        isFallen = false;
        playGamePanel.SetActive(false);
    }
}
