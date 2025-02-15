using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
public class TypeSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // UnityEngine.Object를 상속받는 모든 타입 가져오기
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(UnityEngine.Object).IsAssignableFrom(type)) // Unity Object만 필터링
            .Select(type => type.FullName) // 전체 이름 (네임스페이스 포함)
            .ToArray();

        // 현재 값
        int currentIndex = Array.IndexOf(types, property.stringValue);
        if (currentIndex == -1) currentIndex = 0; // 기본값 처리

        // 드롭다운 표시
        int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, types);

        // 값이 변경되었으면 저장
        if (selectedIndex >= 0 && selectedIndex < types.Length)
        {
            property.stringValue = types[selectedIndex];
        }
    }
}
