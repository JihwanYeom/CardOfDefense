using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class Map_UI_Controller : MonoBehaviour
{
    public AudioClip bt;
    private VisualElement Upgrade_Screen;
    private VisualElement checking_screen;
    private VisualElement cardtree_screen;
    private Button cardtree_button;
    private Button cardtree_close_button;
    private Button Upgrade_Button;
    private Button Upgrade_Exit_Button;
    private Button quit_button;
    private Button cancel_button;
    private VisualElement _W_ScrollView;
    private VisualElement _A_ScrollView;
    private VisualElement _M_ScrollView;
    private VisualElement _S_ScrollView;
    //스크롤 뷰 버튼
    private Button _Button_W;
    private Button _Button_A;
    private Button _Button_M;
    private Button _Button_S;
    
    private Button Back_button;
    private Button stage1_1;//1단계
    private Button stage1_2;
    private Button stage1_3;
    private Button stage1_4;
    
    private Button stage2_1;//2단계1
    private Button stage2_2;
    private Button stage2_3;
    private Button stage2_4;
    
    private Button stage22_1;//2단계1
    private Button stage22_2;
    private Button stage22_3;
    private Button stage22_4;
    
    private Button stage3_1;//3단계
    private Button stage32_1;
    private Button stage3_2;
    private Button stage32_2;
    private Button stage3_3;
    private Button stage32_3;
    private Button stage3_4;
    
    private Button stage4_1;//4단계
    private Button stage4_2;
    private Button stage4_3;
    private Button stage4_4;

	private Label cost_left;
    
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        Upgrade_Screen = root.Q<VisualElement>("Upgrade_Screen");
        checking_screen = root.Q<VisualElement>("checking_screen");
        cardtree_screen = root.Q<VisualElement>("CardTree_Screen");
        
        Upgrade_Button = root.Q<Button>("upgrade");
        Upgrade_Exit_Button = root.Q<Button>("Close_Upgrade_Button");
        Back_button = root.Q<Button>("back_to_main");
        
        quit_button = root.Q<Button>("quit_button");
        cancel_button = root.Q<Button>("cancel_button");
        
        //카드트리 버튼
        cardtree_button = root.Q<Button>("cardtree");
        cardtree_close_button = root.Q<Button>("Button_Close_1");
        
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
        
        ///// 스테이지 버튼//////////
        stage1_1 =  root.Q<Button>("1_1_button");
        stage1_2 =  root.Q<Button>("1_2_button");
        stage1_3 =  root.Q<Button>("1_3_button");
        stage1_4 =  root.Q<Button>("1_4_button");
        
        stage2_1 =  root.Q<Button>("2_1_1_button");
        stage2_2 =  root.Q<Button>("2_1_2_button");
        stage2_3 =  root.Q<Button>("2_1_3_button");
        stage2_4 =  root.Q<Button>("2_1_4_button");
        
        stage22_1 =  root.Q<Button>("2_2_1_button");
        stage22_2 =  root.Q<Button>("2_2_2_button");
        stage22_3 =  root.Q<Button>("2_2_3_button");
        stage22_4 =  root.Q<Button>("2_2_4_button");
        
        stage3_1 =  root.Q<Button>("3_1_1_button");
        stage3_2 =  root.Q<Button>("3_1_2_button");
        stage3_3 =  root.Q<Button>("3_1_3_button");
        stage3_4 =  root.Q<Button>("3_4_button");
        
        stage32_1 =  root.Q<Button>("3_2_1_button");
        stage32_2 =  root.Q<Button>("3_2_2_button");
        stage32_3 =  root.Q<Button>("3_2_3_button");
        
        stage4_1 =  root.Q<Button>("4_1_button");
        stage4_2 =  root.Q<Button>("4_2_button");
        stage4_3 =  root.Q<Button>("4_3_button");
        stage4_4 =  root.Q<Button>("4_4_button");

		cost_left = root.Q<Label>("upgrade_cost_left");
        
        Upgrade_Screen.style.display = DisplayStyle.None;
        checking_screen.style.display = DisplayStyle.None;
        cardtree_screen.style.display = DisplayStyle.None;

        cardtree_button.clicked += OnCardTree;
        cardtree_close_button.clicked += CloseCardTree;
        Upgrade_Button.clicked += OnUpgradeButtonClicked;
        Upgrade_Exit_Button.clicked += OnUpgradeExitButtonClicked;
        Back_button.clicked += OnBackButtonClicked;
        quit_button.clicked += quit_button_clicked;
        cancel_button.clicked += cancel_button_clicked;
        cost_refresh();
        AddClickEvents();
    }

    // Update is called once per frame
    void Update()
    {
        cost_refresh();
    }
	private void cost_refresh()
	{
		cost_left.text = GameDataManager.Inst.upgradeCost.ToString();
	}

    private void quit_button_clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
		GameDataManager.Inst.is_done = true;
        SceneManager.LoadScene("MainScene");
    }
    private void cancel_button_clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        checking_screen.style.display = DisplayStyle.None;
    }

    private void OnCardTree()
    {
        AudioManager.Instance.PlaySFX(bt);
        cardtree_screen.style.display = DisplayStyle.Flex;
    }
    private void CloseCardTree()
    {
        AudioManager.Instance.PlaySFX(bt);
        cardtree_screen.style.display = DisplayStyle.None;
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
    private void OnUpgradeButtonClicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        // 업그레이드 화면 열기
        UICardDataController script = GetComponent<UICardDataController>();
        script.CreateCardButtons();
        Upgrade_Screen.style.display = DisplayStyle.Flex;
    }

    private void OnUpgradeExitButtonClicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Upgrade_Screen.style.display = DisplayStyle.None;
    }

    private void OnBackButtonClicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        checking_screen.style.display = DisplayStyle.Flex;
        //SceneManager.LoadScene("MainScene");
        Debug.Log("Back button clicked. Returning to the main screen.");
    }
    private void AddClickEvents()
    {
        // Stage 1 버튼 이벤트
        stage1_1.clicked += Stage1_1Clicked;
        stage1_2.clicked += Stage1_2Clicked;
        stage1_3.clicked += Stage1_3Clicked;
        stage1_4.clicked += Stage1_4Clicked;

        // Stage 2 버튼 이벤트
        stage2_1.clicked += Stage2_1Clicked;
        stage2_2.clicked += Stage2_2Clicked;
        stage2_3.clicked += Stage2_3Clicked;
        stage2_4.clicked += Stage2_4Clicked;
        
        // Stage 2-2 버튼 이벤트
        stage22_1.clicked += Stage22_1Clicked;
        stage22_2.clicked += Stage22_2Clicked;
        stage22_3.clicked += Stage22_3Clicked;
        stage22_4.clicked += Stage22_4Clicked;

        // Stage 3 버튼 이벤트
        stage3_1.clicked += Stage3_1Clicked;
        stage3_2.clicked += Stage3_2Clicked;
        stage3_3.clicked += Stage3_3Clicked;
        stage3_4.clicked += Stage3_4Clicked;

        // Stage 3-2 버튼 이벤트
        stage32_1.clicked += Stage32_1Clicked;
        stage32_2.clicked += Stage32_2Clicked;
        stage32_3.clicked += Stage32_3Clicked;

        // Stage 4 버튼 이벤트
        stage4_1.clicked += Stage4_1Clicked;
        stage4_2.clicked += Stage4_2Clicked;
        stage4_3.clicked += Stage4_3Clicked;
        stage4_4.clicked += Stage4_4Clicked;
    }
    
    
    
    
    
    // Stage 1 버튼 핸들러
    private void Stage1_1Clicked() 
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 1-1 Button Clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("PT(Beta)");
        WaveManager.Inst.SetWaveCount(0);
    }

    private void Stage1_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 1-2 Button Clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("PT(Beta)");
        WaveManager.Inst.SetWaveCount(1);
    }

    private void Stage1_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        SceneManager.LoadScene("PT(Beta)");
        WaveManager.Inst.SetWaveCount(2);
        Debug.Log("Stage 1-3 Button Clicked");
    }

    private void Stage1_4Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 1-4 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(3);
        SceneManager.LoadScene("PT(Beta)");
    }

    // Stage 2 버튼 핸들러
    private void Stage2_1Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-1 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(4);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage2_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-2 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(5);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage2_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-3 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(6);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage2_4Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-4 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(7);
        SceneManager.LoadScene("PT(Beta)");
    }

    // Stage 2-2 버튼 핸들러
    private void Stage22_1Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-2-1 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(8);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage22_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-2-2 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(9);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage22_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-2-3 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(10);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage22_4Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 2-2-4 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(11);
        SceneManager.LoadScene("PT(Beta)");
    }

    // Stage 3 버튼 핸들러
    private void Stage3_1Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-1 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(12);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage3_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-2 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(13);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage3_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-3 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(14);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage3_4Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-4 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(15);
        SceneManager.LoadScene("PT(Beta)");
    }

    // Stage 3-2 버튼 핸들러
    private void Stage32_1Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-2-1 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(12);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage32_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-2-2 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(13);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage32_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 3-2-3 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(14);
        SceneManager.LoadScene("PT(Beta)");
    }

    // Stage 4 버튼 핸들러
    private void Stage4_1Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 4-1 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(16);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage4_2Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 4-2 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(17);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage4_3Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 4-3 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(18);
        SceneManager.LoadScene("PT(Beta)");
    }

    private void Stage4_4Clicked()
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log("Stage 4-4 Button Clicked");
        Time.timeScale = 1f;
        WaveManager.Inst.SetWaveCount(19);
        SceneManager.LoadScene("PT(Beta)");
    }

}
