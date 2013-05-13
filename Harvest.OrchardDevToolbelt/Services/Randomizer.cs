using System;
using Orchard;

namespace Harvest.OrchardDevToolbelt.Services {
    public interface IRandomizer : ISingletonDependency {
        int Next(int min, int max);
    }

    public class Randomizer : IRandomizer {
        private readonly Random _rnd;

        public Randomizer() {
            _rnd = new Random(Guid.NewGuid().GetHashCode());
        }

        public int Next(int min, int max) {
            return _rnd.Next(min, max);
        }
    }
}