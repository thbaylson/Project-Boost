using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float loadLevelDelay;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip failureSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failureParticles;

    // CACHE - e.g. references for readability or speed
    AudioSource audioSource;

    // STATE - private instance (member) variables
    bool isTransitioning;

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.isTransitioning = false;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) return;

        switch(other.gameObject.tag){
            case "Friendly":{
                //Debug.Log("We collided with something friendly");
                break;
            }
            case "Finish":{
                //Debug.Log("We collided with the landing pad");
                StartFinishSequence();
                break;
            }
            default:{
                //Debug.Log("We collided with something unfriendly");
                StartCrashSequence();
                break;
            }
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(failureSFX);
        failureParticles.Play();
        GetComponent<Movement>().enabled = false; 
        Invoke("ReloadLevel", loadLevelDelay);
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Invoke("LoadNextLevel", loadLevelDelay);
    }

    void ReloadLevel()
    {
        LoadRelativeLevel(0);
    }

    void LoadNextLevel(){
        LoadRelativeLevel(1);
    }

    public static void LoadRelativeLevel(int relativeOffset)
    {
        //Find the scene index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Add the relative offset
        int nextSceneIndex = currentSceneIndex + relativeOffset;
        //Load the next scene. If we're currently in the last scene, the next scene is the first scene
        SceneManager.LoadScene(nextSceneIndex % SceneManager.sceneCountInBuildSettings);
    }
}
