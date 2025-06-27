using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string titleSceneName;
    [SerializeField] private string bossSceneName;
    [SerializeField] private string entranceToBossScene;

    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip gameMusic;

    [SerializeField] private string nextScene;
    [SerializeField] private float transitionTime;

    [SerializeField] private Animator blackScreenAnimator;

    public static SceneLoader Instance;

    private readonly int fadeInHash = Animator.StringToHash("fadeIn");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(blackScreenAnimator != null)
            blackScreenAnimator.gameObject.SetActive(true);

        LevelMusic.Instance.StartMusic();
        SetMusicLevel();
    }

    private void SetMusicLevel()
    {
        if (GetCurrentScene() == titleSceneName)
            LevelMusic.Instance.ChangeMusic(titleMusic);
        else if (GetCurrentScene() == bossSceneName)
            LevelMusic.Instance.ChangeMusic(bossMusic);
        else if (GetCurrentScene() == entranceToBossScene)
            LevelMusic.Instance.StopMusic();
        else
            LevelMusic.Instance.ChangeMusic(gameMusic);
    }

    public void RestartScene()
    {
        StartCoroutine(LoadSceneRoutine(GetCurrentScene()));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneRoutine(nextScene));
    }

    public void GoToTitleScene()
    {
        StartCoroutine(LoadSceneRoutine(titleSceneName));       
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        blackScreenAnimator.SetTrigger(fadeInHash);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
