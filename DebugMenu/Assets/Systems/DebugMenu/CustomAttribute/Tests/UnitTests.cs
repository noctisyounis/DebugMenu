using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DebugAttribute
{
    public class UnitTests
    {
        #region Validation

        /// <summary>
        /// Validation de l'initilisation et de la validition du dictionnaire
        /// </summary>
        [Test]
        public static void ValidateDictionnary()
        {
            DebugAttributeRegistry.ValidateMethods();
        }
        
        /// <summary>
        /// Vérification de la validité d'un chemin
        /// </summary>
        [Test]
        public static void ValidAPath()
        {
            string path = "Enter your static path to test here";

            string[] myPathsFromGetPaths = DebugAttributeRegistry.GetPaths();
            List<string> myPathsToTest = new List<string>();

            bool result = false;

            myPathsToTest.AddRange(myPathsFromGetPaths.ToList());

            result = myPathsToTest
                            .Where(u => u.Equals(path))
                            .Any();

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Vérification de la validition d'un type primitif
        /// </summary>
        [Test]
        public static void ValidAPrimitiveType()
        {
            var myValue = "change me to test";

            var valueType = myValue.GetType();

            Assert.IsTrue(valueType.IsPrimitive);
        }

        #endregion 
    }
}