using Unity.VisualScripting;
using UnityEngine;

public class Progressor : MonoBehaviour
{
    float progress;
    public GameObject playerObject;
    public GameObject mainLights;
    public GameObject darkLights;
    public GameObject mainPosters;
    public GameObject darkPosters;
    public GameObject fakeoutPoster;
    public GameObject deathWall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progress = 0;
        darkLights.SetActive(false);
        darkPosters.SetActive(false);
        fakeoutPoster.SetActive(false);
    }

    void Update()
    {
        if (progress == 1)
        {
            if ((playerObject.transform.rotation.eulerAngles.y < -90 && playerObject.transform.rotation.eulerAngles.y > -270) || (playerObject.transform.rotation.eulerAngles.y > 90 && playerObject.transform.rotation.eulerAngles.y < 270))
            {
                fakeoutPoster.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        playerObject.transform.position = new Vector3(transform.position.x, 9, -3);
        
        switch (progress)
        {
            case 0:
                transform.Translate(0, 0, 9);
                mainLights.SetActive(false);
                darkLights.SetActive(true);
                mainPosters.SetActive(false);
                darkPosters.SetActive(true);
                fakeoutPoster.SetActive(true);
            break;

            case 1:
                darkLights.SetActive(false);
                darkPosters.SetActive(false);
            break;
        }

        progress += 1;
    }
}
