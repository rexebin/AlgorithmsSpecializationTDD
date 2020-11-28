namespace DivideAndConquerTDD
{
    public record Dimension(int Top, int Bottom, int Left, int Right, int VCut, int lastHIndex);

    public class LocalMinimum
    {
        public double GetLocalMinimum(int[][] input, Dimension range)
        {
            var hIndex = GetHIndex(input, range);
            var vIndex = GetVIndex(input, range, hIndex);
            if (vIndex == range.VCut)
                return input[range.VCut][hIndex];
            range = ResetHorizontalBoundary(range, hIndex);
            range = ResetVerticalBoundary(range, vIndex);
            return GetLocalMinimum(input, range);
        }

        private static Dimension ResetVerticalBoundary(Dimension range, int vIndex)
        {
            var result = vIndex > range.VCut ? range with {Top = range.VCut + 1} : range with {Bottom = range.VCut - 1};
            return result with{VCut = vIndex};
        }

        private static Dimension ResetHorizontalBoundary(Dimension range, int hIndex)
        {
            if (range.lastHIndex <= 0)
                return range with{lastHIndex = hIndex};

            var result = hIndex > range.lastHIndex ? range with{Left = hIndex + 1} : range with{Right = hIndex - 1};
            return result with { lastHIndex = hIndex};
        }

        private int GetVIndex(int[][] input, Dimension range, int hIndex)
        {
            var vIndex = range.Top;
            for (int i = range.Top + 1; i <= range.Bottom; i++)
                vIndex = input[vIndex][hIndex] > input[i][hIndex] ? i : vIndex;
            return vIndex;
        }

        private int GetHIndex(int[][] input, Dimension range)
        {
            var hIndex = range.Left;
            for (var i = hIndex + 1; i <= range.Right; i++)
                hIndex = input[range.VCut][hIndex] > input[range.VCut][i] ? i : hIndex;
            return hIndex;
        }
    }
}