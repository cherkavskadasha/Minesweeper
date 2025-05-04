namespace Minesweeper.Models.Field
{
    public class Cell
    {
        public bool IsBomb { get; set; }

        public bool IsActivated { get; set; }

        public CellType CellType { get; set; }

        public Cell(bool isBomb, CellType cellType)
        {
            IsBomb = isBomb;
            IsActivated = false;
            CellType = cellType;
        }

        public bool Activate()
        {
            IsActivated = true;
            return IsBomb;
        }
    }
}
