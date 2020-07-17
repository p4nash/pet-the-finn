using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    bool isADSed;

    public GameObject ADSWeapon, normalWeapon, ACOGFlash, WeaponFlash;
    public Camera camera;

    float normalCameraSize = 3.25f;
    float ADSCameraSize = 2f;

    public AimScript weapon, ADS;
    
    public bool hasGameStarted = false;
    public bool hasGameEnded = false;

    public Animator MockGame;

    public GameObject enemyPrefab;
    public GameObject ElementsContainer;

    public bool spawnLeftTaken, spawnRightTaken;

    float finnTimer, finnSpawnTime;
    float maxFinnSpawnTime = 18;
    float minFinnSpawnTime = 12;

    float notPettingFinnTimer = 0;

    public GameObject FinnHappy, FinnPet, finnStuff;

    public bool isFinnSpawned = false;
    bool isFinnBeingPet = false;

    float finnPetMeter=0;
    public GameObject finnPetMeterContainer;
    public Image finnPetMeterImage;

    float health = 100;
    public Image healthMeter;
    public GameObject healthMeterContainer;

    public Animator damageAnimator;
    public Color red, green;

    int score;
    public Text scoreText;

    public AudioSource soundEffectsSource;

    public AudioClip gunShot, meow, oof, manOof, shuffle, coin;

    public GameObject EndScreenObj;
    public Text highscoreText;
    public Text finalScore;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        isADSed = false;
        spawnLeftTaken = spawnRightTaken = false;
        finnSpawnTime = Random.Range(minFinnSpawnTime, maxFinnSpawnTime);
        finnTimer = notPettingFinnTimer = 0;


        MockGame.SetTrigger("FadeoutMenu");
        healthMeterContainer.SetActive(true);
        scoreText.gameObject.SetActive(true);

        hasGameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameEnded) {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Menu");
            return; 
        }

        if (Input.GetMouseButtonDown(1))
        {
            soundEffectsSource.PlayOneShot(shuffle);
            isADSed = !isADSed;
            if (isADSed)
            {
                normalWeapon.SetActive(false);
                ADSWeapon.SetActive(true);
                camera.orthographicSize = ADSCameraSize;
            }
            else
            {
                normalWeapon.SetActive(true);
                ADSWeapon.SetActive(false);
                camera.orthographicSize = normalCameraSize;
            }
        }


        //MoveAim(normalAimScript, normalAim, normalWeapon);
        //MoveAim(ADSScript, ADSAim, ADSWeapon);
        if (!isADSed)
        {
            ADS.Deactivate();
            weapon.Activate();
        }
        else
        {
            ADS.Activate();
            weapon.Deactivate();
        }

        finnTimer += Time.deltaTime;

        if(finnTimer >= finnSpawnTime)
        {
            SpawnFinn();
            finnTimer = 0;
            finnSpawnTime = Random.Range(minFinnSpawnTime, maxFinnSpawnTime);
        }

        if (Input.GetMouseButtonDown(0)) {
            soundEffectsSource.PlayOneShot(gunShot);

            if (isADSed)
            {
                ACOGFlash.SetActive(true);
                StartCoroutine(Helper.WaitForFrame(3, () =>{
                    ACOGFlash.SetActive(false);
                }));
            }
            else
            {
                WeaponFlash.SetActive(true);
                StartCoroutine(Helper.WaitForFrame(3, () => {
                    WeaponFlash.SetActive(false);
                }));
            }
            RaycastHit2D[] hits = Physics2D.RaycastAll(camera.transform.position, camera.transform.forward);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<EnemyController>().DestroyEnemy();
                        //Destroy(hit.collider.gameObject);
                        IncrementScore();
                    }
                    Debug.Log("Hit something " + hit.collider.gameObject.name);
                }
                else
                {
                    Debug.Log("Hit nothing");
                }
            }
        }

        if (isFinnSpawned)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isFinnBeingPet)
            {
                FinnPet.SetActive(true);
                FinnHappy.GetComponent<Animator>().enabled = false;
                FinnHappy.SetActive(false);
                FinnHappy.transform.position = FinnPet.transform.position;
                notPettingFinnTimer = 0;
                isFinnBeingPet = true;
                finnPetMeter += 5;
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                isFinnBeingPet = false;
            }
            else if (!isFinnBeingPet)
            {
                notPettingFinnTimer += Time.deltaTime;
                finnPetMeter -= Time.deltaTime * 4;
            }

            if(notPettingFinnTimer > 1)
            {
                FinnPet.SetActive(false);
                FinnHappy.SetActive(true);
            }

            if (finnPetMeter < 0) finnPetMeter = 0;
            if (finnPetMeter >= 100)
            {
                finnPetMeter = 100;
                FinnPetSuccessfully();
            }
            finnPetMeterImage.fillAmount = finnPetMeter / 100;
        }

    }

    void SpawnFinn()
    {
        if (isFinnSpawned) return;

        soundEffectsSource.PlayOneShot(meow);
        ADS.GetComponent<AimScript>().sens = 10f;
        weapon.GetComponent<AimScript>().sens = 5f;

        finnStuff.SetActive(true);
        FinnHappy.SetActive(true);

        isFinnSpawned = true;
        isFinnBeingPet = false;

        finnPetMeter = 0;
        finnPetMeterImage.fillAmount = finnPetMeter / 100;

        finnPetMeterContainer.SetActive(true);
    }

    void FinnPetSuccessfully()
    {
        ADS.GetComponent<AimScript>().sens = 40f;
        weapon.GetComponent<AimScript>().sens = 20f;

        finnStuff.SetActive(false);
        FinnHappy.SetActive(false);
        FinnHappy.GetComponent<Animator>().enabled = true;
        FinnPet.SetActive(false);

        isFinnSpawned = false;
        isFinnBeingPet = false;

        finnPetMeter = 0;
        finnPetMeterImage.fillAmount = finnPetMeter / 100;

        finnPetMeterContainer.SetActive(false);

        finnTimer = 0;
        finnSpawnTime = Random.Range(minFinnSpawnTime, maxFinnSpawnTime);
    }

    public void DoDamage()
    {
        health -= 10;
        soundEffectsSource.PlayOneShot(oof);
        damageAnimator.SetTrigger("Damage");
        damageAnimator.gameObject.GetComponent<SpriteRenderer>().color = red;

        if(health <= 0)
        {
            health = 0;
            EndGame();
        }

        healthMeter.fillAmount = health / 100;
    }

    void EndGame()
    {
        hasGameEnded = true;
        finalScore.text = "Your score was " + score;
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        if(score > highscore)
        {
            highscoreText.text = "A new highscore!";
            PlayerPrefs.SetInt("Highscore", score);
        }
        else if(highscore > 0)
        {
            highscoreText.text = "Previous highscore was " + highscore;
        }
        else
        {
            highscoreText.gameObject.SetActive(false);
        }
        EndScreenObj.SetActive(true);
    }

    void IncrementScore()
    {
        score += 5;
        if (score > 0 && score % 20 == 0)
        {
            if (minFinnSpawnTime > 0)
                minFinnSpawnTime--;
            if (maxFinnSpawnTime > 0)
                maxFinnSpawnTime--;

            if (health < 100)
            {
                soundEffectsSource.PlayOneShot(coin);
                health = (health + 20 > 100) ? 100 : health + 20;
                healthMeter.fillAmount = health / 100;
                damageAnimator.gameObject.GetComponent<SpriteRenderer>().color = green;
                damageAnimator.SetTrigger("Damage");
            }
        }
        scoreText.text = "Score: " + score;
    }
}
