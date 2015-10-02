﻿using ConsoleApplication1;
using Moq;
using NUnit.Framework;

namespace MinesweeperTests
{
    [TestFixture]
    public class MineSweeperTests
    {
        [TestCase("", Result="")]
        [TestCase("*", Result = "*")]
        [TestCase(".", Result = "0")]
        [TestCase("*.", Result = "*1")]
        [TestCase(".*", Result = "1*")]
        [TestCase("..", Result = "00")]
        [TestCase("**", Result = "**")]
        [TestCase("*.*", Result = "*2*")]
        [TestCase(".*.", Result = "1*1")]
        [TestCase(".**.*.", Result = "1**2*1")]
        [TestCase("....*..*..**.*", Result = "0001*11*11**2*")]
        public string TestMinesweeper(string input)
        {
            string result = null;

            var sequenceProvider = new Mock<IMineSequenceProvider>();
            sequenceProvider.Setup(x => x.GetSequence()).Returns(input);
            
            var resultHandler = new Mock<IResultHandler>();
            resultHandler.Setup(x => x.HandleResult(It.IsAny<string>())).Callback<string>(s => result = s);

            new Program(sequenceProvider.Object, resultHandler.Object).Run();

            return result;
        }
    }
}
