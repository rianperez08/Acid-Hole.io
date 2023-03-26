using System.Collections;
using TMPro;
using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    [SerializeField] private int _pointsPerFall = 1;
    [SerializeField] private Transform _groundTransform;
    [SerializeField] private int _maxScore = 5;
    [SerializeField] private CircularProgressBar _progressBar;
    [SerializeField] private AudioClip _soundToPlay;

    public TextMeshProUGUI textMeshPro;
    private int _prevSize = 0;
    private int _prevScore;
    private bool _isResettingProgressBar = false;
    private int _score = 0;
    private float _progress;
    private void Start()
    {
        // Find the circular progress bar object and get the CircularProgressBar component
        GameObject progressBarObject = GameObject.Find("CircularProgressBar");
        if (progressBarObject != null)
        {
            _progressBar = progressBarObject.GetComponent<CircularProgressBar>();
        }
    }

    private void Update()
    {
        textMeshPro.text = _score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        _score += _pointsPerFall;
        Debug.Log("Object has fallen! Score: " + _score);
        AudioSource.PlayClipAtPoint(_soundToPlay, _groundTransform.position);
        if (_score % 5 == 0 && _prevScore % 5 != 0)
        {
            HoleController holeController = FindObjectOfType<HoleController>();
            if (holeController != null)
            {
                holeController.IncreaseHoleSize();
                _progress = 0;
                // Reset progress bar only if it's not already resetting
                if (!_isResettingProgressBar)
                {
                    StartCoroutine(ResetProgressBarAfterDelay(0.5f));
                    _progress = 0;
                }
            }
        }

        _prevScore = _score;
        
        _progress = (float)(_score * 0.2f) / _maxScore;
        _progressBar.SetProgress((_progress * 10) % 1);
    }

    private IEnumerator ResetProgressBarAfterDelay(float delay)
    {
        _isResettingProgressBar = true;
        yield return new WaitForSeconds(delay);
        _progressBar.SetProgress(0);
        _isResettingProgressBar = false;
    }
}
