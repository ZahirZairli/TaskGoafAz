using goaf.az.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace goaf.az.Controllers
{
    public class UserController : Controller
    {
        DataClassesDataContext db = new DataClassesDataContext();
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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult SignUp(string UserPassword,string UserName, Gamer newgamer )
        //{
        //    DataTable dt = Sql.Exec($"Select * from Gamer where UserName=N'{UserName}'");
        //    if (dt.Rows.Count == 0)
        //    {
        //       newgamer.UserPassword = CreateMD5("levengi" + UserPassword + "levengi");
        //        db.Gamer.InsertOnSubmit(newgamer);
        //        //Sql.Exec($"Insert into Users(UserName,UserPassword,UserEmail) Values(N'{username}',CONVERT(VARCHAR(32), HashBytes('MD5', 'levengi' + N'{password}' + 'levengi'), 2),N'{email}')");
        //        return RedirectToAction("~/Site/Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
    }
}