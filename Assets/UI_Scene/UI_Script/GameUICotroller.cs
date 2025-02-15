using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
	public AudioClip bt;
    public Transform camera;
    public bool isPaused = false;
    public bool is_left = false;
    public bool is_right = false;
	public float speed = 40;
    
    //private InteractableObject[] interactableObjects;
    
    private VisualElement _PauseScreen;
    private VisualElement _CardTree;
    private VisualElement _UpgradeScreen;
    private Button _openPauseScreen;
    private Button _closePauseScreen;
    private Button _openCardTreeButton;
    private Button _closeCardTreeButton;
    private Button _openUpgradeScreenButton;
    private Button _closeUpgradeScreenButton;
    private Button _ExitButton;
    private Button _RestartButton;
    private VisualElement eventBlocker;

	private VisualElement _W_ScrollView;
    private VisualElement _A_ScrollView;
    private VisualElement _M_ScrollView;
    private VisualElement _S_ScrollView;
                //스크롤 뷰 버튼
    private Button _Button_W;
    private Button _Button_A;
    private Button _Button_M;
    private Button _Button_S;
    
    
    //이동위한 쉬프트 버튼들
    private Button shift_left;
    private Button shift_right;

    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;

        _PauseScreen = root.Q<VisualElement>("Pause_Screen");
        _CardTree = root.Q<VisualElement>("CardTree_Screen");
        _UpgradeScreen = root.Q<VisualElement>("Upgrade_Screen");

        _openPauseScreen = root.Q<Button>("Escape_Button");
        _closePauseScreen = root.Q<Button>("Close_Pause_Button");
        _openCardTreeButton = root.Q<Button>("CardTree_Screen_Button");
        _closeCardTreeButton = root.Q<Button>("Close_CardTree_Button");
        _openUpgradeScreenButton = root.Q<Button>("Upgrade_Screen_Button");
        _closeUpgradeScreenButton = root.Q<Button>("Close_Upgrade_Button");

        //카드 트리 스크롤 뷰
        _W_ScrollView = root.Q<VisualElement>("W_scrollview");
        _A_ScrollView = root.Q<VisualElement>("A_scrollview");
        _M_ScrollView = root.Q<VisualElement>("M_scrollview");
        _S_ScrollView = root.Q<VisualElement>("S_scrollview");
                    //카드 트리 스크롤 뷰 버튼
        _Button_W = root.Q<Button>("Button_W");
        _Button_A = root.Q<Button>("Button_A");
        _Button_M = root.Q<Button>("Button_M");
        _Button_S = root.Q<Button>("Button_S");

 		_Button_W.clicked += OnButton_W_Clicked;
        _Button_A.clicked += OnButton_A_Clicked;
        _Button_M.clicked += OnButton_M_Clicked;
        _Button_S.clicked += OnButton_S_Clicked;

        _ExitButton = root.Q<Button>("Exit_Button");
        _RestartButton = root.Q<Button>("Restart_Button");
        
        //쉬프트 버튼
        shift_left = root.Q<Button>("Left_Shift");
        shift_right = root.Q<Button>("Right_Shift");
        
        shift_left.RegisterCallback<PointerEnterEvent>(evt => on_shift_left());
        shift_right.RegisterCallback<PointerEnterEvent>(evt => on_shift_right());
        shift_left.RegisterCallback<PointerLeaveEvent>(evt => off_shift_left());
        shift_right.RegisterCallback<PointerLeaveEvent>(evt => off_shift_right());

        _PauseScreen.style.display = DisplayStyle.None;
        _CardTree.style.display = DisplayStyle.None;
        _UpgradeScreen.style.display = DisplayStyle.None;

        
        _openPauseScreen.clicked += OpenPauseScreen;
        _closePauseScreen.clicked += CloseAllScreens;
        _openCardTreeButton.clicked += ToggleCardTree;
        _closeCardTreeButton.clicked += CloseAllScreens;
        _openUpgradeScreenButton.clicked += ToggleUpgradeScreen;
        _closeUpgradeScreenButton.clicked += CloseAllScreens;
        
        
        _ExitButton.clicked += OnExitButtonClicked;
        _RestartButton.clicked += OnRestartButtonClicked;
        
        
    }

    void Update()
    {
        if (is_left && camera.transform.position.x>=-25)
        {
            float x = -1;
            x=x*speed*Time.deltaTime;
            if(x!=0) CardManager.Inst.CardAlignment();
            camera.Translate(Vector2.right * x);
            // if(x!=0) CardManager.Inst.CardAlignment();
        }

        if (is_right && camera.transform.position.x<=25)
        {
            float x = 1;
            x=x*speed*Time.deltaTime;
            if(x!=0) CardManager.Inst.CardAlignment();
            camera.Translate(Vector2.right * x);
            // if(x!=0) CardManager.Inst.CardAlignment();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_PauseScreen.style.display == DisplayStyle.Flex)
            {
                ResumeGame();
                CloseAllScreens();
            }
            else if (_CardTree.style.display == DisplayStyle.Flex || _UpgradeScreen.style.display == DisplayStyle.Flex)
            {
                ResumeGame();
                CloseAllScreens();
            }
            else
            {
                OpenPauseScreen();
            }
        }
        if (Input.GetKeyDown(KeyCode.U) && _CardTree.style.display == DisplayStyle.None &&
            _PauseScreen.style.display == DisplayStyle.None)
        {
            ToggleUpgradeScreen();
        }

        if (Input.GetKeyDown(KeyCode.T) && _UpgradeScreen.style.display == DisplayStyle.None && _PauseScreen.style.display == DisplayStyle.None)
        {
            ToggleCardTree();
        }
    }
    
    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        /*
        interactableObjects = FindObjectsOfType<InteractableObject>();
        foreach (var obj in interactableObjects)
        {
            obj.SetInteractable(false);
        }
        */
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        /*
         foreach (var obj in interactableObjects)
        {
            obj.SetInteractable(true);
        }
        */
    }
    private void OnExitButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        SceneManager.LoadScene("MapScene");
    }
    private void OnRestartButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
		ResumeGame();
        SceneManager.LoadScene("PT(BETA)");
    }

    private void OpenPauseScreen()
    {
		AudioManager.Instance.PlaySFX(bt);
        PauseGame();
        CloseAllScreens();
        _PauseScreen.style.display = DisplayStyle.Flex; 
    }

    private void ToggleCardTree()
    {
		AudioManager.Instance.PlaySFX(bt);
        if (_CardTree.style.display == DisplayStyle.None)
        {
            PauseGame();
            CloseAllScreens(); 
        }
        else
        {
            ResumeGame();
        }
        _CardTree.style.display = _CardTree.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void ToggleUpgradeScreen()
    {
		AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Upgrade Screen Toggled");
        if (_UpgradeScreen.style.display == DisplayStyle.None)
        {
            PauseGame();
            CloseAllScreens();
            //UICardDataController script = GetComponent<UICardDataController>();
            //script.CreateCardButtons();
        }
        else
        {
            ResumeGame();   
        }
        _UpgradeScreen.style.display = _UpgradeScreen.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void CloseAllScreens()
    {
		AudioManager.Instance.PlaySFX(bt);
        if(_PauseScreen.style.display == DisplayStyle.Flex || _CardTree.style.display == DisplayStyle.Flex || _UpgradeScreen.style.display == DisplayStyle.Flex)
            ResumeGame();   
        _PauseScreen.style.display = DisplayStyle.None;
        _CardTree.style.display = DisplayStyle.None;
        _UpgradeScreen.style.display = DisplayStyle.None;
    }
	private void CloseAllScorllView()
    {
        _W_ScrollView.style.display = DisplayStyle.None;
        _A_ScrollView.style.display = DisplayStyle.None;
        _M_ScrollView.style.display = DisplayStyle.None;
        _S_ScrollView.style.display = DisplayStyle.None;
    }
    //스크롤 뷰 버튼 할당
    private void OnButton_W_Clicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        CloseAllScorllView();
        _W_ScrollView.style.display = DisplayStyle.Flex;
    }
    private void OnButton_A_Clicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        CloseAllScorllView();
        _A_ScrollView.style.display = DisplayStyle.Flex;
    }
    private void OnButton_M_Clicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        CloseAllScorllView();
        _M_ScrollView.style.display = DisplayStyle.Flex;
    }
    private void OnButton_S_Clicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        CloseAllScorllView();
        _S_ScrollView.style.display = DisplayStyle.Flex;
    }

    private void on_shift_left()
    {
        is_left = true;
    }
    private void on_shift_right()
    {
        is_right = true;
    }
    private void off_shift_left()
    {
        is_left = false;
    }
    private void off_shift_right()
    {
        is_right = false;
    }
}
