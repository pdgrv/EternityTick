using Random = System.Random;

namespace Eternity.Core
{
    public class CoreContext
    {
        public readonly int CurrentLevel;
        public readonly Random Rnd;

        public CoreContext(int currentLevel, int? seed = null)
        {
            CurrentLevel = currentLevel;
            Rnd = seed.HasValue ? new Random(seed.Value) : new Random();
        }
    }
}