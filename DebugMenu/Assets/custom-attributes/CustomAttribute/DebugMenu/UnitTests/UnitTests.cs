using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DebugMenu;

public class UnitTests : MonoBehaviour
{
    #region InvokeMethod Debug/Test/Static

    /// <summary>
    ///     InvokeMethod
    ///
    /// correct path :
    /// path = "Debug/Test/Static"
    /// </summary>
    [Test]
    public static void InvokeVoidMethod()
    {
        string path = "Enter your path to test static here";

        Assert.AreEqual(path, "Debug/Test/Static");
    }

    #endregion InvokeMethod Debug/Test/Static

    #region Invoke Method < type > ( path )

    [Test]
    public static void InvokeVoidMethod(string type, string path)
    {
    }

    #endregion Invoke Method < type > ( path )

    // A Test behaves as an ordinary method
    [Test]
    public void UnitTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator UnitTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}