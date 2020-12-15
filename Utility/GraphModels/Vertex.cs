using System;

namespace Utility.GraphModels
{
    public record Vertex(int Label, int Length) : IComparable<Vertex>
    {
        public int CompareTo(Vertex? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Length.CompareTo(other.Length);
        }
    };
}