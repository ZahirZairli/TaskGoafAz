using goaf.az.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Web.Security;
using System.Windows;

namespace goaf.az.Controllers
{
    public class SiteController : Controller
    {
        DataClassesDataContext db = new DataClassesDataContext();
        // GET: Site
        public ActionResult Index()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie !=null)
            {
            var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
            List<basket> basket = new List<basket>();
            ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }

            ViewBag.cn = db.MainInformation.ToList();   
            ViewBag.search = db.GameType.ToList();     
            ViewBag.IndexSlideMain = db.IndexSlide.ToList(); 
            ViewBag.Bonuseight = db.Bonus8.ToList();
            ViewBag.GameTypeeight = db.GameType8.ToList();
            return View();
        }


        public ActionResult AllGameType(int page = 1, int? sortlar = 0)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();  
            ViewBag.search = db.GameType.ToList();     
            if (sortlar == 1)
            {
                ViewBag.gametype = db.GameTypeAsc.ToList();
                var gametype = db.GameType.ToList().OrderBy(x => x.GameTypeName).ToPagedList(page, 4);
                return View(gametype);
            }
            else if (sortlar == 2)
            {
                ViewBag.gametype = db.GameTypeDesc.ToList();
                var gametype = db.GameType.ToList().OrderByDescending(x=> x.GameTypeName).ToPagedList(page, 4);
                return View(gametype);
            }
            else
            {
                ViewBag.gametype = db.GameType.ToList();
            var gametype = db.GameType.ToList().ToPagedList(page, 4);
            return View(gametype);
            }
        }
        public ActionResult AllBonus(int page = 1)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList(); 
            ViewBag.search = db.GameType.ToList();     
            ViewBag.bonus = db.Bonus.ToList();
            var bonus = db.Bonus.ToList().ToPagedList(page, 4);
            return View(bonus);
        }

        public ActionResult NewsandUpdates()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList(); 
            ViewBag.search = db.GameType.ToList();     
            return View();
        }
        public ActionResult BonusDetal(int id)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();  
            ViewBag.search = db.GameType.ToList();     
            var bonusdetal = db.Bonus.SingleOrDefault(x => x.BonusId == id);
            return View(bonusdetal);
        }
        public ActionResult GameDetal(int id)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            List<GameProducts> product = new List<GameProducts>();
            ViewBag.product = db.GameProducts.ToList();
            List<MainMessage> message = new List<MainMessage>();
            ViewBag.message = db.MainMessage.OrderByDescending(x => x.MessageId).Where(x => x.GameTypeId == id);
            ViewBag.cn = db.MainInformation.ToList(); 
            ViewBag.search = db.GameType.ToList();
            List<GameType> gamedetal = new List<GameType>();
            ViewBag.gamedetal = db.GameType.SingleOrDefault(x => x.GameTypeId == id);
            ViewBag.gamemaindetal = db.GameProducts.Where(x => x.GameTypeId == id);
            return View();
        }

        public ActionResult About()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();  
            ViewBag.search = db.GameType.ToList();     
            return View();
        }

        public ActionResult Contact()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();  
            ViewBag.search = db.GameType.ToList();     
            return View();
        }
        public ActionResult contactinsert(Contact newcontact)
        {

            db.Contact.InsertOnSubmit(newcontact);
            db.SubmitChanges();
            return RedirectToAction("Contact");
        }

        public ActionResult sort(int sortlar)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            if (sortlar == 1)
            {
                ViewBag.gametype = db.GameTypeAsc.ToList();
            }
            else if (sortlar == 2)
            {
                ViewBag.gametype = db.GameTypeDesc.ToList();
            }
            else
            {
                ViewBag.gametype = db.GameType.ToList();
            }
            return RedirectToAction("AllGameType");
        }















        [HttpPost]
        public ActionResult SignUp(string UserPassword, string UserName, Gamer newgamer, string UserEmail, string UserSurName)
        {
            DataTable dt = Sql.Exec($"Select * from Gamer where UserEmail=N'{UserEmail}' Or UserName=N'{UserName}'");
            if (dt.Rows.Count == 0 && UserName != null && UserSurName != null && UserPassword != null && UserEmail != null)
            {
                newgamer.UserPhoto = "game.png";
                newgamer.UserPassword = CreateMD5("github/a gore deyisdim" + UserPassword + "github/a gore deyisdim");
                string date2 = DateTime.Now.ToString("dd-MMM-yy");
                newgamer.UserSignDate = DateTime.Now;

                db.Gamer.InsertOnSubmit(newgamer);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }   
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult SignIn(string UserEmail, string UserPassword, string agreecheckbox)
            {
            var c = CreateMD5("github/a gore deyisdim" + UserPassword + "github/a gore deyisdim");
            DataTable dt = Sql.Exec($"select UserId,UserName,UserSurname,UserEmail,UserPhone,UserPhoto,UserBonus,UserBalans,UserMessage,UserSignDate,UserGender from Gamer where UserEmail=N'{UserEmail}' and UserPassword=N'{c}'");
            //CONVERT(VARCHAR(32), HashBytes('MD5', Concat('github/a gore deyisdim', UserId, 'github/a gore deyisdim')), 2)
            HttpCookie cookie = new HttpCookie("Gamer");

            if (agreecheckbox != null)
            {
            cookie.Expires = DateTime.Now.AddDays(30d);
            }
            if (dt.Rows.Count == 0 || UserEmail == null || UserPassword == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                cookie.Values.Add("UserId", dt.Rows[0][0].ToString());
                cookie.Values.Add("UserName", dt.Rows[0][1].ToString());
                cookie.Values.Add("UserSurname", dt.Rows[0][2].ToString());
                cookie.Values.Add("UserEmail", dt.Rows[0][3].ToString());
                cookie.Values.Add("UserPhone", dt.Rows[0][4].ToString());
                cookie.Values.Add("UserPhoto", dt.Rows[0][5].ToString());
                cookie.Values.Add("UserBonus", dt.Rows[0][6].ToString());
                cookie.Values.Add("UserBalans", dt.Rows[0][7].ToString());
                cookie.Values.Add("UserMessage", dt.Rows[0][8].ToString());
                cookie.Values.Add("UserSignDate", dt.Rows[0][9].ToString());
                cookie.Values.Add("UserGender", dt.Rows[0][10].ToString());
                Response.Cookies.Add(cookie);
                return RedirectToAction("User1");
            }
        }
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5. Create())
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
        public ActionResult ForgetPass()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();   
            ViewBag.search = db.GameType.ToList();     
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPass(string UserName, string UserEmail, string randomkod, string newpass)
        {
            DataTable dt = Sql.Exec($"Select * from Gamer where UserName=N'{UserName}'");
            if (dt.Rows.Count != 0)
            {
            if (dt.Rows[0][1].ToString() == UserName && dt.Rows[0][3].ToString() == UserEmail)
            {
                Random a = new Random();
                var kod = a.Next(100000, 9999999);
                MailMessage mesajim = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("github/a gore deyisdim.github/a gore deyisdim@hotmail.com", "github/a gore deyisdim");
                istemci.Port = 587;
                istemci.Host = "smtp.live.com";
                istemci.EnableSsl = true;
                mesajim.To.Add(UserEmail);
                mesajim.From = new MailAddress("github/a gore deyisdim.github/a gore deyisdim@hotmail.com");
                mesajim.Subject = "The Code for RePassword";
                mesajim.Body = kod.ToString();
                istemci.Send(mesajim);
                Sql.Exec($"Update  Gamer set UserRandomCode=N'{kod}',UserRandomCodeDate = GETDATE() where UserName=N'{UserName}' and UserEmail=N'{UserEmail }'");
                return RedirectToAction("NewPass");
            }
            else
            {
                return RedirectToAction("ForgetPass");
            }
            }
            else
            {
                return RedirectToAction("ForgetPass");
            }

        }

        public ActionResult NewPass()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();  
            ViewBag.search = db.GameType.ToList();     
            return View();
        }
        [HttpPost]
        public ActionResult NewPasspa(string UserRandomCode, string UserPassword, string UserName, string UserEmail)
        {
            DataTable dt = Sql.Exec($"Select * from Gamer where UserName=N'{UserName}' and UserEmail=N'{UserEmail}'");
            DateTime tarix = new DateTime();
            if (dt.Rows.Count !=0)
            {
            tarix = Convert.ToDateTime(dt.Rows[0][11]);
            DateTime cari = DateTime.Now.AddMinutes(-1);
            var l = CreateMD5("levengi" + UserPassword + "levengi");
            if (UserRandomCode == dt.Rows[0][10].ToString() && UserRandomCode != null && tarix >= cari)
            {
                Sql.Exec($"Update Gamer set UserPassword=N'{l}' where UserName=N'{UserName}' and UserEmail=N'{UserEmail}'");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ForgetPass");
            }
            }
            else
            {
                return RedirectToAction("ForgetPass");
            }
        }

        public ActionResult User1()
        {
            try
            {
            ViewBag.cn = db.MainInformation.ToList();
            ViewBag.search = db.GameType.ToList();
                //List<Gamer> gamer = new List<Gamer>();
                //var UserId = Sql.Exec($"select UserId from Gamer where UserName = N'{name}'").Rows[0][0].ToString();
                var cookie = Request.Cookies["Gamer"];
                if (cookie != null)
                {
                    var z2 = Convert.ToInt32(cookie["UserId"]);
                    List<Gamer> gamer = new List<Gamer>();
                    ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                    List<basket> basket = new List<basket>();
                    ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                    int z =Convert.ToInt32(cookie["UserId"]);
                var gamer2 = db.Gamer.SingleOrDefault(x => x.UserId == z);
                    return View(gamer2);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Message(int id, int MessageGameTypeId, string MessagePhoto, string MessageUserName, string MessageUserSurname, string MessageText)
        {
            message newmessage = new message();
            newmessage.MessageDate = DateTime.Now;
            newmessage.MessageGameTypeId = MessageGameTypeId;
            newmessage.MessagePhoto = MessagePhoto;
            newmessage.MessageUserName = MessageUserName;
            newmessage.MessageUserSurname = MessageUserSurname;
            newmessage.MessageText = MessageText;
            db.message.InsertOnSubmit(newmessage);
            db.SubmitChanges();
            return RedirectToAction("GameDetal", new { id });
        }


        public ActionResult Help()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();
                ViewBag.search = db.GameType.ToList();
                return View();
        }
        public ActionResult Rules()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList().ToList();
            }
            ViewBag.cn = db.MainInformation.ToList();
            ViewBag.search = db.GameType.ToList();
            return View();
        }

        public ActionResult Banks()
        {
            List<PaymentType> banks = new List<PaymentType>();
            ViewBag.banks = db.PaymentType.ToList();
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                ViewBag.cn = db.MainInformation.ToList();
            ViewBag.search = db.GameType.ToList();
            return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult BankTransfer()
        {
            List<PaymentType> banks = new List<PaymentType>();
            ViewBag.banks = db.PaymentType.ToList();
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                ViewBag.cn = db.MainInformation.ToList();
                ViewBag.search = db.GameType.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult BankTransferHelp()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                ViewBag.cn = db.MainInformation.ToList();
                ViewBag.search = db.GameType.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UserEdit(int id, Gamer newgamer, HttpPostedFileBase UserPhoto)
        {
            Gamer oldgamer = db.Gamer.SingleOrDefault(x => x.UserId == id);

            if (oldgamer !=null)
            {
                if (UserPhoto != null)
                {
                    string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(UserPhoto.FileName);
                    UserPhoto.SaveAs(Server.MapPath("~/Content/storage/gamer/" + Photoname));
                    oldgamer.UserPhoto = Photoname;
                }
                oldgamer.UserPhone = newgamer.UserPhone;
            oldgamer.UserGender = newgamer.UserGender;
            db.SubmitChanges();
            return RedirectToAction("LogOut");
            }
            else
            {
                return RedirectToAction("User1");
            }
        }


        public ActionResult UserPasswordEdit(int id, Gamer newgamer, string UserPassword, string NewUserPassword)
        {
            Gamer oldgamer = db.Gamer.SingleOrDefault(x => x.UserId == id);
            var Password = CreateMD5("levengi" + UserPassword + "levengi");
            var NewPassword = CreateMD5("levengi" + NewUserPassword + "levengi");

            if (oldgamer.UserPassword == Password)
            {
                oldgamer.UserPassword = NewPassword;
                db.SubmitChanges();
                return RedirectToAction("LogOut");
            }
            return RedirectToAction("User1");
        }


        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Gamer");
            cookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult basket(int id, string GameProductCount)
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z3 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z3);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z3).ToList().ToList();
            }
            var dt = db.GameProducts.SingleOrDefault(x => x.GameProductId == id);
            var z = dt.GameTypeId;
            var z2 = Convert.ToInt32(cookie["UserId"]);
            var dt2 = db.GameType.SingleOrDefault(x => x.GameTypeId == z);
            basket newbasket = new basket();
            newbasket.GameProductPrice = dt.GameProductPrice;
            newbasket.GameProductId = dt.GameProductId;
            newbasket.GameProductName = dt.GameProductName;
            newbasket.GameTypePhoto = dt2.GameTypePhoto;
            newbasket.GameProductType = dt.GameTypeId;
            newbasket.GameProductStock = dt.GameProductStock;
            newbasket.GameProductCount = Convert.ToInt32(GameProductCount);
            newbasket.GameProductBonus = dt.GameProductBonus;
            newbasket.UserId = z2;
            db.basket.InsertOnSubmit(newbasket);
            db.SubmitChanges();
            return RedirectToAction("GameDetal", new { id = z });
        }
        public ActionResult basketview()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> gamer = new List<Gamer>();
                ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                ViewBag.search = db.GameType.ToList();
                ViewBag.cn = db.MainInformation.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteBasket(int id)
        {
            db.basket.DeleteOnSubmit(db.basket.SingleOrDefault(x => x.BasketId == id));
            db.SubmitChanges();
            return RedirectToAction("basketview");
        }

        public ActionResult Messages()
        {
            try
            {
                ViewBag.cn = db.MainInformation.ToList();
                ViewBag.search = db.GameType.ToList();
                var cookie = Request.Cookies["Gamer"];
                if (cookie != null)
                {
                    var z2 = Convert.ToInt32(cookie["UserId"]);
                    List<sendingmessage> sendingmessages = new List<sendingmessage>();
                    ViewBag.sendingmessage = db.sendingmessage.Where(x => x.messageUserId == z2);
                    List<Gamer> gamer = new List<Gamer>();
                    ViewBag.gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                    List<basket> basket = new List<basket>();
                    ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                    int z = Convert.ToInt32(cookie["UserId"]);
                    var gamer2 = db.Gamer.SingleOrDefault(x => x.UserId == z);
                    return View(gamer2);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }






        public ActionResult payment(Payment newpayment, HttpPostedFileBase PaymentBill)
        {
                var cookie = Request.Cookies["Gamer"];
            if (PaymentBill != null && cookie !=null)
            {
                string Photoname = "Photo" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + Path.GetExtension(PaymentBill.FileName);
                PaymentBill.SaveAs(Server.MapPath("~/Content/storage/categories/" + Photoname));
                newpayment.PaymentBill= Photoname;
                newpayment.PaymentDate = DateTime.Now;
                var z2 = Convert.ToInt32(cookie["UserId"]);
                newpayment.PaymentUserID = z2;
                db.Payment.InsertOnSubmit(newpayment);
                db.SubmitChanges();
            }
            return RedirectToAction("BankTransfer");
        }


        public ActionResult apply(string BasketId,string UserId)
        {
            int zahir = Convert.ToInt32(UserId);
            var z = db.Total.Where(y => y.UserId == zahir).Sum(x => x.TotalType);
            var z2 = db.Total.Where(y => y.UserId == zahir).Sum(x => Convert.ToInt32(x.TotalBonus));
            var usb = db.Gamer.SingleOrDefault(x => x.UserId == zahir);
            if ( usb.UserBalans >= z)
            {
                usb.UserBalans = usb.UserBalans - z;
                usb.UserBonus = usb.UserBonus + z2;
                ViewBag.son = db.Total.Where(y => y.UserId == zahir).ToList();
                foreach (var item in ViewBag.son)
                {
                orders neworder = new orders();
                neworder.OrderUserId = zahir;
                neworder.OrderDate = DateTime.Now;
                neworder.OrderCount = item.GameProductCount;
                neworder.OrderGameProductId = item.GameProductId;
                db.orders.InsertOnSubmit(neworder);
                }
                Sql.Exec($"delete basket where UserId =N'{zahir}'");
                TempData["Message"] = "Sifarişiniz tamamlandı!";
                db.SubmitChanges();
            }
            else
            {
                TempData["Message"] = "Zəhmət olmasa balansınızı artırın!";
            }
            return RedirectToAction("basketview");
         }

        public ActionResult getgift(int id, string BonusId)
        {
            int za = Convert.ToInt32(BonusId);
            var bonus = db.Bonus.SingleOrDefault(x => x.BonusId == za);
            var user = db.Gamer.SingleOrDefault(x => x.UserId == id);
            if (bonus != null)
            {
                if (user.UserBonus >= bonus.BonusPrice && bonus.BonusCount !=0)
                {
                    user.UserBonus = user.UserBonus - bonus.BonusPrice;
                    bonus.BonusCount = bonus.BonusCount - 1;
                    db.SubmitChanges();
                    return RedirectToAction("BonusDetal", new { id=za });
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult al(int id, string GameProductCount)
        {
            var product = db.GameProducts.SingleOrDefault(x => x.GameProductId == id);
            var count = Convert.ToInt32(GameProductCount);
            var price = Convert.ToInt32(product.GameProductPrice);
            var bonus = Convert.ToInt32(product.GameProductBonus);
            var total = price * count;
            var totalbonus = count * bonus;
            var cookie = Request.Cookies["Gamer"];
            var user = Convert.ToInt32(cookie["UserId"]);
            var gamer = db.Gamer.SingleOrDefault(x => x.UserId == user);
            var userbalans = Convert.ToInt32(gamer.UserBalans);
            if (userbalans >= total)
            {
                orders neworder = new orders();
                neworder.OrderUserId = user;
                neworder.OrderGameProductId = id;
                neworder.OrderCount = count;
                neworder.OrderDate = DateTime.Now;
                gamer.UserBalans = gamer.UserBalans - total;
                gamer.UserBonus = gamer.UserBonus + totalbonus;
                TempData["Message"] = "Sifarişiniz tamamlandı!";
                db.orders.InsertOnSubmit(neworder);
            }
            else
            {
                TempData["Message"] = "Zəhmət olmasa balansınızı artırın!";
                return RedirectToAction("User1");
            }
            db.SubmitChanges();
            return RedirectToAction("User1");
        }

        public ActionResult sifarişlərim()
        {
            var cookie = Request.Cookies["Gamer"];
            if (cookie != null)
            {
                var z2 = Convert.ToInt32(cookie["UserId"]);
                List<Gamer> Gamer = new List<Gamer>();
                ViewBag.Gamer = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                List<basket> basket = new List<basket>();
                ViewBag.basket = db.basket.Where(x => x.UserId == z2).ToList();
                ViewBag.cn = db.MainInformation.ToList();
                ViewBag.search = db.GameType.ToList();
                List<orderlermain1> orders = new List<orderlermain1>();
                ViewBag.orders = db.orderlermain1.OrderByDescending(x => x.OrderId).Where(x => x.OrderUserId == z2);
                var gamer2 = db.Gamer.SingleOrDefault(x => x.UserId == z2);
                return View(gamer2);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
