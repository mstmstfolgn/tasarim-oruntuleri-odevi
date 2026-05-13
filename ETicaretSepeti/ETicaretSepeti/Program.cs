using System;
using System.Collections.Generic;

namespace ETicaretSepeti
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public decimal WeightKg { get; set; } // Kargo için ağırlık eklendi
        public bool IsAgeRestricted { get; set; } // Yaş sınırı olan ürünler için
        public string Currency { get; set; } = "TRY"; // Ürün bazlı döviz tipi
    }

    public class ShoppingCart
    {
        public List<Product> Items { get; set; } = new List<Product>();
        public string CustomerType { get; set; } // "Standard", "Premium", "Student", "VIP", "Employee"
        public bool IsCorporate { get; set; } // Kurumsal / Bireysel ayrımı
        public int CustomerAge { get; set; }
        public string CouponCode { get; set; }
        public string PaymentMethod { get; set; } // "CreditCard", "BankTransfer", "CashOnDelivery"
        public string ShippingCity { get; set; }
        public string TargetCurrency { get; set; } = "TRY"; // Sepet hangi kurda ödenecek?
        public bool WantsGiftWrap { get; set; }

        public void AddItem(Product product)
        {
            // Sepete eklerken yapılan saçma sapan kontroller
            if (product.StockQuantity > 0)
            {
                if (product.IsAgeRestricted && CustomerAge < 18)
                {
                    Console.WriteLine("HATA: " + product.Name + " için yaşınız tutmuyor (+18)!");
                }
                else
                {
                    Items.Add(product);
                    Console.WriteLine("[LOG] " + DateTime.Now + " - " + product.Name + " sepete eklendi.");
                }
            }
            else
            {
                Console.WriteLine("[LOG] HATA: " + product.Name + " stokta yok!");
            }
        }

        // Bütün şirketin kaderini belirleyen o korkunç Tanrı Metot
        public decimal CalculateTotal()
        {
            decimal totalTry = 0;
            decimal totalWeight = 0;
            decimal taxTotal = 0;

            Console.WriteLine("--- Karmaşık Sepet Hesaplaması Başlıyor ---");

            // 1. Ürün fiyatlarını kura göre çevir ve vergileri hesapla
            foreach (var item in Items)
            {
                decimal itemPriceInTry = item.Price;

                // Spagetti Döviz Çevirici (Sürekli güncellenmesi gereken sabit kodlar)
                if (item.Currency == "USD") itemPriceInTry = item.Price * 35.50m;
                else if (item.Currency == "EUR") itemPriceInTry = item.Price * 38.20m;

                totalTry += itemPriceInTry;
                totalWeight += item.WeightKg;

                // Şirket türüne ve kategoriye göre karmaşık vergi hesaplaması
                if (IsCorporate)
                {
                    // Kurumsallara KDV indirimi falan filan
                    if (item.Category == "Electronics") taxTotal += itemPriceInTry * 0.15m;
                    else if (item.Category == "Books") taxTotal += itemPriceInTry * 0.0m;
                    else taxTotal += itemPriceInTry * 0.10m;
                }
                else
                {
                    // Bireysel vergiler
                    if (item.Category == "Electronics") taxTotal += itemPriceInTry * 0.20m;
                    else if (item.Category == "Clothing") taxTotal += itemPriceInTry * 0.10m;
                    else taxTotal += itemPriceInTry * 0.18m;
                }
            }

            totalTry += taxTotal;
            Console.WriteLine("Vergiler dahil ara toplam: " + totalTry + " TL");

            // 2. Müşteri tipine göre sonsuz indirim zinciri
            if (CustomerType == "Student") totalTry = totalTry * 0.90m;
            else if (CustomerType == "Premium") totalTry = totalTry * 0.85m;
            else if (CustomerType == "VIP") totalTry = totalTry * 0.80m;
            else if (CustomerType == "Employee") totalTry = totalTry * 0.70m;

            // Gece Kuşu İndirimi (Saat 00:00 - 06:00 arası siparişlerde %5 ekstra)
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 6)
            {
                totalTry = totalTry * 0.95m;
                Console.WriteLine("Gece Kuşu %5 indirimi uygulandı!");
            }

            // 3. Kupon Kodu Kontrolleri
            if (CouponCode == "YAZ2026") totalTry -= 50;
            else if (CouponCode == "TEKNOLOJI20")
            {
                decimal techDiscount = 0;
                foreach (var item in Items)
                {
                    if (item.Category == "Electronics") techDiscount += item.Price * 0.20m;
                }
                totalTry -= techDiscount;
            }
            else if (CouponCode == "HOSGELDIN" && totalTry > 200) totalTry -= 30;

            // 4. Kargo Hesaplaması (Ağırlık ve Şehir Bazlı)
            decimal shippingCost = 0;
            if (totalTry > 1000 || CouponCode == "KARGOBEDAVA")
            {
                Console.WriteLine("Kargo Bedava!");
            }
            else
            {
                shippingCost = 40; // Temel ücret

                // Ağırlık başına ekstra ücret
                if (totalWeight > 5) shippingCost += (totalWeight - 5) * 5;

                // Şehre göre saçma bir kontrol
                if (ShippingCity == "Kars" || ShippingCity == "Hakkari" || ShippingCity == "Şırnak")
                {
                    shippingCost += 25; // Uzak bölge ek ücreti
                }
                totalTry += shippingCost;
                Console.WriteLine("Ağırlık ve bölge hesaplamalı kargo eklendi: " + shippingCost + " TL");
            }

            // 5. Hediye Paketi
            if (WantsGiftWrap)
            {
                bool canWrap = true;
                foreach (var item in Items)
                {
                    if (item.WeightKg > 10 || item.Category == "WhiteGoods") canWrap = false;
                }

                if (canWrap)
                {
                    totalTry += 25; // Paket ücreti
                    Console.WriteLine("Hediye paketi ücreti eklendi.");
                }
                else
                {
                    Console.WriteLine("HATA: Sepetteki bazı ürünler çok büyük, heiye paketi yapılamaz!");
                }
            }

            // 6. Ödeme Yöntemi komisyonları
            if (PaymentMethod == "CreditCard") totalTry += totalTry * 0.02m;
            else if (PaymentMethod == "CashOnDelivery") totalTry += 15;
            else if (PaymentMethod == "BankTransfer") totalTry -= totalTry * 0.01m;

            // 7. En son sepeti istenen kura çevir (Yine sabit kodlar)
            decimal finalAmount = totalTry;
            if (TargetCurrency == "USD") finalAmount = totalTry / 35.50m;
            else if (TargetCurrency == "EUR") finalAmount = totalTry / 38.20m;

            // 8. Sadakat Puanı Hesaplama
            int loyaltyPoints = (int)(totalTry / 10);
            if (IsCorporate) loyaltyPoints *= 2; // Kurumsallara x2 puan

            Console.WriteLine("Kazanılan Sadakat Puanı: " + loyaltyPoints);
            Console.WriteLine("--- Hesaplama Bitti ---");

            return finalAmount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();
            cart.CustomerType = "VIP";
            cart.IsCorporate = false;
            cart.CustomerAge = 22;
            cart.CouponCode = "TEKNOLOJI20";
            cart.PaymentMethod = "CreditCard";
            cart.TargetCurrency = "TRY";
            cart.WantsGiftWrap = true;
            cart.ShippingCity = "Konya";

            // Ürünleri Sepete Ekle
            cart.AddItem(new Product { Name = "NVIDIA RTX 4050 Laptop", Price = 1200, Currency = "USD", Category = "Electronics", StockQuantity = 3, WeightKg = 2.5m, IsAgeRestricted = false });
            cart.AddItem(new Product { Name = "Ubuntu Linux Eğitim Kitabı", Price = 250, Currency = "TRY", Category = "Books", StockQuantity = 10, WeightKg = 0.5m, IsAgeRestricted = false });
            cart.AddItem(new Product { Name = "Enerji İçeceği", Price = 50, Currency = "TRY", Category = "Food", StockQuantity = 100, WeightKg = 0.3m, IsAgeRestricted = true });

            // Stokta olmayan bir ürün
            cart.AddItem(new Product { Name = "Karbon Fiber Kask", Price = 4500, Currency = "TRY", Category = "Motorcycle", StockQuantity = 0, WeightKg = 1.5m, IsAgeRestricted = false });

            Console.WriteLine("\nGenel Sepet Toplamı: " + Math.Round(cart.CalculateTotal(), 2) + " " + cart.TargetCurrency);
            Console.ReadLine();
        }
    }
}