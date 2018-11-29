using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : Hitbox
{
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] uint livesAfterDeath = 10;

    void Start()
    {
        StartStuff();
    }

    public override void Die()
    {
        SaveData save = FindObjectOfType<SaveData>();
        if (save.lives != 0)
        {
            StartCoroutine(DeathEvents());
            StaticFunctions.PlayAudio(2,false);
            //Debug.Log(name + " died");
            if (dieShake == true)
            {
                cam.MediumShake();
            }
            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
            }
        }
        else
        {
            if (dieShake == true)
            {
                cam.MediumShake();
            }
            StartCoroutine(GameOverEvents());
        }

    }

    IEnumerator DeathEvents()
    {
        SaveData save = FindObjectOfType<SaveData>();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.9f);
        deathUI.SetActive(true);
        StaticFunctions.PlayAudio(13,false);
        yield return new WaitForSecondsRealtime(1.3f);
        StaticFunctions.PlayAudio(3,false);
        yield return new WaitForSecondsRealtime(0.7f);
        save.lives--;
        SaveLoad.SaveManager(save);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        StaticFunctions.LoadScene(true);
    }

    IEnumerator GameOverEvents()
    {
        SaveData save = FindObjectOfType<SaveData>();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.3f);
        gameOverUI.SetActive(true);
        StaticFunctions.PlayAudio(13,false);
        save.lives = livesAfterDeath;
        SaveLoad.SaveManager(save);
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1;
        StaticFunctions.PlayAudio(13,false);
        StaticFunctions.LoadScene(true);
    }



}
