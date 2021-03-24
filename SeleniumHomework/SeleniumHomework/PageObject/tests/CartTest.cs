using NUnit.Framework;

namespace SeleniumHomework.PageObject
{
    public class CartTest : TestBase
    {
        [Test]
        public void CartAddRemoveTest()
        {
            app.mainHelper.OpenMainPage();
            app.mainHelper.OpenFirstProduct();
            app.productHelper.AddProductToCart(1);
            app.mainHelper.OpenFirstProduct();
            app.productHelper.AddProductToCart(2);
            app.mainHelper.OpenFirstProduct();
            app.productHelper.AddProductToCart(3);
            app.mainHelper.OpenCart();
            app.cartHelper.EmptyCart();
        }
    }
}
