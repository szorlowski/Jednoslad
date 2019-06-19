using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jednoslad2.Controllers
{
	public class HomeController : Controller
	{
		jednosladEntities context = new jednosladEntities();
		public static klienci k;
		public ActionResult Index()
		{
			return View();
		}

	
		public ActionResult Rejestracja()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Zaloguj(string login, string haslo)
		{
			var lista = context.kliencis.Where(k => k.login == login);
			if(lista.Count() > 0)
			{
				var klient = lista.First();
				if (klient.haslo == haslo)
				{
					k = klient;
					return RedirectToAction("Potwierdzenie", klient);
				}
				return View();
			}
			else
			{
				return View();
			}

		}

		[HttpGet]
		public ActionResult Zaloguj()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Rejestracja(string imie, string nazwisko, string login, string haslo)
		{
			k = new klienci();
			k.imie = imie;
			k.nazwisko = nazwisko;
			k.login = login;
			k.haslo = haslo;
			context.kliencis.Add(k);
			context.SaveChanges();
			return RedirectToAction("Potwierdzenie", k);
		}

		public ActionResult Potwierdzenie(klienci klient)
		{
			ViewBag.Zarejestrowany = klient;
			return View();
		}

		[HttpGet]
		public ActionResult ListaMotocykli()
		{

			ViewBag.Motocykle = context.motocykles.Where(m => true);
			return View();
		}

		[HttpPost]
		public ActionResult ListaMotocykli(string marka, Decimal? pojemnosc, Boolean? all, string command, int? idMotocykla)
		{
			if(command == "Szukaj")
			{
				IQueryable<motocykle> lista = context.motocykles.Where(m => true);
				ViewBag.Motocykle = lista;

				if (all != null || all == true)
				{
					return View();
				}

				if (marka == null || marka != "")
				{
					lista = lista.Where(m => m.marka == marka);
				}

				if (pojemnosc != null)
				{
					lista = lista.Where(m => m.pojemnosc == pojemnosc);
				}
				ViewBag.Motocykle = lista;

				return View();
			}
			else
			{
				if (k == null)
				{
					return RedirectToAction("Zaloguj");
				}
				else
				{
					return RedirectToAction("ZakupForm", new { id = idMotocykla });
				}
			}

		}

		[HttpGet]
		public ActionResult ZakupForm(int id)
		{
			ViewBag.motocykl = context.motocykles.Where(m => m.id == id).First();
			ViewBag.klient = k;
			return View();
		}

		[HttpPost]
		public ActionResult ZakupForm(int? idKlienta, int? idMotocykla)
		{
			transakcje t = new transakcje();
			t.id_klienta = idKlienta;
			t.id_motocykla = idMotocykla;
			t.data_wypo = DateTime.Now;
			context.transakcjes.Add(t);
			context.SaveChanges();
			return RedirectToAction("Transakcje");
		}

		public ActionResult Transakcje()
		{
			if(k == null)
			{
				return RedirectToAction("Zaloguj");
			}
			else
			{
				ViewBag.klient = k;
				ViewBag.transakcje = context.transakcjes.Where(t => t.id_klienta == k.id);
				return View();
			}
		}
	}
}