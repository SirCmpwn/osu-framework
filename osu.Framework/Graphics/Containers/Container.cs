// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Lists;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using OpenTK;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.OpenGL;
using OpenTK.Graphics;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.Colour;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Transformations;
using osu.Framework.Timing;
using System.Linq;

namespace osu.Framework.Graphics.Containers
{
    public class Container : Container<Drawable>
    { }

    /// <summary>
    /// A control which can have children added externally.
    /// </summary>
    public partial class Container<T> : Control<T>, IContainerEnumerable<T>, IContainerCollection<T>
        where T : Drawable
    {
        public new IEnumerable<T> AliveChildren => base.AliveChildren;
    
        protected virtual Container<T> Content => this;
    
        public virtual new IEnumerable<T> Children
        {
            get { return Content != this ? Content.Children : base.Children; }
            set
            {
                Clear();
                Add(value);
            }
        }

        public IEnumerable<T> InternalChildren
        {
            get { return Children; }
            set
            {
                Clear();
                base.Add(value);
            }
        }

        public Container(LifetimeList<T> lifetimeList = null) : base(lifetimeList)
        {
        }

        /// <summary>
        /// Add a Drawable to Content's children list, recursing until Content == this.
        /// </summary>
        /// <param name="drawable">The drawable to be added.</param>
        public new virtual void Add(T drawable)
        {
            Debug.Assert(drawable != null, "null-Drawables may not be added to Containers.");
            Debug.Assert(Content != drawable, "Content may not be added to itself.");

            if (Content == this)
                base.Add(drawable);
            else
                Content.Add(drawable);
        }

        /// <summary>
        /// Add a collection of Drawables to Content's children list, recursing until Content == this.
        /// </summary>
        /// <param name="collection">The collection of drawables to be added.</param>
        public new virtual void Add(IEnumerable<T> collection)
        {
            foreach (T d in collection)
                Add(d);
        }

        protected void AddInternal(T drawable) => base.Add(drawable);

        protected void AddInternal(IEnumerable<T> collection) => base.Add(collection);

        public new virtual bool Remove(T drawable)
        {
            if (drawable == null)
                return false;
            if (Content != this)
                return Content.Remove(drawable);
            return base.Remove(drawable);
        }

        public new virtual int RemoveAll(Predicate<T> match)
        {
            List<T> toRemove = Children.Where(c => match(c)).ToList();
            foreach (T removable in toRemove)
                Remove(removable);
            return toRemove.Count;
        }

        public new virtual void Remove(IEnumerable<T> range)
        {
            if (range == null)
                return;
            foreach (T p in range)
                Remove(p);
        }

        public new virtual void Clear(bool dispose = true) => base.Clear(dispose);

        public new virtual void InvalidateFromChild(Invalidation invalidation, IDrawable source) =>
            base.InvalidateFromChild(invalidation, source);

        public new int IndexOf(T drawable) => base.IndexOf(drawable);

        public new bool Contains(T drawable) => base.Contains(drawable);
    }
}
