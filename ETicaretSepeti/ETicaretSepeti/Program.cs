using System;
using System.Collections.Generic;

namespace ETicaretSepeti
{
    public abstract class Urun
    {
        public string urunAdi { get; set; }
        public decimal fiyat { get; set; }
        public int stokAdedi { get; set; }
        public decimal agirlik { get; set; }
        public string paraBirimi { get; set; }

        public abstract string kategori { get; }
        public abstract bool yasSiniriVarMi { get; }

        public abstract decimal VergiHesapla();
    }

    public class Elektronik : Urun
    {
        public override string kategori => "Elektronik";
        public override bool yasSiniriVarMi => false;

        public override decimal VergiHesapla() { return 0.20m; }
    }

    public class Kitap : Urun
    {
        public override string kategori => "Kitap";
        public override bool yasSiniriVarMi => false;

        public override decimal VergiHesapla() { return 0.0m; }
    }

    public class Yiyecek : Urun
    {
        public override string kategori => "Yiyecek";
        public override bool yasSiniriVarMi => true;

        public override decimal VergiHesapla() { return 0.10m; }
    }

    public class Motorsiklet : Urun
    {
        public override string kategori => "Motorsiklet";
        public override bool yasSiniriVarMi => false;

        public override decimal VergiHesapla() { return 0.18m; }
    }

    public static class UrunFactory
    {
        public static Urun UrunOlustur(string urunTipi, string isim, decimal fiyat, string paraBirimi, int stok, decimal agirlik)
        {
            Urun urun;
            switch (urunTipi.ToLower())
            {
                case "elektronik": urun = new Elektronik(); break;
                case "kitap": urun = new Kitap(); break;
                case "yiyecek": urun = new Yiyecek(); break;
                case "motorsiklet": urun = new Motorsiklet(); break;
                default: throw new ArgumentException("Böyle bir ürün tipi yok");
            }
            urun.urunAdi = isim;
            urun.fiyat = fiyat;
            urun.paraBirimi = paraBirimi;
            urun.stokAdedi = stok;
            urun.agirlik = agirlik;
            return urun;
        }
    }

    public abstract class UrunDecorator : Urun
    {
        protected Urun _urun;

        public UrunDecorator(Urun urun)
        {
            _urun = urun;
            urunAdi = urun.urunAdi;
            fiyat = urun.fiyat;
            stokAdedi = urun.stokAdedi;
            agirlik = urun.agirlik;
            paraBirimi = urun.paraBirimi;
        }

        public override string kategori => _urun.kategori;
        public override bool yasSiniriVarMi => _urun.yasSiniriVarMi;
    }

    public class HediyePaketiDecorator : UrunDecorator
    {
        public HediyePaketiDecorator(Urun urun) : base(urun)
        {
            urunAdi = urun.urunAdi + " [Hediye Paketli]";
        }
        public override decimal VergiHesapla() { return _urun.VergiHesapla() + 0.02m; }
    }

    public class EkstraSigortaDecorator : UrunDecorator
    {
        public EkstraSigortaDecorator(Urun urun) : base(urun)
        {
            urunAdi = urun.urunAdi + " (Ekstra Sigortalı)";
        }
        public override decimal VergiHesapla() { return _urun.VergiHesapla() + 0.05m; }
    }

    public interface IKargoServisi { void TeslimEt(string musteriAdi, string adres); }

    public class ArasKargoSistemi
    {
        public void KargoGonder(string tamAdres) { Console.WriteLine("Aras Kargo -> " + tamAdres + " adresine gönderildi."); }
    }

    public class ArasKargoAdapter : IKargoServisi
    {
        private ArasKargoSistemi _arasKargo = new ArasKargoSistemi();
        public void TeslimEt(string musteriAdi, string adres) { _arasKargo.KargoGonder(musteriAdi + " - " + adres); }
    }

    public interface IIndirimStratejisi
    {
        decimal IndirimUygula(decimal tutar);
    }

    public class VipIndirimStratejisi : IIndirimStratejisi
    {
        public decimal IndirimUygula(decimal tutar) => tutar * 0.80m;
    }

    public class OgrenciIndirimStratejisi : IIndirimStratejisi
    {
        public decimal IndirimUygula(decimal tutar) => tutar * 0.90m;
    }

    public class IndirimYokStratejisi : IIndirimStratejisi
    {
        public decimal IndirimUygula(decimal tutar) => tutar;
    }

    public interface IBildirimGozlemcisi
    {
        void BildirimAl(string mesaj);
    }

    public class EmailBildirim : IBildirimGozlemcisi
    {
        public void BildirimAl(string mesaj) { Console.WriteLine("📧 EMAIL GÖNDERİLDİ: " + mesaj); }
    }

