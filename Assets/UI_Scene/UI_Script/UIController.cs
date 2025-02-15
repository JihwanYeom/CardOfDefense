using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public AudioClip bt;
	
	//메인 화면 관련
		//게임시작버튼
	private Button _GameStartButton;
		//조작키 버튼
    private VisualElement _PlayKey;
    private Button _openButtonPlayKey;
    private Button _closeButtonPlayKey;
		//카드 트리 버튼
    private VisualElement _CardTree;
    private Button _openButtonCardTree;
    private Button _closeButtonCardTree;
            // 카드 트리 창
                //스크롤 뷰
    private VisualElement _W_ScrollView;
    private VisualElement _A_ScrollView;
    private VisualElement _M_ScrollView;
    private VisualElement _S_ScrollView;
                //스크롤 뷰 버튼
    private Button _Button_W;
    private Button _Button_A;
    private Button _Button_M;
    private Button _Button_S;
                    //스크롤뷰 호버링
    private VisualElement _Skill_Window;
    private VisualElement _Card_Hover;
		//세팅버튼
    private VisualElement _SettingScreen;
    private Button _openButtonSettingScreen;
    private Button _closeButtonSettingScreen;
		//크레딧 버튼
    private VisualElement _CreditScreen;
    private Button _openButtonCreditScreen;
    private Button _closeButtonCreditScreen;


	// 게임시작 버튼 누를 시 이동할 씬 설정
	[SerializeField] private string targetSceneName = "PT(BETA)";
    void Start()
    {
        // UIDocument와 연결된 root를 불러오기
        var root = GetComponent<UIDocument>()?.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("UIDocument가 할당되지 않았습니다.");
            return;
        }


		_GameStartButton = root.Q<Button>("Button_GameStart");

        // UI 요소 초기화
            //조작키
        _PlayKey = root.Q<VisualElement>("PlayKey_Screen");
        _openButtonPlayKey = root.Q<Button>("Button_PlayKey");
        _closeButtonPlayKey = root.Q<Button>("Button_Close");
            //카드트리
        _CardTree = root.Q<VisualElement>("CardTree_Screen");
        _openButtonCardTree = root.Q<Button>("Button_CardTree");
        _closeButtonCardTree = root.Q<Button>("Button_Close_1");
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
        _Skill_Window = root.Q<VisualElement>("CardSkill_Window");
        _Card_Hover = root.Q<VisualElement>("T_U_0");
        
            //세팅
        _SettingScreen = root.Q<VisualElement>("Setting_Screen");
        _openButtonSettingScreen = root.Q<Button>("Button_Setting");
        _closeButtonSettingScreen = root.Q<Button>("Button_Close_2");
            //크레딧
        _CreditScreen = root.Q<VisualElement>("Credit_Screen");
        _openButtonCreditScreen = root.Q<Button>("Button_Credit");
        _closeButtonCreditScreen = root.Q<Button>("Button_Close_3");

        // UI 초기 상태 설정
        _PlayKey.style.display = DisplayStyle.None;  // 기본적으로 숨김
        _openButtonPlayKey.clicked += OpenPlayKey;
        _closeButtonPlayKey.clicked += ClosePlayKey;

        _CardTree.style.display = DisplayStyle.None;  // 기본적으로 숨김
        _openButtonCardTree.clicked += OpenCardTree;
        _closeButtonCardTree.clicked += CloseCardTree;

        _SettingScreen.style.display = DisplayStyle.None;
        _openButtonSettingScreen.clicked += OpenSetting;
        _closeButtonSettingScreen.clicked += CloseSetting;

        _CreditScreen.style.display = DisplayStyle.None;
        _openButtonCreditScreen.clicked += OpenCredit;
        _closeButtonCreditScreen.clicked += CloseCredit;

		_GameStartButton.clicked += OnSwitchButtonClicked;
        
        _Button_W.clicked += OnButton_W_Clicked;
        _Button_A.clicked += OnButton_A_Clicked;
        _Button_M.clicked += OnButton_M_Clicked;
        _Button_S.clicked += OnButton_S_Clicked;
        
        
        //호버링
        
        /*
        _Skill_Window.style.display = DisplayStyle.None;
        _Card_Hover.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
        _Card_Hover.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        _Card_Hover.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        */
    }
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 각 UI 창이 열려 있으면 닫기
            if (_PlayKey.style.display == DisplayStyle.Flex) ClosePlayKey();
            else if (_CardTree.style.display == DisplayStyle.Flex) CloseCardTree();
            else if (_SettingScreen.style.display == DisplayStyle.Flex) CloseSetting();
            else if (_CreditScreen.style.display == DisplayStyle.Flex) CloseCredit();
        }
    }
    
	private void OnSwitchButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
		Time.timeScale = 1f;
        SceneManager.LoadScene("MapScene");
    }

    // PlayKey 창을 여는 메서드
    private void OpenPlayKey()
    {
		AudioManager.Instance.PlaySFX(bt);
        _PlayKey.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void ClosePlayKey()
    {
		AudioManager.Instance.PlaySFX(bt);
        _PlayKey.style.display = DisplayStyle.None;
    }
    private void OpenCardTree()
    {
		AudioManager.Instance.PlaySFX(bt);
        _CardTree.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseCardTree()
    {
		AudioManager.Instance.PlaySFX(bt);
        _CardTree.style.display = DisplayStyle.None;
    }
    private void OpenSetting()
    {
		AudioManager.Instance.PlaySFX(bt);
        _SettingScreen.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseSetting()
    {
		AudioManager.Instance.PlaySFX(bt);
        _SettingScreen.style.display = DisplayStyle.None;
    }
    private void OpenCredit()
    {
		AudioManager.Instance.PlaySFX(bt);
        _CreditScreen.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseCredit()
    {
		AudioManager.Instance.PlaySFX(bt);
        _CreditScreen.style.display = DisplayStyle.None;
    }
    
    //스크롤 뷰 다 닫는 함수
    private void CloseAllScorllView()
    {
		AudioManager.Instance.PlaySFX(bt);
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
    
    //호버링 함수
    private void OnMouseEnter(MouseEnterEvent evt)
    {
        _Skill_Window.style.display = DisplayStyle.Flex;
        Debug.Log("Mouse entered: " + evt.localMousePosition);
    }

    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        _Skill_Window.style.display = DisplayStyle.None;
        Debug.Log("Mouse left: " + evt.localMousePosition);
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        Vector2 screenMousePosition = Input.mousePosition;

        _Skill_Window.style.left = screenMousePosition.x;
        _Skill_Window.style.top = (Screen.height/2 - screenMousePosition.y) + 550;
        /*
        _Skill_Window.style.position = Position.Absolute;
        _Skill_Window.style.left = Screen.width - evt.localMousePosition.x;
        _Skill_Window.style.top = Screen.height - evt.localMousePosition.y;
        */
        Debug.Log("Mouse moved: " + screenMousePosition);
        Debug.Log("screen y : " + _Skill_Window.style.top);
    }
}
