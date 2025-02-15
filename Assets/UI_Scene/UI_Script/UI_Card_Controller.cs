using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Card_Controller : MonoBehaviour // 더미관련 
{
	public AudioClip bt;
	public VisualTreeAsset buttonTemplate; // UI Toolkit 카드 버튼 템플릿

	private Button Deck_Button;
	private Button Dummy_Button;
	private VisualElement Deck_Screen;
	private VisualElement Dummy_Screen;
	private VisualElement Deck_Cards;
	private VisualElement Dummy_Cards;
	private Button Deck_Close_Button;
	private Button Dummy_Close_Button;
	
	
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        Deck_Button = root.Q<Button>("Deck_Button");
        Dummy_Button = root.Q<Button>("Dummy_Button");
        Deck_Screen = root.Q<VisualElement>("Deck_Screen");
        Dummy_Screen = root.Q<VisualElement>("Dummy_Screen");
        Deck_Cards = root.Q<VisualElement>("deck_cards");
        Dummy_Cards = root.Q<VisualElement>("dummy_cards");
        Deck_Close_Button = root.Q<Button>("Close_Deck_Button");
        Dummy_Close_Button = root.Q<Button>("Close_Dummy_Button");
        
        Deck_Cards.style.flexDirection = FlexDirection.Row;
        Deck_Cards.style.flexWrap = Wrap.Wrap;
        Dummy_Cards.style.flexDirection = FlexDirection.Row;
        Dummy_Cards.style.flexWrap = Wrap.Wrap;
        
        
        Deck_Screen.style.display = DisplayStyle.None;
        Dummy_Screen.style.display = DisplayStyle.None;
        
        
        Deck_Button.clickable.clicked += Deck_Button_Click;
        Dummy_Button.clickable.clicked += Dummy_Button_Click;
        Deck_Close_Button.clickable.clicked += Deck_Close_Button_Click;
        Dummy_Close_Button.clickable.clicked += Dummy_Close_Button_Click;
    }
	private void DeckScreenInit()
	{
		Deck_Cards.Clear();
		foreach (var item in CardManager.Inst.myDeck)
		{
			//Debug.Log(item.name":name");
			
			var button = buttonTemplate.CloneTree();
           

			// 버튼 배경에 아이템 스프라이트 설정
			button.style.backgroundImage = new StyleBackground(item.sprite);
			Deck_Cards.Add(button);
		}


	}
    private void DummyScreenInit()
    {
	    Dummy_Cards.Clear();
	    foreach (var item in CardManager.Inst.usedCards)
	    {
		    //Debug.Log(item.name":name");
			
		    var button = buttonTemplate.CloneTree();
           

		    // 버튼 배경에 아이템 스프라이트 설정
		    button.style.backgroundImage = new StyleBackground(item.sprite);
		    Dummy_Cards.Add(button);
	    }
    }

    private void Deck_Button_Click()
    {
	    AudioManager.Instance.PlaySFX(bt);
	    Time.timeScale = 0f;
	    DeckScreenInit();
	    Deck_Screen.style.display = DisplayStyle.Flex;
    }

    private void Dummy_Button_Click()
    {
	    AudioManager.Instance.PlaySFX(bt);
	    Time.timeScale = 0f;
	    DummyScreenInit();
	    Dummy_Screen.style.display = DisplayStyle.Flex;
    }

    private void Deck_Close_Button_Click()
    {
	    AudioManager.Instance.PlaySFX(bt);
	    Time.timeScale = 1f;
	    Deck_Screen.style.display = DisplayStyle.None;
    }

    private void Dummy_Close_Button_Click()
    {
	    AudioManager.Instance.PlaySFX(bt);
	    Time.timeScale = 1f;
	    Dummy_Screen.style.display = DisplayStyle.None;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
