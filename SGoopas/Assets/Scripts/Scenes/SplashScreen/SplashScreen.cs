using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public Image unity,osu,goomba;

	void Start () {
        unity.canvasRenderer.SetAlpha(0f);
        osu.canvasRenderer.SetAlpha(0f);
        goomba.canvasRenderer.SetAlpha(0f);
        StartCoroutine("RunFadeAnimation");
    }

    private void Update()
    {
        // Skip the intro with space.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MasterStateMachine.Instance.setState(new MainMenuState());
        }
    }

    IEnumerator RunFadeAnimation() {
        FadeIn(unity);
        yield return new WaitForSeconds(2.5f);
        FadeOut(unity);
        yield return new WaitForSeconds(2.5f);

        FadeIn(osu);
        yield return new WaitForSeconds(2.5f);
        FadeOut(osu);
        yield return new WaitForSeconds(2.5f);

        FadeIn(goomba);
        yield return new WaitForSeconds(2.5f);
        FadeOut(goomba);
        yield return new WaitForSeconds(2.5f);
        MasterStateMachine.Instance.setState(new MainMenuState());
    }
	
    public void FadeIn(Image image)
    {
        image.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    public void FadeOut(Image image)
    {
        image.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
