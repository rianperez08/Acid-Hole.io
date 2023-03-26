using UnityEngine;
using UnityEngine.UI;

public class CircularProgressBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    Quaternion iniRot;

    private void Start()
    {
        iniRot = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = iniRot;
    }

    // Set the progress of the circular progress bar
    public void SetProgress(float progress)
    {
        _image.fillAmount = progress;
    }
}
