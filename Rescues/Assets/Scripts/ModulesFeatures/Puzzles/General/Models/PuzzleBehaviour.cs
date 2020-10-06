

namespace Rescues
{
    /// <summary>
    /// Добавляется на объект, с которым должен взаимойдествоать главный герой
    /// </summary>
    public sealed class  PuzzleBehaviour: InteractableObjectBehavior
    {
        #region Fields

        public Puzzle Puzzle;
        public ItemData ItemData;

        #endregion
    }
}
