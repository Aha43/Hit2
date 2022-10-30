using System.Collections;

namespace Hit2.XUnit
{
    public abstract class HitXunitClassData : IEnumerable<object[]>
    {
        private readonly Hit _hit;

        protected HitXunitClassData(Action<HitOpt>? setOptions = null) => _hit = new Hit(setOptions);

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var testName in _hit.TestNames) yield return new object[] { _hit, testName };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}
