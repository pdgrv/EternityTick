using Eternity.Core.TickSystem.Commands;

namespace Eternity.Core.TickSystem
{
    public interface ITickEntity
    {
        public TickCommand CurrentCommand { get; }
        public void ClearCommand();
    }
}