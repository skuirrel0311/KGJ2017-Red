using UnityEngine;

public class TitleManager : BaseManager<TitleManager>
{
    [SerializeField]
    string nextSceneName = "";

    bool isTransition = false;

    void Update()
    {
		if(MyInputManager.GetButtonDown(MyInputManager.Button.Start))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        if (isTransition) return;
        isTransition = true;
        LoadSceneManager.I.LoadScene(nextSceneName, true, 1.0f, 0.3f);
    }
}
