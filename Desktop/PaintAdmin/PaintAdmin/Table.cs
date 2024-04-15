using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintAdmin
{
    class Table
    {
        public class City
        {
            public int IdCity { get; set; }
            public string NameCity { get; set; }
        }

        public class Color
        {
            public int IdColor { get; set; }
            public byte[] Certificate { get; set; }
            public byte[] Photo { get; set; }
            public double Priority { get; set; }
            public int TypeApplicationId { get; set; }
            public int TempPulverizationId { get; set; }
            public int ShineId { get; set; }
            public int TypeSurfaceId { get; set; }
            public int RalCatalogId { get; set; }
            public TypeApplication TypeApplications { get; set; }
            public TempPulverization TempPulverizations { get; set; }
            public Shine Shines { get; set; }
            public TypeSurface TypeSurfaces { get; set; }
            public RalCatalog RalCatalog { get; set; }
        }

        public class ColorDelivery
        {
            public int IdColorDelivery { get; set; }
            public int DeliveryId { get; set; }
            public int ColorId { get; set; }
            public int Quantity { get; set; }
            public Delivery Deliverys { get; set; }
            public Color Colors { get; set; }
        }

        public class Delivery
        {
            public int IdDelivery { get; set; }
            public string Adress { get; set; }
            public string Salt { get; set; }
            public byte[] Cheque { get; set; }
            public int CityId { get; set; }
            public int StatusOrderId { get; set; }
            public int UserId { get; set; }
            public DateTime Date { get; set; }
            public City Cites { get; set; }
            public StatusDelivery StatusDeliveres { get; set; }
            public User Users { get; set; }

            public bool IsDelivired { get; set; }
            public bool IsPay { get; set; }
            public bool IsNotPay { get; set; }
            public bool IsCancel { get; set; }
            public bool IsPickUp { get; set; }
            public bool IsPoint { get; set; }
        }

        public class Discount
        {
            public int IdDiscount { get; set; }
            public int ColorId { get; set; }
            public int SizeDicsount { get; set; }
            public Color Colors { get; set; }
        }

        public class FeedBack
        {
            public int IdFeedBack { get; set; }
            public string Number_Or_E_mail { get; set; }
            public bool End { get; set; }
            public string NameUser { get; set; }
            public byte[] Message { get; set; }
            public int? UserId { get; set; }
            public User Users { get; set; }
            public DateTime Date { get; set; }
            public string Category { get; set; }
            public string Mes { get; set; }
            public bool IsDateExpired { get; set; }
        }

        public class News
        {
            public int IdNews { get; set; }
            public string HeadingNews { get; set; }
            public byte[] TextNews { get; set; }
            public DateTime Date { get; set; }
            public int UserId { get; set; }
            public User Users { get; set; }
        }

        public class PhotoNews
        {
            public int IdPhotoNews { get; set; }
            public int NewsId { get; set; }
            public byte[] Photo { get; set; }
            public News Newses { get; set; }
        }

        public class QuantityColor
        {
            public int IdQuantityColors { get; set; }
            public int ColorId { get; set; }
            public int WareHouseId { get; set; }
            public int Quantity { get; set; }
            public double Price_For_KG { get; set; }
            public DateTime Date { get; set; }
            public int Shelf_Life { get; set; }
            public Color Colors { get; set; }
            public WareHouse WareHouses { get; set; }
        }

        public class RalCatalog
        {
            public int IdRalCatalog { get; set; }
            public string NameRal { get; set; }
            public string ColorRal { get; set; }
            public string ColorHtml { get; set; }
        }

        public class Role
        {
            public int IdRole { get; set; }
            public string NameRole { get; set; }
        }

        public class Shine
        {
            public int IdShine { get; set; }
            public int FirstProcent { get; set; }
            public int EndProcent { get; set; }
            public string NameShine { get; set; }
        }

        public class StatusDelivery
        {
            public int IdStatusOrder { get; set; }
            public string NameStatusOrder { get; set; }
        }

        public class TempPulverization
        {
            public int IdTempPulverization { get; set; }
            public int Degree { get; set; }
            public int Time { get; set; }
        }

        public class Token
        {
            public int IdToken { get; set; }
            public string Token1 { get; set; }
            public int UserId { get; set; }
            public User Users { get; set; }
        }

        public class TypeApplication
        {
            public int IdTypeApplication { get; set; }
            public string NameTypeApplication { get; set; }
        }

        public class TypeSurface
        {
            public int IdTypeSurface { get; set; }
            public string NameTypeSurface { get; set; }
        }

        public class User
        {
            public int IdUser { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patromymic { get; set; }
            public string EMail { get; set; }
            public string NumberTelephone { get; set; }
            public double Priority { get; set; }
            public string Salt { get; set; }
            public bool Verification { get; set; }
            public bool Verification_Number { get; set; }
            public byte[] Photo { get; set; }
            public int RoleId { get; set; }
            public Role Roles { get; set; }
        }

        public class WareHouse
        {
            public int IdWareHouse { get; set; }
            public int CityId { get; set; }
            public string Adress { get; set; }
            public string AdressCity { get; set; }
            public City City { get; set; }
        }

        public class SoldColor
        {
            public int ID_Sold_Color { get; set; }
            public int Delivery_ID { get; set; }
            public float Price_Delivery { get; set; }
            public DateTime Date { get; set; }
            public Delivery Delivery { get; set; }
        }
    }
}
