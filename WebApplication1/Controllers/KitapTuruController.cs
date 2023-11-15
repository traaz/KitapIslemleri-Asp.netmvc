using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.NewFolder;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapTuruController(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> kitapTurleriList = _uygulamaDbContext.KitapTurleri.ToList();
            return View(kitapTurleriList);
        }
        //viewi olusturmak icin action
        public IActionResult KitapTuruEkle()
        {
            return View();
        }
        //post metodu olusturmak icin action
        [HttpPost]
        public IActionResult KitapTuruEkle(KitapTuru kitapTuru)
        {
           

            if (ModelState.IsValid) //modelstate entityedik required alanlaarı vs.
            {
                var kontrol = _uygulamaDbContext.KitapTurleri.FirstOrDefault(x => x.Ad == kitapTuru.Ad);
                if(kontrol == null)
                {
                    _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
                    _uygulamaDbContext.SaveChanges();

                    return RedirectToAction("Index", "KitapTuru");
                }
                else
                {
                    TempData["basarisiz"] = "Kitap Türü Mevcut";
                    return View();
                }
               
            }
          
            
            return View();
            
        }


        public IActionResult Guncelle(int id)
        {
            KitapTuru kitapTuru = _uygulamaDbContext.KitapTurleri.Find(id); 
            if(kitapTuru == null)
            {
                return NotFound();
            }
            return View(kitapTuru);
        }
        //post metodu olusturmak icin action
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {


            if (ModelState.IsValid) //modelstate entityedik required alanlaarı vs.
            {
                var kontrol = _uygulamaDbContext.KitapTurleri.FirstOrDefault(x => x.Ad == kitapTuru.Ad);
                if (kontrol == null)
                {
                    _uygulamaDbContext.KitapTurleri.Update(kitapTuru);
                    _uygulamaDbContext.SaveChanges();

                    return RedirectToAction("Index", "KitapTuru");
                }
                else
                {
                    TempData["basarisiz"] = "Kitap Türü Mevcut";
                    return View();
                }

            }


            return View();

        }

        public IActionResult Sil(int id)
        {
            KitapTuru kitapTuru = _uygulamaDbContext.KitapTurleri.Find(id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            return View(kitapTuru);
        }
        //post metodu olusturmak icin action
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int id)
        {

            KitapTuru kitapTuru = _uygulamaDbContext.KitapTurleri.Find(id);
            _uygulamaDbContext.Remove(kitapTuru);
            _uygulamaDbContext.SaveChanges();
            return RedirectToAction("Index", "KitapTuru");
            

        }








    }
}
