using UnityEngine;

public class spawnSquare : MonoBehaviour
{
    public GameObject shapePrefab; // 생성할 도형 프리팹
    private bool isDragging = false; // 드래그 중인지 확인
    private Vector3 spawnPosition; // 도형이 생성될 위치

    void FixedUpdate()
    {
        // 드래그 중일 때 마우스 위치 업데이트
        if (isDragging)
        {
            spawnPosition = GetMouseWorldPosition();
            spawnPosition.y = 0; // Y 좌표를 0으로 고정
        }

        // 마우스 왼쪽 버튼을 떼면 드래그 종료 및 도형 생성
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            if (GetMouseWorldPosition().y > 0)
                Instantiate(shapePrefab, spawnPosition, Quaternion.identity); // 지정된 위치에 도형 생성
        }
    }

    private void OnMouseDown()
    {
        // 객체가 클릭되면 드래그 시작
        isDragging = true;
        spawnPosition = GetMouseWorldPosition();
        spawnPosition.y = 0; // Y 좌표를 0으로 고정
    }

    private void OnMouseUp()
    {
        // 마우스 버튼을 떼면 드래그 종료
        if (isDragging)
        {
            isDragging = false;
            Instantiate(shapePrefab, spawnPosition, Quaternion.identity); // 지정된 위치에 도형 생성
        }
    }

    // 마우스 위치를 월드 좌표로 변환하는 메서드
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); // Z 거리 설정
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}