using DebugMenu;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitTests
{
    #region Validate the ValidateMethods

    [Test]
    public static void ValidateTheValidation()
    {
        // it's false because in DebugDemo we have a not Static Method that we writed to show how to not use :)

        DebugCall.ValidateMethods();
    }

    #endregion Validate the ValidateMethods

    #region Valid me that path

    [Test]
    public static void ValidAPath()
    {
        // path to enter :
        // A path should be in the dictionnary
        // i.e. : string path = "Debug/Test/Static";

        string path = "Enter your static path to test here";

        string[] myPathsFromGetPaths = DebugCall.GetPaths();
        List<string> myPathsToTest = new List<string>();

        bool result = false;

        myPathsToTest.AddRange(myPathsFromGetPaths.ToList());

        result = myPathsToTest
                        .Where(u => u.Equals(path))
                        .Any();

        Assert.IsTrue(result);
    }

    #endregion Valid me that path

    #region Valid me that Primitive type

    [Test]
    public static void ValidAPrimitiveType()
    {
        var myValue = "change me to test";

        var valueType = myValue.GetType();

        Assert.IsTrue(valueType.IsPrimitive);
    }

    #endregion Valid me that Primitive type

    #region Wait Parameters To valid

    // hum ... how to validate that anything isn't anything UwU

    // need to wait other invokemethod custom tools to unit test those one by one

    #endregion Wait Parameters To valid

    /*

    #region Testrunner Example

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

    #endregion Testrunner Example

    */
}