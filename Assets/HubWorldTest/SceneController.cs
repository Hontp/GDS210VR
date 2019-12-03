using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private string samuraiCutter = "InputSceneName";
    [SerializeField]
    private string towerDrop = "InputSceneName";
    [SerializeField]
    private string alienEscape = "InputSceneName";
    

    // Start is called before the first frame update


    public void SamuraiCutter()
    {
        SceneManager.LoadScene(samuraiCutter);
    }

    public void TowerDefense()
    {
        SceneManager.LoadScene(towerDrop);
    }

    public void AlienEscape()
    {
        SceneManager.LoadScene(alienEscape);
    }




}
