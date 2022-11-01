using System.Collections;

namespace Hit2.XUnit
{
    public abstract class HitXunit : IEnumerable<object[]>
    {
        private readonly Hit _hit;

        protected HitXunit(Action<HitOpt>? setOptions = null) => _hit = new Hit(setOptions);

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var testName in _hit.TestNames) yield return new object[] { _hit, testName };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}
