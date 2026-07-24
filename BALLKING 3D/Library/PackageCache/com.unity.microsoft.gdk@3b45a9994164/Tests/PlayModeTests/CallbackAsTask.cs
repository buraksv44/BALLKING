#nullable enable

using System;
using System.Threading.Tasks;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)

namespace Unity.Microsoft.GDK.Tests
{
    /// <summary>
    ///     Helper methods to ensure tests can wait on a callback to be raised, and handle successes and failures
    ///     appropriately.
    /// </summary>
    /// <remarks>
    ///     The callbacks are raised from a separate thread as defined in <see cref="GdkTestHelper" />. Tests need to wait for
    ///     the callback to be raised, otherwise the test will not run the asserts in the callback. If there are assertion
    ///     failures, they need to be passed back to the test thread and raised there. Using a
    ///     <see cref="TaskCompletionSource{TResult}" />, we can implement this behaviour easily.
    /// </remarks>
    internal static class CallbackAsTask
    {
        internal static void SafeExecute(TaskCompletionSource<Empty> taskCompletionSource, Action callback)
        {
            try
            {
                callback.Invoke();
                taskCompletionSource.SetResult(new Empty());
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
        }

        internal static void SafeExecute<T>(TaskCompletionSource<T> taskCompletionSource, Func<T> callback)
        {
            try
            {
                taskCompletionSource.SetResult(callback.Invoke());
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }
        }
    }

    internal struct Empty
    {
    }
}

#endif //#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_GAMECORE_XBOXONE || UNITY_GAMECORE_SCARLETT)