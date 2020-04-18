using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhoIsWho.Controllers;

namespace WhoIsWhoUnitTests
{
    [TestClass]
    public class AssignationTests
    {
        [TestMethod]
        public void TryToStart()
        {
            AssignationController controller = new AssignationController();
            controller.TryToStart();
        }
    }
}
