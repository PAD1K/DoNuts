using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;

public class SwipeUIController : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameObject _swipeGameScreen;
    [SerializeField] private Sprite[] _swipeSprites;
    private Button _pauseButton;
    private OnScreenStick _joystick;
    private Image _swipeImage;
    void Awake()
    {
        _pauseButton = GetComponentInChildren<Button>();
        _joystick = GetComponentInChildren<OnScreenStick>();
        _swipeImage = _swipeGameScreen.GetComponentInChildren<Image>();
    }
    public void ShowUI()
    {
        _pauseMenu.Pause();
        _pauseButton.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
        _swipeGameScreen.SetActive(true);
    }

    public void HideUI()
    {
        _pauseMenu.Pause();
        _pauseButton.gameObject.SetActive(true);
        _joystick.gameObject.SetActive(true);
        _pauseMenu.gameObject.SetActive(false);
        _swipeGameScreen.SetActive(false);
    }
    public void SetUISwipeImage(int swipeDirection)
    {
        _swipeImage.sprite = _swipeSprites[swipeDirection];
    }
}
