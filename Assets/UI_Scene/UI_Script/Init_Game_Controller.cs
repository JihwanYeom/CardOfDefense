using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Init_Game_Controller : MonoBehaviour
{
	public AudioClip bt;
    private VisualElement init_screen;
	private Button select_1_button;
    private Button select_2_button;
    private Button select_3_button;
    
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        init_screen = root.Q<VisualElement>("Init_Screen");
        select_1_button = root.Q<Button>("select_1_button");
        select_2_button = root.Q<Button>("select_2_button");
        select_3_button = root.Q<Button>("select_3_button");

        select_1_button.clicked += select_1_button_OnClick;
        select_2_button.clicked += select_2_button_OnClick;
        select_3_button.clicked += select_3_button_OnClick;
        
        
        if (GameDataManager.Inst.is_done)
        {
            GameDataManager.Inst.is_done = false;
            init_screen.style.display = DisplayStyle.Flex;
        }
        else
        {
            init_screen.style.display = DisplayStyle.None;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    //기본 값이 업그레이드는 0 별 3개 드로우 4장
    private void select_1_button_OnClick()
    {
		AudioManager.Instance.PlaySFX(bt);
        GameDataManager.Inst.totalCardCost = 5;
        GameDataManager.Inst.drawNum = 4;
        GameDataManager.Inst.upgradeCost = 2;
        init_screen.style.display = DisplayStyle.None;
    }
    private void select_2_button_OnClick()//업그레이드 추가
    {
		AudioManager.Instance.PlaySFX(bt);
        GameDataManager.Inst.totalCardCost = 6;
        GameDataManager.Inst.drawNum = 4;
        GameDataManager.Inst.upgradeCost = 0;
        init_screen.style.display = DisplayStyle.None;
    }
    private void select_3_button_OnClick()//총 비용 추가
    {
		AudioManager.Instance.PlaySFX(bt);
        GameDataManager.Inst.totalCardCost = 5;
        GameDataManager.Inst.drawNum = 5;
        GameDataManager.Inst.upgradeCost = 0;
        init_screen.style.display = DisplayStyle.None;
    }
}
