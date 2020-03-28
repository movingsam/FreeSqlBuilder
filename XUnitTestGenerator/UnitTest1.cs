using System;
using System.IO;
using FreeSql.Generator.Core;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTestGenerator
{
    public class UnitTest1
    {
        //private Options project;
        public UnitTest1()
        {
            using (StreamReader configStream = new StreamReader(Path.Combine(AppContext.BaseDirectory, "test.json")))
            {
                var jsonConfigStr = configStream.ReadToEnd();
                //project = JsonConvert.DeserializeObject<Options>(jsonConfigStr);
            }
        }

        [Fact]
        public void Test1()
        {
            
            //var temp =project ;
        }
    }
}
