using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SqlDependency.Stop(@"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999");
            SqlDependency.Start(
                @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999");
        }
    }
}
