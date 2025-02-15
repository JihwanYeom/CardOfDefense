using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICardDataController : MonoBehaviour
{
    public AudioClip bt;
    [SerializeField] ItemSO DeckSO; // DeckSO 연결
    
    public VisualTreeAsset buttonTemplate; // UI Toolkit 카드 버튼 템플릿
    public VisualTreeAsset skill_window; // 스킬 담을 창
    public VisualTreeAsset Card_Container; // 업그레이드 루트 담을 창

	private int flag = 0;	

    private void Start()
    {
        // 덱의 카드 리스트로 UI 생성
        if (DeckSO == null)
        {
            Debug.LogError("DeckSO가 할당되지 않았습니다. 인스펙터에서 DeckSO를 할당해 주세요.");
            return;
        }

        //CreateCardButtons();
    }

    /// <summary>
    /// 덱 카드 정보를 기반으로 버튼 생성
    /// </summary>
    public void CreateCardButtons()
    {
        // UI 루트 가져오기
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("UI Document의 rootVisualElement가 null입니다. UI Document가 제대로 연결되었는지 확인해주세요.");
            return;
        }

        // VisualElement: "1_star_cards" 가져오기
        var cardContainer_1 = root.Q<VisualElement>("1_star_cards");
        var cardContainer_2 = root.Q<VisualElement>("2_star_cards");
        var cardContainer_3 = root.Q<VisualElement>("3_star_cards");
        var cardContainer_4 = root.Q<VisualElement>("4_star_cards");
        
        

        // 카드들이 가로로 배치되도록 스타일 설정
        cardContainer_1.style.flexDirection = FlexDirection.Row;
        cardContainer_1.style.flexWrap = Wrap.Wrap;
        cardContainer_2.style.flexDirection = FlexDirection.Row;
        cardContainer_2.style.flexWrap = Wrap.Wrap;
        cardContainer_3.style.flexDirection = FlexDirection.Row;
        cardContainer_3.style.flexWrap = Wrap.Wrap;
        cardContainer_4.style.flexDirection = FlexDirection.Row;
        cardContainer_4.style.flexWrap = Wrap.Wrap;
        
        
        cardContainer_1.Clear(); // 기존 UI 제거
        cardContainer_2.Clear(); // 기존 UI 제거
        cardContainer_3.Clear(); // 기존 UI 제거
        cardContainer_4.Clear(); // 기존 UI 제거
        
        
        
        
        
        
        // DeckSO의 아이템 순회
        if (DeckSO == null || DeckSO.items == null)
        {
            Debug.LogError("DeckSO 또는 items 배열이 null입니다. DeckSO를 확인해주세요.");
            return;
        }
    
        foreach (var item in DeckSO.items)
        {
            if (item == null) continue;
            
            // 버튼 템플릿 클론
            var button = buttonTemplate.CloneTree();
            

            // 아이템의 스프라이트가 null일 경우 기본 스프라이트 사용
           

            // 버튼 배경에 아이템 스프라이트 설정
            button.style.backgroundImage = new StyleBackground(item.sprite);
           
            // 클릭 이벤트 등록
			if(item.upgrade!=null){
			
            button.RegisterCallback<ClickEvent>(evt => {
				if(flag==0)
					OnCardButtonClick(item);
			});
		
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
               //button.style.transform = new StyleTranslate(new Translate(0, 0, 0));
                button.style.scale = new StyleScale(new Vector3(1.05f, 1.05f, 1.05f));
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.style.scale = new StyleScale(new Vector3(1f, 1f, 1f));
            });
			}
            

            // 카드 컨테이너에 버튼 추가
            if (item.level == 1)
            {
                cardContainer_1.Add(button);
            }
            else if (item.level == 2)
            {
                cardContainer_2.Add(button);
            }
            else if (item.level == 3)
            {
                cardContainer_3.Add(button);
            }
            else if (item.level == 4)
            {
                cardContainer_4.Add(button);
            }
        }
    }
    
    /// <summary>
    /// 카드 버튼 클릭 이벤트 처리
    /// </summary>
    /// <param name="item">클릭된 카드</param>
    private void OnCardButtonClick(Item item)
    {
        AudioManager.Instance.PlaySFX(bt);
        Debug.Log($"Clicked Card: {item.name}, Level: {item.level} {item.upgrade}{item.upgrade2}{item.upgrade3}{item.upgrade4}");
        flag = 1;
		upgrade_route(item);
        // 여기에서 선택된 카드의 업그레이드 UI를 호출하거나 다른 로직을 추가
    }

    private void upgrade_route(Item item)
    {
        var card_container_root = Card_Container.CloneTree();
        var card_container_box = card_container_root.Q<VisualElement>("screen");
        var card_container = card_container_root.Q<VisualElement>("card_container");
        var card_container_close_button = card_container_root.Q<Button>("card_container_close_button");

        card_container.style.flexDirection = FlexDirection.Row;
        card_container.style.flexWrap = Wrap.Wrap;
        if (item.level == 1)
        {
            Debug.Log("1");
            card_container.style.width = 1440;
            card_container.style.height = 400;
            //card_container_box.style.width = 1440;
            //card_container_box.style.height = 400;
        }
        else if (item.level == 2)
        {
            Debug.Log("2");
            card_container.style.width = 960;
            card_container.style.height = 400;
            //card_container_box.style.width = 960;
            //card_container_box.style.height = 400;
        }
        else
        {
            Debug.Log("3");
            card_container.style.width = 480;
            card_container.style.height = 400;
            //card_container_box.style.width = 480;
           //card_container_box.style.height = 400;
        }
        
        
        Vector2 mousePosition = Input.mousePosition;

        // Y축 뒤집기 (UI Toolkit 좌표계 변환)
        var root = GetComponent<UIDocument>().rootVisualElement;
        var screenHeight = root.resolvedStyle.height;
        float adjustedY = screenHeight - mousePosition.y;

        // 창 위치 설정
        card_container_root.style.position = Position.Absolute;
        card_container_root.style.left = mousePosition.x;
        card_container_root.style.top = adjustedY;
        
		
        if (item.upgrade != null)
        {
            
            var button = buttonTemplate.CloneTree();
            button.style.backgroundImage = new StyleBackground(item.upgrade.sprite);
            button.RegisterCallback<ClickEvent> (evt =>
            {
                AudioManager.Instance.PlaySFX(bt);
                //UpgradeManager sc = GetComponent<UpgradeManager>();
                //sc.Upgrade(item, item.upgrade);
                UpgradeManager.Inst.Upgrade(item, item.upgrade);
                GameUIController scipt = GetComponent<GameUIController>();
                //card_container_root.RemoveFromHierarchy();
                
				CreateCardButtons();

				//scipt.ToggleUpgradeScreen();
                //scipt.ToggleUpgradeScreen();
                
				

				
				card_container_root.RemoveFromHierarchy();
				flag = 0;
            });
            //button.RegisterCallback<ClickEvent>(evt => OnCardButtonClick(item)); // 업그레이드 호출로 바꿔야 하는 부분
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                //button.style.transform = new StyleTranslate(new Translate(0, 0, 0));
                button.style.scale = new StyleScale(new Vector3(1.05f, 1.05f, 1.05f));
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.style.scale = new StyleScale(new Vector3(1f, 1f, 1f));
            });
            card_container.Add(button);
        }
        if (item.upgrade2 != null)
        {
           
            var button = buttonTemplate.CloneTree();
            button.style.backgroundImage = new StyleBackground(item.upgrade2.sprite);
            button.RegisterCallback<ClickEvent> (evt =>
            {
                AudioManager.Instance.PlaySFX(bt);
                //UpgradeManager sc = GetComponent<UpgradeManager>();
                //sc.Upgrade(item, item.upgrade2);
                UpgradeManager.Inst.Upgrade(item, item.upgrade2);
                GameUIController scipt = GetComponent<GameUIController>();
                //card_container_root.RemoveFromHierarchy();

				CreateCardButtons();

                //scipt.ToggleUpgradeScreen();
                //scipt.ToggleUpgradeScreen();



                card_container_root.RemoveFromHierarchy();
				flag = 0;
            });
            //button.RegisterCallback<ClickEvent>(evt => OnCardButtonClick(item)); // 업그레이드 호출로 바꿔야 하는 부분
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                //button.style.transform = new StyleTranslate(new Translate(0, 0, 0));
                button.style.scale = new StyleScale(new Vector3(1.05f, 1.05f, 1.05f));
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.style.scale = new StyleScale(new Vector3(1f, 1f, 1f));
            });
            card_container.Add(button);
        }
        if (item.upgrade3 != null)
        {
       
            var button = buttonTemplate.CloneTree();
            button.style.backgroundImage = new StyleBackground(item.upgrade3.sprite);
            button.RegisterCallback<ClickEvent> (evt =>
            {
                AudioManager.Instance.PlaySFX(bt);
                //UpgradeManager sc = GetComponent<UpgradeManager>();
                //sc.Upgrade(item, item.upgrade3);
                UpgradeManager.Inst.Upgrade(item, item.upgrade3);
                //card_container_root.RemoveFromHierarchy();
                GameUIController scipt = GetComponent<GameUIController>();
                
				CreateCardButtons();


				//scipt.ToggleUpgradeScreen();
                //scipt.ToggleUpgradeScreen();
                



				card_container_root.RemoveFromHierarchy();
				flag = 0;
            });
            //button.RegisterCallback<ClickEvent>(evt => OnCardButtonClick(item)); // 업그레이드 호출로 바꿔야 하는 부분
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                //button.style.transform = new StyleTranslate(new Translate(0, 0, 0));
                button.style.scale = new StyleScale(new Vector3(1.05f, 1.05f, 1.05f));
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.style.scale = new StyleScale(new Vector3(1f, 1f, 1f));
            });
            card_container.Add(button);
        }
        if (item.upgrade4 != null)
        {
           
            var button = buttonTemplate.CloneTree();
            button.style.backgroundImage = new StyleBackground(item.upgrade4.sprite);
            button.RegisterCallback<ClickEvent> (evt =>
            {
                AudioManager.Instance.PlaySFX(bt);
                //UpgradeManager sc = GetComponent<UpgradeManager>();
                //sc.Upgrade(item, item.upgrade4);
                UpgradeManager.Inst.Upgrade(item, item.upgrade4);
                GameUIController scipt = GetComponent<GameUIController>();
                //card_container_root.RemoveFromHierarchy();
                //scipt.ToggleUpgradeScreen();
                //scipt.ToggleUpgradeScreen();
                card_container_root.RemoveFromHierarchy();
				flag = 0;
            });
           // button.RegisterCallback<ClickEvent>(evt => OnCardButtonClick(item)); // 업그레이드 호출로 바꿔야 하는 부분
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                //button.style.transform = new StyleTranslate(new Translate(0, 0, 0));
                button.style.scale = new StyleScale(new Vector3(1.05f, 1.05f, 1.05f));
            });

            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                button.style.scale = new StyleScale(new Vector3(1f, 1f, 1f));
            });
            card_container.Add(button);
        }
        
        card_container_close_button.clicked += () =>
        {
            AudioManager.Instance.PlaySFX(bt);
            card_container_root.RemoveFromHierarchy();
			flag = 0;
        };
        root.Add(card_container_root);
    }
}
