using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using TMPro;


public class SceneLoader : MonoBehaviour
{
    [SerializeField]private TMP_Text percentText;
    void Start()
    {
        //percentText = GetComponent<TMP_Text>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    //IEnumerator LoadSceneRoutine(string sceneName)
    //{
    //    AsyncOperation op =
    //    SceneManager.LoadSceneAsync(sceneName);
    //    while (op.isDone == false)
    //    {
    //        percentText.text =
    //        Mathf.Round((op.progress * 100) / 100)
    //        + "%";
    //        yield return null;
    //    }

    //    AsyncOperationHandle op = Addressables.LoadSceneAsync(sceneName);
    //    while (op.PercentComplete < 1)
    //    {
    //        percentText.text =
    //        string.Format("Loading: {0}%",
    //        (int)(op.PercentComplete * 100));
    //        yield return null;
    //    }
    //}

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        AsyncOperation op =
        SceneManager.LoadSceneAsync(sceneName);
        while (op.isDone == false)
        {
            percentText.text = Mathf.Round((op.progress * 100) / 100)
            + "%";
            yield return null;
        }
    }


}
