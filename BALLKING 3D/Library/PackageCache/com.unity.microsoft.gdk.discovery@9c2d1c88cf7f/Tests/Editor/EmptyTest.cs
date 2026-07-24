using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

internal class EmptyTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void AlwaysPasses()
    {
        Debug.Log(@"This package ONLY SUPPORTS THE WINDOWS PLATFORM. GDK is not supported on Linux or OSX. However the automation system expects to see 'some' tests pass");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator WithEnumeratorAlwaysPasses()
    {
        Debug.Log(@"This package ONLY SUPPORTS THE WINDOWS PLATFORM. GDK is not supported on Linux or OSX. However the automation system expects to see 'some' tests pass");

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}