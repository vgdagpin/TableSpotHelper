namespace TableSpotHelper
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var table = new Table(6, 6);

            table.PrintTable();

            do
            {
                var width = 0;
                #region Width Input
                Console.Write("Write block width: ");
                var blockWidth = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(blockWidth))
                {
                    break;
                }
                else if (!int.TryParse(blockWidth, out width))
                {
                    continue;
                }
                #endregion

                var height = 0;
                #region Height Input
                Console.Write("Write block height: ");
                var blockHeight = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(blockHeight))
                {
                    break;
                }
                else if (!int.TryParse(blockHeight, out height))
                {
                    continue;
                }
                #endregion

                var (row, col, isHorizontal) = table.FindNextAvailableSpot(width, height);

                if (row != -1 && col != -1)
                {
                    table.PlaceBlock(row, col, width, height);
                }
                else
                {
                    Console.WriteLine($"No space for {width}x{height}");
                }

                Console.WriteLine();

                table.PrintTable();
            } while (true);
        }
    }

    public class Table
    {
        private char[,] table;

        public Table(int rows, int col)
        {
            table = new char[rows, col];
            InitializeTable();
        }

        private void InitializeTable()
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = '0';
                }
            }
        }

        public (int, int, bool) FindNextAvailableSpot(int blockWidth, int blockHeight)
        {
            for (int i = 0; i <= table.GetLength(0) - blockHeight; i++)
            {
                for (int j = 0; j <= table.GetLength(1) - blockWidth; j++)
                {
                    bool canPlace = true;

                    // Check if the block can fit in the current position
                    for (int x = 0; x < blockHeight; x++)
                    {
                        for (int y = 0; y < blockWidth; y++)
                        {
                            if (table[i + x, j + y] != '0')
                            {
                                canPlace = false;
                                break;
                            }
                        }
                        if (!canPlace)
                        {
                            break;
                        }
                    }

                    if (canPlace)
                    {
                        return (i, j, true); // true or false doesn't matter for dynamic blocks
                    }
                }
            }

            return (-1, -1, false); // No available spot found
        }

        public void PlaceBlock(int row, int col, int blockWidth, int blockHeight)
        {
            for (int x = 0; x < blockHeight; x++)
            {
                for (int y = 0; y < blockWidth; y++)
                {
                    table[row + x, col + y] = '*';
                }
            }
        }

        public void PrintTable()
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
