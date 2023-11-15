using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.NewFolder;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
   
    public class KitapController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapController(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
        }
        public IActionResult Index()
        {
            List<Kitap> kitapList = _uygulamaDbContext.Kitaplar.Include(k=>k.KitapTuru).ToList();
            return View(kitapList);
        }
        //viewi olusturmak icin action
        public IActionResult KitapEkle()
        {
            IEnumerable<SelectListItem> kitapTuruList = _uygulamaDbContext.KitapTurleri.ToList()
                .Select(kitapTur => new SelectListItem
                {
                    Text = kitapTur.Ad,
                    Value = kitapTur.Id.ToString()
                });
            ViewBag.KitapTuruList = kitapTuruList;
                
            return View();
        }
        //post metodu olusturmak icin action
        [HttpPost]
        public IActionResult KitapEkle(Kitap kitap)
        {


            if (ModelState.IsValid) //modelstate entityedik required alanlaarı vs.
            {
                var kontrol = _uygulamaDbContext.Kitaplar.FirstOrDefault(x => x.KİtapAdi == kitap.KİtapAdi);
                if (kontrol == null)
                {
                    _uygulamaDbContext.Kitaplar.Add(kitap);
                    _uygulamaDbContext.SaveChanges();

                    return RedirectToAction("Index", "Kitap");
                }
                else
                {
                    TempData["basarisiz"] = "Kitap Mevcut";
                    return View();
                }

            }


            return View();

        }


        public IActionResult Guncelle(int id)
        {
            IEnumerable<SelectListItem> kitapTuruList = _uygulamaDbContext.KitapTurleri.ToList()
               .Select(kitapTur => new SelectListItem
               {
                   Text = kitapTur.Ad,
                   Value = kitapTur.Id.ToString()
               });
            ViewBag.KitapTuruList = kitapTuruList;
            Kitap kitap = _uygulamaDbContext.Kitaplar.Find(id);
            if (kitap == null)
            {
                return NotFound();
            }
            return View(kitap);
        }
        //post metodu olusturmak icin action
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {


            if (ModelState.IsValid) //modelstate entityedik required alanlaarı vs.
            {
                
                    _uygulamaDbContext.Kitaplar.Update(kitap);
                    _uygulamaDbContext.SaveChanges();

                    return RedirectToAction("Index", "Kitap");
                }
              

            return View();

        }

        public IActionResult Sil(int id)
        {
            Kitap kitap = _uygulamaDbContext.Kitaplar.Find(id);
            if (kitap == null)
            {
                return NotFound();
            }
            return View(kitap);
        }
        //post metodu olusturmak icin action
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPost(int id)
        {

            Kitap kitap = _uygulamaDbContext.Kitaplar.Find(id);
            _uygulamaDbContext.Remove(kitap);
            _uygulamaDbContext.SaveChanges();
            return RedirectToAction("Index", "Kitap");


        }



    }
}
