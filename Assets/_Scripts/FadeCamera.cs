using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCamera : MonoBehaviour
{
    [SerializeField] AnimationCurve FadeCurve;

    private float _alpha = 1;
    private Texture2D _texture;
    private bool _done;
    private float _time;
    bool shouldFade;
    WingManager wingManager;

    private void Awake()
    {
        wingManager = FindObjectOfType<WingManager>();
    }

    public void Fade()
    {
        shouldFade = true;
    }

    public void OnGUI()
    {
        if (shouldFade)
        {
            if (!_done)
            {
                if (_texture == null) _texture = new Texture2D(1, 1);

                _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
                _texture.Apply();

                _time += Time.deltaTime;
                _alpha = FadeCurve.Evaluate(_time);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

                if (_alpha >= 1) _done = true;
            } else
            {
                Debug.Log("DONE!");
                shouldFade = false;
                wingManager.OnCinematicEnded();
            }
        }
    }
}
