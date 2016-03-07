using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MusicDog.Models;
using System.Collections.Generic;
using MusicDog.Handler;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace MusicDog.Controllers
{
    public class WebSongsController : Controller
    {
        private WebSongContext _context;

        public List<WebSong> WebSongs { get; set; } = new List<WebSong>();

        public WebSongsController(WebSongContext context)
        {
            _context = context;    
        }

        // GET: WebSongs
        public IActionResult Index()
        {
            return View(_context.WebSong.ToList());
        }

        //GET: BandList
        public async Task<IActionResult> BandList(int? id)
        {
            if (id == null)
                return HttpNotFound();
            SongResponseBandList resSongs1 = new SongResponseBandList();
            await Task.Run(() =>
            {
                var task1 = HttpReqProxy.GetBandListAsync(id.Value); 
                resSongs1 = task1.GetAwaiter().GetResult();
                // http请求返回错误
                if (resSongs1.showapi_res_code == -1)
                {
                    throw new HttpRequestException(resSongs1.showapi_res_error);
                }
            });
            SongFileManager.SetSongListByBand(WebSongs, resSongs1.showapi_res_body.pagebean.songlist);

            ViewData["rankname"] = RankNameFromId(id.Value);
            return View(WebSongs);
        }

        public async Task<IActionResult> LyricInfo(int? id ,string title=null, string artist=null ,string picurl=null)
        {
            if (id == null)
                return HttpBadRequest();
            //编码
            SongResponseById detailedSong = new SongResponseById();
            WebSong song = new WebSong();
            await Task.Run(() =>
            {
                var task = HttpReqProxy.GetSongByIdAsync(id.Value.ToString());
                detailedSong = task.GetAwaiter().GetResult();
                if (detailedSong.showapi_res_code == -1)
                    throw new HttpRequestException(detailedSong.showapi_res_error);
            });
            SongFileManager.SetSongById(song, detailedSong.showapi_res_body);
            song.Title = title;
            song.Artist = artist;
            song.Albumpic_big = picurl;
            return View(song);
        }

        //GET: Search
        [HttpGet]
        public async Task<IActionResult> Search(string content)
        {
            if (string.IsNullOrEmpty(content))
                return View();
            SongResponseByName resSongs = new SongResponseByName();
            await Task.Run(() =>
            {
                var task = HttpReqProxy.GetSongByNameAsync(content);
                resSongs = task.GetAwaiter().GetResult();
                // http请求返回错误
                if (resSongs.showapi_res_code == -1)
                {
                    throw new HttpRequestException(resSongs.showapi_res_error);
                }
            });
            SongFileManager.SetSongListByName(WebSongs, resSongs.showapi_res_body.pagebean.contentlist);
            ViewData["content"] = content;
            return View(WebSongs);
        }

        // GET: WebSongs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            WebSong webSong = _context.WebSong.Single(m => m.Id == id);
            if (webSong == null)
            {
                return HttpNotFound();
            }

            return View(webSong);
        }

        // GET: WebSongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebSongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WebSong webSong)
        {
            if (ModelState.IsValid)
            {
                _context.WebSong.Add(webSong);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webSong);
        }

        // GET: WebSongs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            WebSong webSong = _context.WebSong.Single(m => m.Id == id);
            if (webSong == null)
            {
                return HttpNotFound();
            }
            return View(webSong);
        }

        // POST: WebSongs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WebSong webSong)
        {
            if (ModelState.IsValid)
            {
                _context.Update(webSong);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webSong);
        }

        // GET: WebSongs/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            WebSong webSong = _context.WebSong.Single(m => m.Id == id);
            if (webSong == null)
            {
                return HttpNotFound();
            }

            return View(webSong);
        }

        // POST: WebSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            WebSong webSong = _context.WebSong.Single(m => m.Id == id);
            _context.WebSong.Remove(webSong);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string RankNameFromId(int id)
        {
            switch(id)
            {
                case 3:
                    return "欧美榜单";
                case 5:
                    return "大陆榜单";
                case 6:
                    return "港台榜单";
                case 16:
                    return "韩国榜单";
                case 17:
                    return "日本榜单";
                case 18:
                    return "民谣榜单";
                case 19:
                    return "摇滚榜单";
                case 23:
                    return "销量榜单";
                case 26:
                    return "热歌榜单";
                default:
                    return "";

            }
        }
    }
}
