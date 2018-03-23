using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NanoSoftExam.Controllers
{
    public class LogicalTestController : Controller
    {
        //
        // GET: /LogicalTest/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create()
        {
            string number = Request["inputtext"];
            ViewBag.result = Sum(number);

            return View("Index");
        }

        public static string Sum(string number)
        {
            int lineNumber = 100;
            int digitNumber = 50;
            int[,] numberArray = new int[lineNumber, digitNumber];
            int[] num = new int[digitNumber];
            string[] lines = GetLines(number);
            for (int i = 0; i < lines.Length; i++)
            {
                char[] chars = lines[i].ToCharArray();
                for (int j = 0; j < chars.Length; j++)
                {
                    numberArray[i, j] = Convert.ToInt32(chars[j].ToString());
                }
            }
            for (int i = 0; i < numberArray.GetLength(1); i++)
            {
                int x = 0;
                for (int j = 0; j < numberArray.GetLength(0); j++)
                {
                    x += numberArray[j, i];
                }
                num[i] = x;
            }
            int div = 0;
            string finalNumber = "";
            for (int i = num.Length - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    int n = num[i] + div;
                    div = n / 10;
                    var reminder = n % 10;
                    finalNumber = reminder + finalNumber;
                }
                else
                {
                    int n = num[i] + div;
                    finalNumber = n + finalNumber;
                }

            }
            return finalNumber;
        }
        public static String[] GetLines(string numbers)
        {
            return numbers.Replace(" ", "").Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
