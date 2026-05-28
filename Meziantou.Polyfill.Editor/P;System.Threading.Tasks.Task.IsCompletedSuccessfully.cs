namespace System.Threading.Tasks;

static partial class PolyfillExtensions
{
    extension(Task task)
    {
        public bool IsCompletedSuccessfully => task.Status == TaskStatus.RanToCompletion;
    }
}
