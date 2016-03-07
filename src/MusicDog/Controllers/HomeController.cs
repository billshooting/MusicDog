using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MusicDog.ViewModels.Song;
using MusicDog.Handler;
using MusicDog.Models;
using System.Net.Http;

namespace MusicDog.Controllers
{
    public class HomeController : Controller
    {
        private HomeIndexBandList homeSongDb = new HomeIndexBandList();
        public async Task<IActionResult> Index()
        {
            SongResponseBandList resSongs1 = new SongResponseBandList();
            SongResponseBandList resSongs2 = new SongResponseBandList();
            SongResponseBandList resSongs3 = new SongResponseBandList();
            await Task.Run(() =>
            {
                var task1 = HttpReqProxy.GetBandListAsync(5); //内地
                resSongs1= task1.GetAwaiter().GetResult();
                // http请求返回错误
                if (resSongs1.showapi_res_code == -1)
                {
                    throw new HttpRequestException(resSongs1.showapi_res_error);
                }

                var task2 = HttpReqProxy.GetBandListAsync(5); //内地
                resSongs2 = task2.GetAwaiter().GetResult();
                // http请求返回错误
                if (resSongs2.showapi_res_code == -1)
                {
                    throw new HttpRequestException(resSongs2.showapi_res_error);
                }

                var task3 = HttpReqProxy.GetBandListAsync(5); //内地
                resSongs3 = task3.GetAwaiter().GetResult();
                // http请求返回错误
                if (resSongs3.showapi_res_code == -1)
                {
                    throw new HttpRequestException(resSongs3.showapi_res_error);
                }
            });
            SongFileManager.SetSongListByBand(homeSongDb.Band1List, resSongs1.showapi_res_body.pagebean.songlist);
            SongFileManager.SetSongListByBand(homeSongDb.Band2List, resSongs2.showapi_res_body.pagebean.songlist);
            SongFileManager.SetSongListByBand(homeSongDb.Band3List, resSongs3.showapi_res_body.pagebean.songlist);
            return View(homeSongDb);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
