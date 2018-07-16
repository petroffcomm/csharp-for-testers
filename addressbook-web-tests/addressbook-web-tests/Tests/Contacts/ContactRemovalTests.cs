using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            CreateContactForTestIfNecessary();

            app.Contacts.Delete(1);
        }
    }
}
