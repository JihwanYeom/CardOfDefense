using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
public class TypeSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // UnityEngine.Object�� ��ӹ޴� ��� Ÿ�� ��������
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(UnityEngine.Object).IsAssignableFrom(type)) // Unity Object�� ���͸�
            .Select(type => type.FullName) // ��ü �̸� (���ӽ����̽� ����)
            .ToArray();

        // ���� ��
        int currentIndex = Array.IndexOf(types, property.stringValue);
        if (currentIndex == -1) currentIndex = 0; // �⺻�� ó��

        // ��Ӵٿ� ǥ��
        int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, types);

        // ���� ����Ǿ����� ����
        if (selectedIndex >= 0 && selectedIndex < types.Length)
        {
            property.stringValue = types[selectedIndex];
        }
    }
}
