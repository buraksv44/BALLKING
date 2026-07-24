using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity.Microsoft.GDK.Tests
{
    public class EmptyTest
    {
        [Test]
        public void AlwaysPasses()
        {
            Debug.Log(@"This package ONLY SUPPORTS THE WINDOWS PLATFORM. GDK is not supported on Linux or OSX. However the automation system expects to see 'some' tests pass");
        }
    }
}