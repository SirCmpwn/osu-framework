using System;
using OpenTK;

namespace osu.Framework.Graphics.Containers
{
    public interface IControl : IDrawable
    {
        Vector2 ChildSize { get; }
        Vector2 ChildOffset { get; }
    }
}