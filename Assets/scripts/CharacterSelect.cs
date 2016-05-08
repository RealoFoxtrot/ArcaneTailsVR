using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {

    public GameObject FocusPoint;
    public GameObject MenuUp;
    public GameObject MenuDown;
    public Toggle toggle;
    public GameObject Audio;


    void start()
    {
        PlayerPrefs.SetInt("BackgroundMusic", 1);
    }

    void Update()
    {
        
    }

    public void QuitGame()
    {

        Application.Quit();

    }

    public void ChangeMusicBool(bool enabled)
    {
        enabled = toggle.isOn;
        if (!enabled)
        {
            Audio.SetActive(false);
            PlayerPrefs.SetInt("BackgroundMusic", 0);
            print(PlayerPrefs.GetInt("BackgroundMusic"));
        }
        else {
            Audio.SetActive(true);
            PlayerPrefs.SetInt("BackgroundMusic", 1);
            print(PlayerPrefs.GetInt("BackgroundMusic"));
        }
    }

    public void EnableMusic()
    {
        PlayerPrefs.SetInt("BackgroundMusic", 1);
    }

    public void levelselect(int l)
    {

        if (l == 0)
        {
            SceneManager.LoadScene("TrainingLevel");
        }
        if (l == 1)
        {
            SceneManager.LoadScene("ToadLevel");
        }
        else if (l == 2)
        {
            SceneManager.LoadScene("RatLevel");
        }
        else if (l == 3)
        {
            SceneManager.LoadScene("Kitten-Test");
        }
    }


    public void MoveMenuUp(bool clickUp)
    {
        if (clickUp == true)
        {
            StartCoroutine(MoveUP());
            clickUp = false;
        }
    }

    public void MoveMenuFocus(bool clickcenter)
    {
        if (clickcenter == true)
        {
            StartCoroutine(MoveFocus());
            clickcenter = false;
        }
    }

    public void MoveMenuDown(bool clickDown)
    {
        if (clickDown == true)
        {
            StartCoroutine(MoveDown());
            clickDown = false;
        }
    }

    IEnumerator MoveUP()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, MenuUp.transform.position, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (this.transform.position == MenuUp.transform.position)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    IEnumerator MoveFocus()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, FocusPoint.transform.position, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (this.transform.position == FocusPoint.transform.position)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    IEnumerator MoveDown()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, MenuDown.transform.position, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (this.transform.position == MenuDown.transform.position)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

}


