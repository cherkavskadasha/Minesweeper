namespace Minesweeper.Models.Field
{
    public class Field
    {
        public Cell[,] Cells { get; set; }

        public int BombCount { get; set; }

        public int ActiveCellsRemain { get; set; }

        public IFieldStrategy FieldStrategy { get; set; }

        public Field(int rows, int columns, string difficulty)
        {
            switch (difficulty)
            {
                case "Expert":
                    FieldStrategy = new ExpertFieldStrategy();
                    break;
                case "Intermediate":
                    FieldStrategy = new IntermediateFieldStrategy();
                    break;
                default:
                    FieldStrategy = new BeginnerFieldStrategy();
                    break;
            }

            Cells = FieldStrategy.CreateField(rows, columns);
            BombCount = FieldStrategy.BombCount;
            ActiveCellsRemain = rows * columns - BombCount;
        }
    }
}
