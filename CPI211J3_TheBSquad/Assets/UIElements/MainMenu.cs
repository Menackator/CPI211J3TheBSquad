using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip StartSound;
    public AudioClip EndSound;
    private AudioSource MenuSounds;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuSounds = GetComponent<AudioSource>();
    }
    public void PlayGame()
    {
        MenuSounds.clip = StartSound;
        MenuSounds.Play();
        StartCoroutine(WaitUntilEndOfStartClip());
    }

    IEnumerator WaitUntilEndOfStartClip()
    {
        yield return new WaitForSeconds(StartSound.length);
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        MenuSounds.clip = EndSound;
        MenuSounds.Play();
        StartCoroutine(WaitUntilEndOfEndClip());
    }

    IEnumerator WaitUntilEndOfEndClip()
    {
        yield return new WaitForSeconds(EndSound.length);
        Application.Quit();
    }
}
