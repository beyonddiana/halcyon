using NUnit.Framework;
using Nini.Config;
using InWorldz.Testing;
using InWorldz.Phlox.Types;
using Nini.Ini;
using OpenMetaverse;
using System.Collections.Generic;
using OpenSim.Region.ScriptEngine.Shared.ScriptBase;
using OpenSim.Region.Framework.Scenes;

namespace InWorldz.Phlox.Engine.Tests
{

    [TestFixture]
    public class JsonTests
    {
        LSLSystemAPI lslSystemApi;
        Scene world;

        [TestFixtureSetUp]
        public void Setup()
        {
            var iniDoc = new IniDocument();
            var configSource = new IniConfigSource(iniDoc);
            configSource.AddConfig("InWorldz.Phlox");
            world = SceneHelper.CreateScene(9000, 1000, 1000);
            var engine = new MockScriptEngine(world, configSource);
            lslSystemApi = new LSLSystemAPI(engine, null, 0, UUID.Zero);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            SceneHelper.TearDownScene(world);
        }

        [Test]
        public void TestJsonSimpleObject()
        {
            var expectedResult = new LSLList(new List<object> { "dummy", "data" });
            LSLList asList = lslSystemApi.llJson2List("{ \"dummy\" : \"data\" }");
            Assert.IsTrue(expectedResult.Equals(asList));
        }

        [Test]
        public void TestJsonSimpleObjectWithNullInValue()
        {
            var expectedResult = new LSLList(new List<object>{ "dummy", ScriptBaseClass.JSON_NULL });
            var asList = lslSystemApi.llJson2List("{ \"dummy\" : null }");
            Assert.IsTrue(expectedResult.Equals(asList));
        }

        [Test]
        public void TestJsonArrayWithNull()
        {
            var expectedResult = new LSLList(new List<object> { ScriptBaseClass.JSON_NULL });
            var asList = lslSystemApi.llJson2List("[ null ]");
            Assert.IsTrue(expectedResult.Equals(asList));
        }
        [Test]
        public void TestJsonSimpleObjectWithFloatZero()
        {
            var expectedResult = new LSLList(new List<object> { "dummy", 0.0f });
            LSLList asList = lslSystemApi.llJson2List("{ \"dummy\" : 0.0 }");
            Assert.IsTrue(expectedResult.Equals(asList));
        }

        [Test]
        public void TestListToJsonSimpleObjectWithIntegerZero()
        {
            var expectedResult = "{\"dummy\":0.0}";
            string asJson = lslSystemApi.llList2Json(ScriptBaseClass.JSON_OBJECT, new LSLList(new List<object> { "dummy", 0.0f }));
            Assert.IsTrue(expectedResult.Equals(asJson));
        }

        [Test]
        public void TestListToJsonSimpleObjectWithIntegerNonZero()
        {
            var expectedResult = "{\"dummy\":1.0}";
            string asJson = lslSystemApi.llList2Json(ScriptBaseClass.JSON_OBJECT, new LSLList(new List<object> { "dummy", 1.0f }));
            Assert.IsTrue(expectedResult.Equals(asJson));
        }

        [Test]
        public void TestJsonSimpleObjectWithIntegerZero()
        {
            var expectedResult = new LSLList(new List<object> { "dummy", 0 });
            LSLList asList = lslSystemApi.llJson2List("{ \"dummy\" : 0 }");
            Assert.IsTrue(expectedResult.Equals(asList));
        }

        [Test]
        public void TestListToJsonSimpleObjectWithFloatZero()
        {
            var expectedResult = "{\"dummy\":0}";
            string asJson = lslSystemApi.llList2Json(ScriptBaseClass.JSON_OBJECT, new LSLList(new List<object> { "dummy", 0 }));
            Assert.IsTrue(expectedResult.Equals(asJson));
        }

        [Test]
        public void TestListToJsonSimpleObjectWithFloatNonZero()
        {
            var expectedResult = "{\"dummy\":1}";
            string asJson = lslSystemApi.llList2Json(ScriptBaseClass.JSON_OBJECT, new LSLList(new List<object> { "dummy", 1 }));
            Assert.IsTrue(expectedResult.Equals(asJson));
        }


    }
}
