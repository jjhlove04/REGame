using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager _instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        _target = 0;    
        _progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        }
        while(scene.progress < 0.8f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);

    }

    private void Update() {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 1.5f * Time.deltaTime);
    }
}
