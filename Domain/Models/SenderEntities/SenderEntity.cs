namespace Domain.Models.SenderEntities
{
    public class SenderEntity
    {
        public string UserName { get; private set; }
        public string UserEmail { get; private set; }
        public string UserPhone { get; private set; }
        public string ProductName { get; private set; }
        public double ProductPrice { get; private set; }
        public string ProductThumbImg { get; private set; }
        public string ProductLinkRedirect { get; private set; }

        public SenderEntity(
            string userName, 
            string userEmail, 
            string userPhone, 
            string productName, 
            double productPrice, 
            string productThumbImg, 
            string productLinkRedrect
        )
        {
            UserName = userName;
            UserEmail = userEmail;
            UserPhone = userPhone;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductThumbImg = productThumbImg;
            ProductLinkRedirect = productLinkRedrect;
        }
    }
}