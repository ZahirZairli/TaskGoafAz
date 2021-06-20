using goaf.az.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace goaf.az.Controllers
{
    public class AdminController : Controller
    {
        DataClassesDataContext db = new DataClassesDataContext();
        // GET: Admin

        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult SignIn(string AdminMail, string AdminPassword)
        {
            var c = CreateMD5("github/a gore deyisdim" + AdminPassword + "github/a gore deyisdim");
            DataTable dt = Sql.Exec($"select AdminId,AdminName from Admin where AdminMail=N'{AdminMail}' and AdminPassword=N'{c}'");
            HttpCookie cookie = new HttpCookie("Admin");
            cookie.Expires = DateTime.Now.AddDays(1d);
            if (dt.Rows.Count == 0 || AdminMail == null || AdminPassword == null)
            {
                return RedirectToAction("AdminLogin");
            }
            else
            {
                cookie.Values.Add("AdminId", dt.Rows[0][0].ToString());
                cookie.Values.Add("AdminName", dt.Rows[0][1].ToString());
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
        }

        public ActionResult ForgetPass( string AdminMail)
        {
            DataTable dt = Sql.Exec($"Select * from Admin where AdminMail=N'{AdminMail}'");
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][3].ToString() == AdminMail)
                {
                    Random a = new Random();
                    var kod = a.Next(100000, 9999999);
                    MailMessage mesajim = new MailMessage();
                    SmtpClient istemci = new SmtpClient();
                    istemci.Credentials = new System.Net.NetworkCredential("github/a gore deyisdim.github/a gore deyisdim@hotmail.com", "github/a gore deyisdim");
                    istemci.Port = 587;
                    istemci.Host = "smtp.live.com";
                    istemci.EnableSsl = true;
                    mesajim.To.Add(AdminMail);
                    mesajim.From = new MailAddress("github/a gore deyisdim.github/a gore deyisdim@hotmail.com");
                    mesajim.Subject = "The Code for RePassword";
                    mesajim.Body = kod.ToString();
                    istemci.Send(mesajim);
                    Sql.Exec($"Update  Admin set AdminRandomeCode=N'{kod}',AdminRandomeCodeDate= GETDATE() where AdminMail=N'{AdminMail}'");
                    return RedirectToAction("NewPassword");
                }
                else
                {
                    return RedirectToAction("AdminForgetPassword");
                }
            }
            else
            {
                return RedirectToAction("AdminForgetPassword");
            }

        }
        public ActionResult NewPasswordmain(string AdminRandomeCode, string AdminPassword, string AdminMail)
        {
            DataTable dt = Sql.Exec($"Select * from Admin where AdminMail=N'{AdminMail}'");
            DateTime tarix = new DateTime();
            if (dt.Rows.Count != 0)
            {
            tarix = Convert.ToDateTime(dt.Rows[0][7]);
            DateTime cari = DateTime.Now.AddMinutes(-1);
            var l = CreateMD5("levengi" + AdminPassword + "levengi");
            if (AdminRandomeCode == dt.Rows[0][6].ToString() && AdminRandomeCode != null && tarix >= cari)
            {
                Sql.Exec($"Update Admin set AdminPassword=N'{l}' where AdminMail=N'{AdminMail}'");
                return RedirectToAction("AdminLogin");
            }
            else
            {
                return RedirectToAction("AdminForgetPassword");
            }
            }
            else
            {
                return RedirectToAction("AdminForgetPassword");
            }
        }
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Admin");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);
            return RedirectToAction("AdminLogin");
        }

        public ActionResult salam(int id, string password, string AdminPassword)
        {
            Admin oldadmin= db.Admin.SingleOrDefault(x => x.AdminId == id);
            var Password = CreateMD5("github/a gore deyisdim" + password + "github/a gore deyisdim");
            var NewPassword = CreateMD5("github/a gore deyisdim" + AdminPassword + "github/a gore deyisdim");

            if (oldadmin.AdminPassword == Password)
            {
                oldadmin.AdminPassword = NewPassword;
                db.SubmitChanges();
                return RedirectToAction("LogOut");
            }
            return RedirectToAction("Index");
        }
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        public ActionResult AdminForgetPassword()
        {
            return View();
        }

        public ActionResult Index()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            var cookie = Request.Cookies["Admin"];
            //var admin = cookie["AdminId"];
            return View(cookie);
        }
        public ActionResult BonusList()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.BonusList = db.Bonus.ToList();
            return View();
        }
        public ActionResult deletebonuslist(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.Bonus.DeleteOnSubmit(db.Bonus.SingleOrDefault(x => x.BonusId == id));
            db.SubmitChanges();
            return RedirectToAction("BonusList");
        }
        public ActionResult BonusListEdit(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            var editbonus = db.Bonus.SingleOrDefault(x => x.BonusId == id);
            return View(editbonus);
        }
        public ActionResult UpdateBonusList(int id, Bonus newbonus, HttpPostedFileBase BonusPhoto)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            Bonus bonus1 = db.Bonus.SingleOrDefault(s => s.BonusId == id);
            if (BonusPhoto != null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(BonusPhoto.FileName);
                BonusPhoto.SaveAs(Server.MapPath("~/Content/storage/blogs/" + Photoname));
                bonus1.BonusPhoto = Photoname;
            }
            bonus1.BonusName = newbonus.BonusName;
            bonus1.BonusPrice = newbonus.BonusPrice;
            bonus1.BonusCount = newbonus.BonusCount;
            db.SubmitChanges();
            return RedirectToAction("BonusList");
        }

        public ActionResult NewBonus()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult NewBonus(Bonus newbonus, HttpPostedFileBase BonusPhoto)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            Bonus old = new Bonus();
            if (BonusPhoto != null)
            {
                string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(BonusPhoto.FileName);
                BonusPhoto.SaveAs(Server.MapPath("~/Content/storage/blogs/" + PhotoName));
                old.BonusPhoto = PhotoName;
            }
            old.BonusName = newbonus.BonusName;
            old.BonusPrice = newbonus.BonusPrice;
            old.BonusCount = newbonus.BonusCount;
            db.Bonus.InsertOnSubmit(old);
            db.SubmitChanges();
            return RedirectToAction("NewBonus");
        }

        public ActionResult IndexSlideList()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.SlideList = db.IndexSlide.ToList();
            return View();
        }
        public ActionResult IndexSlideListDelete(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.IndexSlide.DeleteOnSubmit(db.IndexSlide.SingleOrDefault(x => x.SlideId == id));
            db.SubmitChanges();
            return RedirectToAction("IndexSlideList");
        }
        public ActionResult IndexSlideListEdit(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            var editindexslide = db.IndexSlide.SingleOrDefault(x => x.SlideId == id);
            return View(editindexslide);
        }
        public ActionResult IndexSlideListUpdate(int id, IndexSlide newslide, HttpPostedFileBase SlidePhoto)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            IndexSlide slide1 = db.IndexSlide.SingleOrDefault(s => s.SlideId == id);
            if (SlidePhoto != null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(SlidePhoto.FileName);
                SlidePhoto.SaveAs(Server.MapPath("~/Content/storage/sliders/" + Photoname));
                slide1.SlidePhoto = Photoname; 
            }
            slide1.SlideName = newslide.SlideName;
            slide1.SlideLink = newslide.SlideLink;
            db.SubmitChanges();
            return RedirectToAction("IndexSlideList");
        }

        public ActionResult NewIndexSlide()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult NewIndexSlide(IndexSlide newslide, HttpPostedFileBase SlidePhoto)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            IndexSlide old = new IndexSlide();
            if (SlidePhoto != null)
            {
                string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(SlidePhoto.FileName);
                SlidePhoto.SaveAs(Server.MapPath("~/Content/storage/sliders/" + PhotoName));
                old.SlidePhoto = PhotoName;
            }
            old.SlideName = newslide.SlideName;
            old.SlideLink = newslide.SlideLink;
            db.IndexSlide.InsertOnSubmit(old);
            db.SubmitChanges();
            return RedirectToAction("IndexSlideList");
        }

        public ActionResult MainInformation()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.information = db.MainInformation.ToList();
            return View();
        }
        public ActionResult editmaininfo(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.maininfo = db.MainInformation.ToList();
            return View();
        }
        public ActionResult MainInfoUpdate(int id, MainInformation newinfo, HttpPostedFileBase Loqo)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            MainInformation oldinfo = db.MainInformation.SingleOrDefault(s => s.InfoId == id);
            if (Loqo != null)
            {
                string LoqoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(Loqo.FileName);
                Loqo.SaveAs(Server.MapPath("~/Content/storage/site/" + LoqoName));
                oldinfo.Loqo = LoqoName;
            }
            oldinfo.Facebook = newinfo.Facebook;
            oldinfo.Instagram = newinfo.Instagram;
            oldinfo.Youtube = newinfo.Youtube;
            oldinfo.Tvitter = newinfo.Tvitter;
            oldinfo.Whtasapp = newinfo.Whtasapp;
            oldinfo.Address = newinfo.Address;
            oldinfo.Email = newinfo.Email;
            db.SubmitChanges();
            return RedirectToAction("MainInformation");
        }

        public ActionResult GameType()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.gametype = db.GameType.ToList();
            return View();
        }


        public ActionResult deletegametype(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.GameType.DeleteOnSubmit(db.GameType.SingleOrDefault(x => x.GameTypeId == id));
            db.SubmitChanges();
            return RedirectToAction("GameType");
        }
        public ActionResult NewGameType()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult NewGameType(HttpPostedFileBase GameTypePhoto, GameType newgametype)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            GameType old = new GameType();
            if (GameTypePhoto != null)
            {
                string PhotoName = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(GameTypePhoto.FileName);
                GameTypePhoto.SaveAs(Server.MapPath("~/Content/storage/categories/" + PhotoName));
                old.GameTypePhoto = PhotoName;
            }
            old.GameTypeAbout = newgametype.GameTypeAbout;
            old.GameTypeName = newgametype.GameTypeName;
            db.GameType.InsertOnSubmit(old);
            db.SubmitChanges();
            return RedirectToAction("GameType");
        }

        public ActionResult gametypeedit(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<GameType> gametype = db.GameType.ToList();
            ViewBag.gametype = db.GameType.SingleOrDefault(x => x.GameTypeId == id);
            return View();
        }

        public ActionResult updategametpe(int id, GameType game2, HttpPostedFileBase GameTypePhoto)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            GameType game1 = db.GameType.SingleOrDefault(s => s.GameTypeId == id);
            if (GameTypePhoto != null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(GameTypePhoto.FileName);
                GameTypePhoto.SaveAs(Server.MapPath("~/Content/storage/categories/" + Photoname));
                game1.GameTypePhoto = Photoname;
            }
            game1.GameTypeAbout = game2.GameTypeAbout;
            game1.GameTypeName = game2.GameTypeName;
            db.SubmitChanges();
            return RedirectToAction("GameType");
        }






        public ActionResult GameProductList()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.gametype = db.GameType.ToList();
            ViewBag.gameproduct = db.GameProduct.ToList();
            return View();
        }
        public ActionResult deleteproductlist(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.GameProduct.DeleteOnSubmit(db.GameProduct.SingleOrDefault(x => x.GameProductId == id));
            db.SubmitChanges();
            return RedirectToAction("GameProductList");
        }
        public ActionResult NewProduct()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.gametype = db.GameType.ToList();
            return View();
        }
        public ActionResult NewProductMain(GameProduct newproduct)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.GameProduct.InsertOnSubmit(newproduct);
            db.SubmitChanges();
            return RedirectToAction("GameProductList");

        }

        public ActionResult ProductListEdit(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<GameType> gametype = db.GameType.ToList();
            ViewBag.gametype = db.GameType.ToList();
            List<GameProduct> gameproduct = db.GameProduct.ToList();
            ViewBag.gameproduct = db.GameProduct.SingleOrDefault(x => x.GameProductId == id);
            return View();
        }
        public ActionResult updateproduct(int id, GameProduct productyeni)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            GameProduct productold = db.GameProduct.SingleOrDefault(s => s.GameProductId == id);
            productold.GameProductName = productyeni.GameProductName;
            productold.GameProductType = productyeni.GameProductType;
            productold.GameProductBonus = productyeni.GameProductBonus;
            productold.GameProductPrice = productyeni.GameProductPrice;
            productold.GameProductCount = productyeni.GameProductCount;
            productold.GameProductStock = productyeni.GameProductStock;
            db.SubmitChanges();
            return RedirectToAction("GameProductList");
        }


        public ActionResult ContactList()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            ViewBag.contactlist = db.Contact.ToList();
            return View();
        }

        public ActionResult deletecontactlist(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.Contact.DeleteOnSubmit(db.Contact.SingleOrDefault(x => x.ContactId == id));
            db.SubmitChanges();
            return RedirectToAction("ContactList");
        }

        public ActionResult AdminUser()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<Gamer> gamer = new List<Gamer>();
            ViewBag.gamer = db.Gamer.OrderBy(x => x.UserName).ToList();
            return View();
        }

        public ActionResult deleteuser(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            db.Gamer.DeleteOnSubmit(db.Gamer.SingleOrDefault(x => x.UserId == id));
            db.SubmitChanges();
            return RedirectToAction("AdminUser");
        }
        public ActionResult edituser(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            var user = db.Gamer.SingleOrDefault(x => x.UserId == id);
            return View(user);
        }
        public ActionResult updateuser(int id, Gamer newgamer)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            Gamer oldgamer = db.Gamer.SingleOrDefault(x => x.UserId == id);
            oldgamer.UserBalans = newgamer.UserBalans;
            oldgamer.UserBonus = newgamer.UserBonus;
            oldgamer.UserEmail = newgamer.UserEmail;
            db.SubmitChanges();
            return RedirectToAction("AdminUser");
        }
        public ActionResult sendmessage(int id, string text)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            sendingmessage newmessage = new sendingmessage();
            newmessage.messagetext = text;
            newmessage.messageUserId = id;
            newmessage.messagedate = DateTime.Now;
            db.sendingmessage.InsertOnSubmit(newmessage);
            db.SubmitChanges();

            return RedirectToAction("sendme", new { id });
        }
        public ActionResult sendme(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<sendingmessage> message = new List<sendingmessage>();
            ViewBag.message = db.sendingmessage.Where(x => x.messageUserId == id);
            var gamer = db.Gamer.SingleOrDefault(x => x.UserId == id);
            return View(gamer);
        }
        public ActionResult deletesendme(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<sendingmessage> mesaj= new List<sendingmessage>();
            var z = db.sendingmessage.SingleOrDefault(x => x.messageid == id);
            var user = z.messageUserId;
            db.sendingmessage.DeleteOnSubmit(db.sendingmessage.SingleOrDefault(x => x.messageid == id));
            db.SubmitChanges();
            return RedirectToAction("sendme", new { id=user });
        }

        public ActionResult DetailPayment(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<Payment> paymen2t = new List<Payment>();
            ViewBag.paymen2t = db.Payment.Where(x => x.PaymentTypeId == id);
            return View();
        }

        public ActionResult Payment()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            return View();
        }
        public ActionResult deletepaymenttype(int id)
        {
            db.PaymentType.DeleteOnSubmit(db.PaymentType.SingleOrDefault(x => x.PamentTypeMainId == id));
            db.SubmitChanges();
            return RedirectToAction("Payment");
        }

        public ActionResult NewPaymentType()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            return View();
        }

        public ActionResult newpaymenttypemain(PaymentType newpaymenttype, HttpPostedFileBase PaymentTypePhoto)
        {
            if (PaymentTypePhoto != null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(PaymentTypePhoto.FileName);
                PaymentTypePhoto.SaveAs(Server.MapPath("~/Content/storage/gamer/" + Photoname));
                newpaymenttype.PaymentTypePhoto = Photoname;
            }
            db.PaymentType.InsertOnSubmit(newpaymenttype);
            db.SubmitChanges();
            return RedirectToAction("NewPaymentType");
        }
        public ActionResult EditPaymentType(int id)
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            var payment2 = db.PaymentType.SingleOrDefault(x => x.PamentTypeMainId == id);
            return View(payment2);
        }

        public ActionResult updatepaymenttypemain(int id, PaymentType newpayment, HttpPostedFileBase PaymentTypePhoto)
        {

            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            PaymentType old = db.PaymentType.SingleOrDefault(s => s.PamentTypeMainId == id);
            if (PaymentTypePhoto != null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(PaymentTypePhoto.FileName);
                PaymentTypePhoto.SaveAs(Server.MapPath("~/Content/storage/gamer/" + Photoname));
                old.PaymentTypePhoto = Photoname;
            }
            old.PaymentTypeName = newpayment.PaymentTypeName;
            old.PaymentTypeNumber = newpayment.PaymentTypeNumber;
            old.PaymentTypeOwner = newpayment.PaymentTypeOwner;
            db.SubmitChanges();
            return RedirectToAction("Payment");
        }
        public ActionResult deletepayment(int id)
        {
            DataTable dt = Sql.Exec($"select * from Payment where PaymentID=N'{id}'");
            int z1 = Convert.ToInt32(dt.Rows[0][1]);
            db.Payment.DeleteOnSubmit(db.Payment.SingleOrDefault(x => x.PaymentID == id));
            db.SubmitChanges();
            return RedirectToAction("DetailPayment" , new { id=z1 });
        }
        public ActionResult Order()
        {
            List<PaymentType> payment = new List<PaymentType>();
            ViewBag.payment = db.PaymentType.ToList();
            List<orderlermain> orderlers = new List<orderlermain>();
            ViewBag.orderler = db.orderlermain.ToList();
            return View();
        }

        public ActionResult deleteorder(int id)
        {
            db.orders.DeleteOnSubmit(db.orders.SingleOrDefault(x => x.OrderId == id));
            db.SubmitChanges();
            return RedirectToAction("Order");
        }




    }
}
