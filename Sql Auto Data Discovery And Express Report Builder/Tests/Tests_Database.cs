using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sql_Auto_Data_Discovery.Business.Data;
using Sql_Auto_Data_Discovery.Business.Models.Commom;
using Sql_Auto_Data_Discovery.Business.Models.Sql;
using SqlEnums.master;
using SqlEnums.master.Columns;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class Tests_Database
    {
        [TestMethod]
        public void MethodUnderTest_Scenario_Expected()
        {
            //Arrange
            
            //Act
            using (var db = new Database(Connections.List.FirstOrDefault(i => i.Key != "LocalSqlServer").Value.ConnectionString))
            {
                var listOfTables = db.GetListOfObjects();
            }
            //Assert

        }
        [TestMethod]
        public void Select_into_NamedObject()
        {
            //Arrange
            
            //Act
            using (var db = new Database(Connections.List.FirstOrDefault(i => i.Key != "LocalSqlServer").Value.ConnectionString))
            {
                var listOfTables = db.Select<ObjectKeyValue>(Schemas.sys, Views.objects
                    , fields: new object[]
                    {
                        objects.object_id,
                        objects.name,
                        objects.type_desc,
                    }
                    ,where: new []
                    {
                        new KeyValue(objects.type_desc, object_types.USER_TABLE.ToString()), 
                    }
                    ,order_by: new object[]
                    {
                        objects.type_desc,
                        objects.name,
                    });
            }
            //Assert

        }
    }
}