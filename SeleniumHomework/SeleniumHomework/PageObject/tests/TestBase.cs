using NUnit.Framework;

namespace SeleniumHomework.PageObject
{
    public class TestBase
    {
        protected Application app;

        [SetUp]
        public void Start()
        {
            app = new Application();
        }

        [TearDown]
        public void Stop()
        {
            app.Quit();
        }
    }
}
