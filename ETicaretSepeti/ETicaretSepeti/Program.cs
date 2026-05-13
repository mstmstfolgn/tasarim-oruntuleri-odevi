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

        public override decimal VergiHesapla()
        {
            return 0.20m;
        }
    }

    public class Kitap : Urun
    {
        public override string kategori => "Kitap";

        public override bool yasSiniriVarMi => false;

        public override decimal VergiHesapla()
        {
            return 0.0m;
        }
    }

    public class Yiyecek : Urun
    {
        public override string kategori => "Yiyecek";

        public override bool yasSiniriVarMi => true;

        public override decimal VergiHesapla()
        {
            return 0.10m;
        }
    }

    public class Motorsiklet : Urun
    {
        public override string kategori => "Motorsiklet";

        public override bool yasSiniriVarMi => false;

        public override decimal VergiHesapla()
        {
            return 0.18m;
        }
    }

    public static class UrunFactory
    {
        public static Urun UrunOlustur(
            string urunTipi,
            string isim,
            decimal fiyat,
            string paraBirimi,
            int stok,
            decimal agirlik)
        {
            Urun urun;

            switch (urunTipi.ToLower())
            {
                case "elektronik":
                    urun = new Elektronik();
                    break;

                case "kitap":
                    urun = new Kitap();
                    break;

                case "yiyecek":
                    urun = new Yiyecek();
                    break;

                case "motorsiklet":
                    urun = new Motorsiklet();
                    break;

                default:
                    throw new ArgumentException("Böyle bir ürün tipi yok");
            }

            urun.urunAdi = isim;
            urun.fiyat = fiyat;
            urun.paraBirimi = paraBirimi;
            urun.stokAdedi = stok;
            urun.agirlik = agirlik;

            return urun;
        }
    }

    public class Sepet
    {
        public List<Urun> urunler = new List<Urun>();

        public string musteriTipi { get; set; }

        public bool kurumsalMi { get; set; }

        public int musteriYasi { get; set; }

        public string kuponKodu { get; set; }

        public string odemeTuru { get; set; }

        public string sehir { get; set; }

        public string hedefParaBirimi { get; set; } = "TRY";

        public bool hediyePaketi { get; set; }

        public void UrunEkle(Urun urun)
        {
            if (urun.stokAdedi <= 0)
            {
                Console.WriteLine("Ürün stokta yok");
                return;
            }

            if (urun.yasSiniriVarMi && musteriYasi < 18)
            {
                Console.WriteLine("Bu ürün için yaşınız yetmiyor");
                return;
            }

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

                if (urun.paraBirimi == "USD")
                {
                    tlFiyat = urun.fiyat * 35.50m;
                }
                else if (urun.paraBirimi == "EUR")
                {
                    tlFiyat = urun.fiyat * 38.20m;
                }

                toplam += tlFiyat;

                toplamAgirlik += urun.agirlik;

                if (kurumsalMi)
                {
                    if (urun.kategori == "Elektronik")
                    {
                        vergiToplami += tlFiyat * 0.15m;
                    }
                    else
                    {
                        vergiToplami += tlFiyat * 0.10m;
                    }
                }
                else
                {
                    vergiToplami += tlFiyat * urun.VergiHesapla();
                }
            }

            toplam += vergiToplami;

            if (musteriTipi == "Student")
            {
                toplam = toplam * 0.90m;
            }
            else if (musteriTipi == "VIP")
            {
                toplam = toplam * 0.80m;
            }

            if (kuponKodu == "YAZ2026")
            {
                toplam -= 50;
            }

            decimal kargo = 40;

            if (toplam > 1000)
            {
                kargo = 0;
            }

            if (toplamAgirlik > 5)
            {
                kargo += 20;
            }

            toplam += kargo;

            if (hediyePaketi)
            {
                toplam += 25;
            }

            if (odemeTuru == "CreditCard")
            {
                toplam += toplam * 0.02m;
            }

            decimal sonTutar = toplam;

            if (hedefParaBirimi == "USD")
            {
                sonTutar = toplam / 35.50m;
            }

            return sonTutar;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Sepet sepet = new Sepet();

            sepet.musteriTipi = "VIP";
            sepet.kurumsalMi = false;
            sepet.musteriYasi = 22;
            sepet.kuponKodu = "YAZ2026";
            sepet.odemeTuru = "CreditCard";
            sepet.hediyePaketi = true;

            sepet.UrunEkle(
                UrunFactory.UrunOlustur(
                    "elektronik",
                    "RTX 4050 Laptop",
                    1200,
                    "USD",
                    3,
                    2.5m
                )
            );

            sepet.UrunEkle(
                UrunFactory.UrunOlustur(
                    "kitap",
                    "Linux Kitabi",
                    250,
                    "TRY",
                    5,
                    1
                )
            );

            Console.WriteLine("Toplam fiyat: " + Math.Round(sepet.ToplamHesapla(), 2));
        }
    }
}