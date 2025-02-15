using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public void SetInteractable(bool value)
    {
        // 스크립트나 컴포넌트 비활성화로 상호작용 제어
        enabled = value;  // 예: MonoBehaviour를 활성/비활성화
    }
}