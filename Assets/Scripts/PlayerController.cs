using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isADSed;

    public GameObject ADSWeapon, normalWeapon;
    public Camera camera;

    float normalCameraSize = 3.25f;
    float ADSCameraSize = 2f;

    public GameObject ADSAim, normalAim;
    public AimScript normalAimScript, ADSScript;

    public float sens = 30f;
    public float ADSsens = 50f;

    bool hasGameStarted = false;

    public GameObject MenuOverlay;
    public Animator MockGame;

    public GameObject enemyPrefab;
    public GameObject ElementsContainer;

    public GameObject spawnLeft, spawnRight, spawnLeftObject, spawnRightObject;
    public bool spawnLeftTaken, spawnRightTaken;

    float timer, enemySpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        isADSed = false;
        spawnLeftTaken = spawnRightTaken = false;
        enemySpawnTime = Random.Range(3, 7);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasGameStarted){
            if (Input.anyKeyDown)
            {
                hasGameStarted = true;
                MenuOverlay.SetActive(false);
                MockGame.SetTrigger("FadeoutMenu");
            }
            else
                return;
        }
        if (Input.GetMouseButtonDown(1))
        {
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


        if (!isADSed) {
            MoveAim(normalAimScript, normalAim, normalWeapon);
        }
        else
        {
            MoveAim(ADSScript, ADSAim, ADSWeapon);
        }

        if(spawnRightObject == null)
        {
            spawnRightTaken = false;
        }
        if(spawnLeftObject == null)
        {
            spawnLeftTaken = false;
        }

        timer += Time.deltaTime;

        if(timer >= enemySpawnTime)
        {
            Debug.Log("Trying to spawn an enemy");
            SpawnEnemy();
            timer = 0;
            enemySpawnTime = Random.Range(3, 7);
        }

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit2D hit = Physics2D.Raycast(camera.transform.position, camera.transform.forward);
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enemy")
                    Destroy(hit.collider.gameObject);
                Debug.Log("Hit something "+hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Hit nothing");
            }
        }

    }

    void MoveAim(AimScript aimScript, GameObject aimCollider, GameObject aimGraphic)
    {
        if (Input.GetAxis("Mouse X") < 0 && !aimScript.collLeft)
        {
            //Code for action on mouse moving left
            aimCollider.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;
            aimGraphic.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;
        }
        if (Input.GetAxis("Mouse X") > 0 && !aimScript.collRight)
        {
            //Code for action on mouse moving right
            aimCollider.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;
            aimGraphic.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;
        }
        if (Input.GetAxis("Mouse Y") < 0 && !aimScript.collDown)
        {
            //Code for action on mouse moving left
            aimCollider.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
            aimGraphic.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
        }
        if (Input.GetAxis("Mouse Y") > 0 && !aimScript.collUp)
        {
            //Code for action on mouse moving right
            aimCollider.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
            aimGraphic.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
        }
        camera.transform.position = new Vector3(aimCollider.transform.position.x, aimCollider.transform.position.y, camera.transform.position.z);
    }

    void SpawnEnemy()
    {
        if (Random.value < 0.5 && !spawnLeftTaken)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            //enemy.transform.SetParent(ElementsContainer.transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            if (!spawnLeftTaken)
            {
                spawnLeftObject = enemy;
                spawnLeftTaken = true;
                enemy.transform.position = spawnLeft.transform.position;
                enemy.transform.eulerAngles += new Vector3(0, 0, -20);
                enemyController.type = "left";
                enemyController.originalPosition = spawnLeft.transform.position;
                enemyController.Activate();
                Debug.Log("Spawning left enemy");
            }
        }

        else if (Random.value >= 0.5 && !spawnRightTaken)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            //enemy.transform.SetParent(ElementsContainer.transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();

            spawnRightObject = enemy;
            spawnRightTaken = true;
            enemy.transform.position = spawnRight.transform.position;
            enemy.transform.eulerAngles += new Vector3(0, 0, 20);
            enemyController.type = "right";
            enemyController.originalPosition = spawnRight.transform.position;
            enemyController.Activate();
            Debug.Log("Spawning right enemy");
        }
    }
}
