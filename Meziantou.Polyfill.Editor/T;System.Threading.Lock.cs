#pragma warning disable CS9216 // A value of type 'System.Threading.Lock' converted to a different type will use likely unintended monitor-based locking in 'lock' statement.
namespace System.Threading
{
    internal sealed class Lock
    {
        private readonly object _lockObject = new();

        public bool IsHeldByCurrentThread => Monitor.IsEntered(_lockObject);

        public void Enter() => Monitor.Enter(_lockObject);
        public bool TryEnter() => Monitor.TryEnter(_lockObject);
        public bool TryEnter(TimeSpan timeout) => Monitor.TryEnter(_lockObject, timeout);
        public bool TryEnter(int millisecondsTimeout) => TryEnter(TimeSpan.FromMilliseconds(millisecondsTimeout));
        public void Exit() => Monitor.Exit(_lockObject);

        public Scope EnterScope()
        {
            Enter();
            return new Scope(this);
        }

        public readonly ref struct Scope
        {
            private readonly Lock _owner;

            internal Scope(Lock owner) => _owner = owner;

            public void Dispose() => _owner.Exit();
        }
    }
}