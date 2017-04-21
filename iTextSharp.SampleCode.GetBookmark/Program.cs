using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.SampleCode.GetBookmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var _srcPath = "../../NET Platform Guide.pdf";
            var _buffer = File.ReadAllBytes(_srcPath);
            
            var _json = string.Empty;

            using(PdfReader _reader = new PdfReader(_buffer))
            {
                var _bookmarks = SimpleBookmark.GetBookmark(_reader);
                _json = JsonConvert.SerializeObject(_bookmarks);
            }

            var _jArray = JArray.Parse(_json);
            

            Console.WriteLine("-----------------------");
            Console.WriteLine("此份文件書籤");
            Console.WriteLine("-----------------------");
            Console.WriteLine(_jArray.ToString());
            Console.WriteLine();

            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }

    /// <summary> Pdf 書籤資料的物件</summary>
    public class Bookmark
    {
        public string Title { get;set; }
        public string Open { get; set; }
        public string Page { get; set; }
        public string Action { get; set; }
        public Bookmark Kids { get; set; }

        /// <summary>取得書籤代表的頁面</summary>
        public int PageNumber
        {
            get
            {
                if(string.IsNullOrWhiteSpace(this.Page) == true)
                {
                    return 0;
                }

                var _cells = this.Page.Split(new char[]{' '});
                var _number = _cells.FirstOrDefault();

                return Convert.ToInt32(_number);
            }
        }
    }
}

