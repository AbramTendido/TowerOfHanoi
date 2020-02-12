using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public CurrentScene currentScene;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        //PlayBG();
    }

    public void PlayBG()
    {
        Debug.Log(currentScene.scene);
        Debug.Log((int)currentScene.scene);

        int clipToPlay = (int)currentScene.scene;
        audioSource.Stop();
        audioSource.clip = audioClips[clipToPlay];
        //audioSource.PlayOneShot(audioClips[clipToPlay]);
        audioSource.Play(0);
    }
}
