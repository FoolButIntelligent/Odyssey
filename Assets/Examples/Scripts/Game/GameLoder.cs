using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLoder : Singleton<GameLoder>
{
    public UnityEvent OnLoadStart;
    public UnityEvent OnLoadFinish;
    public bool isLoading { get; protected set; }
    public UIAnimator loadingScreen;
    public float loadingProgress { get; protected set; }
    public string currentScene => SceneManager.GetActiveScene().name;

    [Header("Minimum Time")] 
    public float starDelay = 1f;
    public float finishDelay = 1f;
    
    public virtual void Reload()
    {
        StartCoroutine(LoadRoutine(currentScene));
    } 
    
    public virtual void Load(string scene)
    {
        if (!isLoading && (currentScene != scene))
        {
            StartCoroutine(LoadRoutine(scene));   
        }
    }

    protected virtual IEnumerator LoadRoutine(string scene)
    {
        OnLoadStart?.Invoke();
        isLoading = true;
        loadingScreen.SetActive(true);
        loadingScreen.Show();

        yield return new WaitForSeconds(starDelay);

        var operation = SceneManager.LoadSceneAsync(scene);
        loadingProgress = 0;

        while (!operation.isDone)
        {
            loadingProgress = operation.progress;
            yield return null;
        }

        loadingProgress = 1;

        yield return new WaitForSeconds(finishDelay);
        isLoading = false;
        loadingScreen.Hide();
        OnLoadFinish?.Invoke();
    }
}
