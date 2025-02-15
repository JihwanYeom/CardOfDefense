using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ending_Controller : MonoBehaviour
{
	public AudioClip bt;
    //엔딩컨트롤러에 승리랑 패배시 경우를 나눠야함 창을 두개 만들어서 조건 분기 후 표시 되도록
    //승리시에는 선택할 수 있는 창을 띄운 다음에 게임매니저로 점수를 올려주고
    //패배시에는 다시 하기 버튼이랑 게임 나가기버튼 (나가기 버튼시에는 게임매니저 초기화랑 메인씬으로 가도록)
    public Transform camera;
    public VisualElement ending_screen;
    public VisualElement v_ending_screen;
    public VisualElement shadow_screen;
    public VisualElement ending_window;
    public VisualElement v_ending_window;
    public VisualElement vv_shadow_screen;
    public VisualElement vv_ending_window;
    public Button restart_button;
    public Button exit_button;
    private Button next_button;
    private Button vv_next_button;


    private VisualElement GetCard_Screen;
    public Button select_1_button;
    public Button select_2_button;
    public Button select_3_button;
    
    private VisualElement root;
    private int tmp = WaveManager.Inst.nowWaveCount;
    private bool checkFin = false;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>()?.rootVisualElement;
        
        ending_screen = root.Q<VisualElement>("Ending_Screen");
        v_ending_screen = root.Q<VisualElement>("V_Ending_Screen");
        shadow_screen = root.Q<VisualElement>("shadow_screen");
        ending_window = root.Q<VisualElement>("Ending_Window");
        v_ending_window = root.Q<VisualElement>("v_Ending_Window");
        vv_shadow_screen = root.Q<VisualElement>("vv_shadow_screen");
        vv_ending_window = root.Q<VisualElement>("vv_Ending_Window");
        GetCard_Screen = root.Q<VisualElement>("Get_Card_Screen");

        restart_button = root.Q<Button>("End_Restart_Button");
        exit_button = root.Q<Button>("End_Exit_Button");
        next_button = root.Q<Button>("Next_Button");
        vv_next_button = root.Q<Button>("vv_Next_Button");
        
        select_1_button = root.Q<Button>("select_1_button");
        select_2_button = root.Q<Button>("select_2_button");
        select_3_button = root.Q<Button>("select_3_button");

        ending_screen.style.display = DisplayStyle.None;
        v_ending_screen.style.display = DisplayStyle.None;
        vv_shadow_screen.style.display = DisplayStyle.None;
        GetCard_Screen.style.display = DisplayStyle.None;
        select_3_button.style.display = DisplayStyle.None;
        ending_window.style.display = DisplayStyle.None;
        v_ending_window.style.display = DisplayStyle.None;
        vv_ending_window.style.display = DisplayStyle.None;
        if (tmp == 3 || tmp == 7 || tmp == 11 || tmp == 15 )
        {
            select_3_button.style.display = DisplayStyle.Flex;
        }
        
        restart_button.clicked += OnRestartButtonClicked;
        exit_button.clicked += OnExitButtonClicked;
        
        next_button.clicked += OnNextButtonClicked;
        vv_next_button.clicked += OnExitButtonClicked;
        select_1_button.clicked += select_1;
        select_2_button.clicked += select_2;
        select_3_button.clicked += select_3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            StartCoroutine(Start_v_Ending_Screen());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(Start_Ending_Screen());
        }

        if (EnemySpawnManager.Inst.check==1&&checkFin==false)
        {
            StartCoroutine(Start_v_Ending_Screen());
            checkFin=true;
        }

        if (EnemySpawnManager.Inst.check==-1&&checkFin==false)
        {
            StartCoroutine(Start_Ending_Screen());
            checkFin=true;
        }
    }

    IEnumerator Start_Ending_Screen()
    {
        Time.timeScale = 0.2f;
        GameUIController script = GetComponent<GameUIController>();
        script.is_left = true;
        script.speed = 80;
        ending_screen.style.display = DisplayStyle.Flex;
        ending_window.style.display = DisplayStyle.None;

        // 서서히 게임을 멈추게 하기 (Time.timeScale을 0.2로 줄이면서)
        float waitTime = 2f; // 1초 대기
        float elapsedTime = 0f;

        // 시간을 기다리며 다른 작업을 처리
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.unscaledDeltaTime; // Time.unscaledDeltaTime를 사용하여 시간 흐름에 구애받지 않음
            
            yield return null; // 한 프레임 대기
        }

        // 대기 후 'Ending_Window'를 보여주기
        ending_window.style.display = DisplayStyle.Flex;

        // 필요한 추가적인 UI 업데이트가 있다면 여기서 추가할 수 있습니다.
    }
    IEnumerator Start_v_Ending_Screen()
    {
        if (tmp != 19)
        {
            Time.timeScale = 0.2f;
            GameUIController script = GetComponent<GameUIController>();
            script.is_right = true;
            script.speed = 80;
            v_ending_screen.style.display = DisplayStyle.Flex;
            v_ending_window.style.display = DisplayStyle.None;


            // 서서히 게임을 멈추게 하기 (Time.timeScale을 0.2로 줄이면서)
            float waitTime = 2f; // 1초 대기
            float elapsedTime = 0f;

            // 시간을 기다리며 다른 작업을 처리
            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.unscaledDeltaTime; // Time.unscaledDeltaTime를 사용하여 시간 흐름에 구애받지 않음

                yield return null; // 한 프레임 대기
            }

            // 대기 후 'Ending_Window'를 보여주기
            v_ending_window.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.Log("19레벨 조건 만족 - vv_shadow_screen 활성화");
            Time.timeScale = 0.2f;
            GameUIController script = GetComponent<GameUIController>();
            script.is_right = true;
            script.speed = 80;
            v_ending_screen.style.display = DisplayStyle.Flex;
            vv_shadow_screen.style.display = DisplayStyle.Flex;
            vv_ending_window.style.display = DisplayStyle.None;

            float waitTime = 2f; 
            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            Debug.Log("대기 완료 - vv_ending_window 활성화");
            vv_ending_window.style.display = DisplayStyle.Flex;
        }
        // 필요한 추가적인 UI 업데이트가 있다면 여기서 추가할 수 있습니다.
    }

    private void OnExitButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        GameDataManager.Inst.is_done = true;
        SceneManager.LoadScene("MainScene");
    }

    private void OnRestartButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        SceneManager.LoadScene("PT(BETA)");
    }

    
    private void OnNextButtonClicked()
    {
		AudioManager.Instance.PlaySFX(bt);
        v_ending_screen.style.display = DisplayStyle.None;
        GetCard_Screen.style.display = DisplayStyle.Flex;
    }

    public void select_1()
    {
		AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        //게임매니저에 업데이트 해야하는 부분
        GameDataManager.Inst.upgradeCost+=2;
        SceneManager.LoadScene("MapScene");
    }
    public void select_2()
    {
		AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        //게임매니저에 업데이트 해야하는 부분
        GameDataManager.Inst.totalCardCost++;
        SceneManager.LoadScene("MapScene");
    }
    public void select_3()
    {
		AudioManager.Instance.PlaySFX(bt);
        Time.timeScale = 1f;
        //게임매니저에 업데이트 해야하는 부분
        GameDataManager.Inst.drawNum++;
        SceneManager.LoadScene("MapScene");
    }
}
