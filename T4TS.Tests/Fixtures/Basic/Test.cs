﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using T4TS.Tests.Utils;

namespace T4TS.Tests.Fixtures.Basic
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void BasicModelHasExpectedOutput()
        {
            // Generated output
            var solution = DTETransformer.BuildDteSolution(typeof(BasicModel));
            var settings = new Settings();
            var generator = new CodeTraverser(solution, settings);
            var data = generator.GetAllInterfaces().ToList();
            var generatedOutput = OutputFormatter.GetOutput(data, settings);

            const string expectedOutput =
@"
/****************************************************************************
  Generated by T4TS.tt - don't make any changes in this file
****************************************************************************/

declare module T4TS {
    /** Generated from T4TS.Tests.Fixtures.Basic.BasicModel **/
    export interface BasicModel {
        MyProperty: number;
    }
}
";

            Assert.AreEqual(Normalize(expectedOutput), Normalize(generatedOutput));
        }

        private string Normalize(string input)
        {
            return Regex.Replace(input, @"\r\n|\n\r|\n|\r", "\n").Trim();
        }
    }
}
