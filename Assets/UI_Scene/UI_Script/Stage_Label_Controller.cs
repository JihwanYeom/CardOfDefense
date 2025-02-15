using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage_Label_Controller : MonoBehaviour
{
    // 맵 뷰에서 스테이지 호출 할때 그 숫자를 받아서 케이스 판단 후 스테이지 출력한다음 씬이 시작 했을때 일정 시간 보이고 사라지는 스크립트 작성
    [SerializeField] private float displayDuration = 2.0f; // UI 표시 시간
    [SerializeField] private float fadeDuration = 1.0f; // 페이드 효과 시간

    public Label stage_label;
    private VisualElement stage_screen;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        stage_label = root.Q<Label>("stage_n");
        stage_screen = root.Q<VisualElement>("stage_screen");
        Set_Stage_Label();
        if (stage_screen != null)
        {
            // Label 초기화 (보이지 않게 설정)
            stage_screen.style.opacity = 0;
            // 표시 코루틴 실행
            StartCoroutine(ShowStageIntro());
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator ShowStageIntro()
    {
        // UI 보이기
        stage_screen.style.opacity = 1;

        // 지정된 시간 동안 유지
        yield return new WaitForSeconds(displayDuration);

        // 페이드 아웃
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            stage_screen.style.opacity = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        // UI 완전히 숨기기
        stage_screen.style.opacity = 0;
    }
    public void Set_Stage_Label()
    {
		
        if (WaveManager.Inst.nowWaveCount == 0)
        {
            stage_label.text = "1-1";
        }
        else if (WaveManager.Inst.nowWaveCount == 1)
        {
            stage_label.text = "1-2";
        }
        else if (WaveManager.Inst.nowWaveCount == 2)
        {
            stage_label.text = "1-3";
        }
        else if (WaveManager.Inst.nowWaveCount == 3)
        {
            stage_label.text = "1-4";
        }
        else if (WaveManager.Inst.nowWaveCount == 4)
        {
            stage_label.text = "2-1";
        }
        else if (WaveManager.Inst.nowWaveCount == 5)
        {
            stage_label.text = "2-2";
        }
        else if (WaveManager.Inst.nowWaveCount == 6)
        {
            stage_label.text = "2-3";
        }
        else if (WaveManager.Inst.nowWaveCount == 7)
        {
            stage_label.text = "2-4";
        }
        else if (WaveManager.Inst.nowWaveCount == 8)
        {
            stage_label.text = "2-1";
        }
        else if (WaveManager.Inst.nowWaveCount == 9)
        {
            stage_label.text = "2-2";
        }
        else if (WaveManager.Inst.nowWaveCount == 10)
        {
            stage_label.text = "2-3";
        }
        else if (WaveManager.Inst.nowWaveCount == 11)
        {
            stage_label.text = "2-4";
        }
        else if (WaveManager.Inst.nowWaveCount == 12)
        {
            stage_label.text = "3-1";
        }
        else if (WaveManager.Inst.nowWaveCount == 13)
        {
            stage_label.text = "3-2";
        }
        else if (WaveManager.Inst.nowWaveCount == 14)
        {
            stage_label.text = "3-3";
        }
        else if (WaveManager.Inst.nowWaveCount == 15)
        {
            stage_label.text = "3-4";
        }else if (WaveManager.Inst.nowWaveCount == 16)
        {
            stage_label.text = "4-1";
        }else if (WaveManager.Inst.nowWaveCount == 17)
        {
            stage_label.text = "4-2";
        }else if (WaveManager.Inst.nowWaveCount == 18)
        {
            stage_label.text = "4-3";
        }
        else if (WaveManager.Inst.nowWaveCount == 19)
        {
            stage_label.text = "4-4";
        }
    }
}
