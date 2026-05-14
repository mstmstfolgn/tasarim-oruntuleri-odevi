Neden Bu Örüntüler Seçildi?

Decorator:
Ürünlere sonradan ekstra özellik eklemek için seçildi.
Hediye paketi ,Ekstra sigorta
Mevcut ürün sınıflarını değiştirmeden yeni özellik eklenebildi.

Adapter:
Dış kargo sistemi mevcut yapıyla uyumlu olmadığı için seçildi.
ArasKargoSistemi, IKargoServisi arayüzüne uyarlandı ve sistem bağımlılığı azaltıldı.

Facade:
Sipariş sürecindeki karmaşıklığı azaltmak için seçildi.
Ürün oluşturma, sepete ekleme ve kargo işlemleri tek sınıf altında toplandı.

Neden Diğerleri Seçilmedi?

Bridge:
Abstraction ve implementation ayrımı gerektiren bir yapı olmadığı için kullanılmadı.

Composite:
Projede ağaç yapılı nesne sistemi olmadığı için uygun değildi.

Flyweight:
Bellek optimizasyonu gerektirecek kadar büyük nesne üretimi olmadığı için kullanılmadı.

Proxy:
Güvenlik, cache veya erişim kontrolü ihtiyacı olmadığı için tercih edilmedi.


// Bence bu örüntüler seçilmiş çünkü projede karşılaşılan sorunlara doğrudan çözüm sunuyorlar.
Decorator, ürünlere esnek bir şekilde yeni özellikler eklemeyi sağlarken, 
Adapter mevcut sistemle uyumsuz olan dış kargo sistemini entegre etmeye yardımcı oluyor. 
Facade ise sipariş sürecindeki karmaşıklığı azaltarak kodun okunabilirliğini ve bakımını kolaylaştırıyor.
Diğer örüntüler ise projenin ihtiyaçlarına tam olarak uymadığı için tercih etmedim.


# Faz 2 - AI Log

Yapay Zekaya Ne Sordum?
"Projeye Decorator, Adapter ve Facade ekledik. Peki neden Proxy veya Bridge gibi diğer yapısal
örüntüleri değil de bunları seçtik? Bize ne faydası oldu?"

Yapay Zekanın Cevabı:
Yapay zeka özetle şunu dedi: Ürünlerin fiyatını dinamik olarak değiştirmek istediğimiz
için Decorator şarttı Proxy sadece nesneye erişimi kontrol eder, üzerine yeni özellik eklemez.
Dışarıdan Aras Kargo API'sini bizim sisteme uydurmak için Adapter kullanmak zorundaydık
Bridge daha çok sistemi sıfırdan tasarlarken işe yarıyor. Facade'ı da Main metodundaki o
çorbaya dönmüş karmaşık kodu tek bir çatı altında toplayıp temizlemek için kullandık.

Yapay Zekanın Hatası ve Benim Düzeltmem:
**Yapay Zekanın Hatası ve Benim Düzeltmem (Refleksiyon):**
AI, Decorator kullanırken ürün özelliklerinin otomatik aktarılacağını varsaydığı
için sepet hesaplaması patladı (değerler 0 geldi). Asıl nesnenin verilerini korumak amacıyla,
constructor içine girip özellikleri elimle manuel eşitleyerek (örn: `this.fiyat = urun.fiyat;`) hatayı çözdüm.