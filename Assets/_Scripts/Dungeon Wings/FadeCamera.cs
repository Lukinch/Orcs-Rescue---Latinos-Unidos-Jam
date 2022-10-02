using UnityEngine;

public class FadeCamera : MonoBehaviour
{
    [SerializeField] AnimationCurve FadeCurve;

    float alpha;
    private Texture2D texture;
    private bool done;
    private float time;
    bool shouldFade;
    WingManager wingManager;

    private void Awake()
    {
        wingManager = FindObjectOfType<WingManager>();
    }

    public void Fade()
    {
        texture = new Texture2D(1, 1);
        shouldFade = true;
    }

    public void OnGUI()
    {
        if (shouldFade)
        {
            if (!done)
            {
                texture.SetPixel(0, 0, new Color(0, 0, 0, alpha));
                texture.Apply();

                time += Time.deltaTime;
                alpha = FadeCurve.Evaluate(time);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

                if (alpha >= 1) done = true;
            } else
            {
                shouldFade = false;
                wingManager.OnCinematicEnded();
            }
        }
    }
}
