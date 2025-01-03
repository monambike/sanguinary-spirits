// Copyright(c) 2024 Vinicius Gabriel Marques de Melo (@monambike). All rights reserved.
// Contact: contact@monambike.com for more information.
// For license information, please see the LICENSE file in the root directory.

/// <summary>
/// Represents a scene with a name.
/// </summary>
public class Scene
{
    /// <summary>
    /// Gets the name of the scene.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Scene"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the scene.</param>
    public Scene(string name) => Name = name;

    /// <summary>
    /// Returns the name of the scene.
    /// </summary>
    /// <returns>A string representing the name of the scene.</returns>
    public override string ToString() => Name;
}
