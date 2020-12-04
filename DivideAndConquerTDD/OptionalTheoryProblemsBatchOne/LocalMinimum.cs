namespace DivideAndConquerTDD.OptionalTheoryProblemsBatchOne
{
    public record Dimension(int Top, int Bottom, int Left, int Right,
        int LastVIndex, int LastHIndex);

    public class LocalMinimum
    {
        public double GetLocalMinimum(int[][] input, Dimension range)
        {
            var hIndex = GetHIndex(input, range);
            if (hIndex == range.LastHIndex)
                return input[range.LastVIndex][range.LastHIndex];
            var vIndex= GetVIndex(input, range, hIndex);
            if (vIndex == range.LastVIndex)
                return input[range.LastVIndex][hIndex];
            range = ResetHorizontalBoundary(range, hIndex);
            range = ResetVerticalBoundary(range, vIndex);
            return GetLocalMinimum(input, range);
        }

        private static Dimension ResetVerticalBoundary(Dimension range, int vrtIndex)
        {
            var result = vrtIndex > range.LastVIndex
                ? range with {Top = range.LastVIndex + 1}
                : range with {Bottom = range.LastVIndex - 1};
            return result with{LastVIndex = vrtIndex};
        }

        private static Dimension ResetHorizontalBoundary(Dimension range, int hIndex)
        {
            if (range.LastHIndex <= 0)
                return range with{LastHIndex = hIndex};

            var result = hIndex > range.LastHIndex
                ? range with{Left = hIndex + 1}
                : range with{Right = hIndex - 1};
            return result with { LastHIndex = hIndex};
        }

        private int GetVIndex(int[][] input, Dimension range, int hIndex)
        {
            if (range.LastVIndex - 1 >= range.Top &&
                range.LastVIndex + 1 <= range.Bottom &&
                input[range.LastVIndex - 1][hIndex] > input[range.LastVIndex][hIndex] &&
                input[range.LastVIndex + 1][hIndex] > input[range.LastVIndex][hIndex])
            {
                return range.LastVIndex;
            }

            var vIndex = range.Top;
            for (int i = range.Top + 1; i <= range.Bottom; i++)
                vIndex = input[vIndex][hIndex] > input[i][hIndex] ? i : vIndex;
            return vIndex;
        }

        private int GetHIndex(int[][] input, Dimension range)
        {
            if (range.LastHIndex - 1 >= range.Left &&
                range.LastHIndex + 1 <= range.Bottom &&
                input[range.LastVIndex][range.LastHIndex - 1] >
                input[range.LastVIndex][range.LastHIndex] &&
                input[range.LastVIndex][range.LastHIndex + 1] >
                input[range.LastVIndex][range.LastHIndex]
            )
            {
                return range.LastHIndex;
            }

            var hIndex = range.Left;
            for (var i = hIndex + 1; i <= range.Right; i++)
                hIndex = input[range.LastVIndex][hIndex] > input[range.LastVIndex][i]
                    ? i
                    : hIndex;
            return hIndex;
        }
    }
}