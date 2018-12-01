using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticFunctions : MonoBehaviour
{

    public static GameObject audioPrefab;
    public static AudioClip[] clips;
    public static bool paused = false;


    public static void PlayAudio(int clip,bool printNames)
    {
        if (audioPrefab == null)
        {
            audioPrefab = (GameObject)Resources.Load("Prefabs/EmptyAudioPrefab", typeof(GameObject));
        }
        if (clips == null)
        {
            clips = Resources.LoadAll<AudioClip>("SFX");
        }
        AudioSource newClip = Instantiate(audioPrefab, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
        newClip.clip = clips[clip];
        newClip.Play();
        Destroy(newClip.gameObject, clips[clip].length);
        if(printNames == true){
            for (int i = 0; i < clips.Length; i++)
            {
                print(i + " = " + clips[i].name);
            }
        }
    }

    public static void LoadScene(int number)
    {
        SceneManager.LoadScene(number);
    }
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadScene(bool current)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
