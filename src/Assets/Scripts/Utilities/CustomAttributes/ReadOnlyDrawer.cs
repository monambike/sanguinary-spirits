// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom property drawer for displaying read-only properties in the Unity Editor.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// Gets the height of the property drawer.
    /// </summary>
    /// <param name="property">The property being drawn.</param>
    /// <param name="label">The label of the property.</param>
    /// <returns>The height of the property drawer.</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        => EditorGUI.GetPropertyHeight(property, label, true);

    /// <summary>
    /// Draws the property in the Unity Editor as read-only.
    /// </summary>
    /// <param name="position">The position to draw the property.</param>
    /// <param name="property">The property being drawn.</param>
    /// <param name="label">The label of the property.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Disable GUI for the property.
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