    public class SmsBildirim : IBildirimGozlemcisi
    {
        public void BildirimAl(string mesaj) { Console.WriteLine("📱 SMS GÖNDERİLDİ: " + mesaj); }
    }

    public class Sepet
    {
        public List<Urun> urunler = new List<Urun>();

        private IIndirimStratejisi _indirimStratejisi = new IndirimYokStratejisi();

        public bool kurumsalMi { get; set; }
        public int musteriYasi { get; set; }
        public string kuponKodu { get; set; }
        public string odemeTuru { get; set; }
        public string hedefParaBirimi { get; set; } = "TRY";

        public void IndirimStratejisiBelirle(IIndirimStratejisi strateji)
        {
            _indirimStratejisi = strateji;
        }

        public void UrunEkle(Urun urun)
        {
            if (urun.stokAdedi <= 0) { Console.WriteLine("Ürün stokta yok"); return; }
            if (urun.yasSiniriVarMi && musteriYasi < 18) { Console.WriteLine("Bu ürün için yaşınız yetmiyor"); return; }

            urunler.Add(urun);
            Console.WriteLine(urun.urunAdi + " sepete eklendi");
        }

        public decimal ToplamHesapla()
        {
            decimal toplam = 0;
            decimal toplamAgirlik = 0;
            decimal vergiToplami = 0;

            foreach (var urun in urunler)
            {
                decimal tlFiyat = urun.fiyat;
                if (urun.paraBirimi == "USD") tlFiyat = urun.fiyat * 35.50m;
                else if (urun.paraBirimi == "EUR") tlFiyat = urun.fiyat * 38.20m;

                toplam += tlFiyat;
                toplamAgirlik += urun.agirlik;

                if (kurumsalMi) vergiToplami += (urun.kategori == "Elektronik") ? tlFiyat * 0.15m : tlFiyat * 0.10m;
                else vergiToplami += tlFiyat * urun.VergiHesapla();
            }

            toplam += vergiToplami;

            toplam = _indirimStratejisi.IndirimUygula(toplam);

            if (kuponKodu == "YAZ2026") toplam -= 50;

            decimal kargo = (toplam > 1000) ? 0 : 40;
            if (toplamAgirlik > 5) kargo += 20;
            toplam += kargo;

            if (odemeTuru == "CreditCard") toplam += toplam * 0.02m;

            return (hedefParaBirimi == "USD") ? toplam / 35.50m : toplam;
        }
    }

    public class ETicaretFacade
    {
        private Sepet _sepet;
        private IKargoServisi _kargoServisi;
        private List<IBildirimGozlemcisi> _gozlemciler = new List<IBildirimGozlemcisi>();

        public ETicaretFacade()
        {
            _sepet = new Sepet();
            _kargoServisi = new ArasKargoAdapter();

            _sepet.kurumsalMi = false;
            _sepet.musteriYasi = 22;
            _sepet.kuponKodu = "YAZ2026";
            _sepet.odemeTuru = "CreditCard";

            _sepet.IndirimStratejisiBelirle(new VipIndirimStratejisi());
        }

        public void GozlemciEkle(IBildirimGozlemcisi gozlemci)
        {
            _gozlemciler.Add(gozlemci);
        }

        private void SiparisTamamlandiBildir(string mesaj)
        {
            foreach (var gozlemci in _gozlemciler)
            {
                gozlemci.BildirimAl(mesaj);
            }
        }

        public void SiparisOlustur()
        {
            Urun laptop = UrunFactory.UrunOlustur("elektronik", "RTX 4050 Laptop", 1200, "USD", 3, 2.5m);
            laptop = new EkstraSigortaDecorator(laptop);
            _sepet.UrunEkle(laptop);

            Urun kitap = UrunFactory.UrunOlustur("kitap", "Linux Kitabi", 250, "TRY", 5, 1);
            kitap = new HediyePaketiDecorator(kitap);
            _sepet.UrunEkle(kitap);

            decimal toplam = _sepet.ToplamHesapla();
            Console.WriteLine("\nToplam fiyat: " + Math.Round(toplam, 2) + " TRY");

            _kargoServisi.TeslimEt("Mesut Mustafa", "Konya Teknik Üniversitesi");

            SiparisTamamlandiBildir("Siparişiniz başarıyla alındı ve kargoya teslim edildi. Tutar: " + Math.Round(toplam, 2));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ETicaretFacade facade = new ETicaretFacade();
            facade.GozlemciEkle(new EmailBildirim());
            facade.GozlemciEkle(new SmsBildirim());
            facade.SiparisOlustur();
        }
    }
}