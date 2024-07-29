using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    AudioSource pickupSource;
    Animator animator;

    Scene activeScene;

    public float volume = 0.5f;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        activeScene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
            animator.SetTrigger(Animations.isWaking);

            if (activeScene.buildIndex == 1)
            {
                StartCoroutine(LoadScene(activeScene.buildIndex));
            }
            if (activeScene.buildIndex == 2)
            {
                StartCoroutine(LoadScene(activeScene.buildIndex));
            }
            if (activeScene.buildIndex == 3)
            {
                StartCoroutine(LoadScene(activeScene.buildIndex));
            }
        }

    }

    IEnumerator LoadScene(int build_index)
    {
        yield return new WaitForSeconds(3);
        if (build_index == 3)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            SceneManager.LoadSceneAsync(build_index +1);
        }
    }
}
