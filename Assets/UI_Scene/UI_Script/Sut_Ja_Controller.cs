using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sut_Ja_Controller : MonoBehaviour
{

    private Label card_cost;
    private Label draw_n;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>()?.rootVisualElement;
        card_cost = root.Q<Label>("card_cost");
        draw_n = root.Q<Label>("draw_n");
        update_card_cost();
        update_draw_n();
    }
    
    // Update is called once per frame
    void Update()
    {
        update_card_cost();
        update_draw_n();
    }

    private void update_card_cost()
    {
        card_cost.text = GameDataManager.Inst.totalCardCost.ToString();
    }
    private void update_draw_n()
    {
        draw_n.text = GameDataManager.Inst.drawNum.ToString();
    }
}
