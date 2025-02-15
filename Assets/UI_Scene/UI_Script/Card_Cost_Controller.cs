using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Card_Cost_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    //게임매니저에서 총 카드 비용정보를 들고 와서 비용을 관리 하는 
    public Label full_cost;
    public Label cost_left;
    private VisualElement cost_box;
    
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        full_cost = root.Q<Label>("right_slash");
        cost_left = root.Q<Label>("left_slash");
        cost_box = root.Q<VisualElement>("cost_box");
        init_cost();
        cost_box_sizing();
    }

    // Update is called once per frame
    void Update()
    {
        init_cost();
        cost_box_sizing();
    }

    public void left_cost_update()
    {
        //cost_left.text = 이 부분에 카드매니저에서 사용하는 부분 연결후 카드 사용시에 함수 작동
    }
    private void init_cost()
    {
        full_cost.text = GameDataManager.Inst.totalCardCost.ToString();
        cost_left.text = CardManager.Inst.nowCost.ToString();
    }

    private void cost_box_sizing()
    {
        int len1 = full_cost.text.Length;
        int len2 = cost_left.text.Length;
        if (len1 + len2 == 2)
        {
            cost_box.style.width = 300;
        }
        else if (len1 + len2 == 3)
        {
            cost_box.style.width = 345;
        }
        else if (len1 + len2 == 4)
        {
            cost_box.style.width = 396;
        }
    }
}
