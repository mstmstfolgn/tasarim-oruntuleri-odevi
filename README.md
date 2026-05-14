# tasarim-oruntuleri-odevi

Konu Seçimi: -D- E-Ticaret Sepeti
Program arka planda çalışınca, çok fazla çıktı alamayınca yaptığım işten keyif almıyorum .
Uğraşlarımın sonucunda gözle görülür gerçek hayatta kullanabileceğim projeler üretmekten daha çok keyif alıyorum.


# E-Ticaret Sepeti Tasarım Örüntüleri Projesi

Bu proje, bir e-ticaret sitesindeki sepet işlemlerini tasarım örüntüleri (design patterns) kullanarak basitçe simüle etmek için hazırlandı.
Temel amacımız, sepetteki ürünlerin hesaplanması, indirimlerin uygulanması ve sipariş sonunda bildirim gitmesi 
gibi işlemleri kodun içine boğulmadan, esnek bir yapıda yönetmek. Özellikle "Açık/Kapalı Prensibini" (OCP) uygulayarak, mevcut 
kodlara dokunmadan sisteme yeni indirim veya ürün tipleri eklemeyi hedefledik.

### Kullanılan Örüntüler ve Mantıkları

Projede toplamda 6 tane tasarım örüntüsü kullandık. Ürünlerin üretim aşamasında Factory Method kullanarak nesne 
oluşturma işini ayırdık. Sepetteki indirimleri if-else kalabalığından kurtarmak için Strategy pattern kullandık; 
böylece her indirim tipi kendi sınıfında çalışıyor. Ürünlere sonradan "Hediye Paketi" gibi özellikler eklemek için Decorator,
dışarıdan gelen kargo sistemlerini projemize uydurmak içinse Adapter kullandık. Sipariş bittiğinde SMS veya Email 
gitmesi işlemini Observer ile abone sistemine bağladık. Tüm bu karmaşık süreci dışarıdan tek bir yerden yönetmek için de Facade yapısını kurduk.

### Mimari Diyagram

```mermaid
graph TD
    User --> ETicaretFacade
    ETicaretFacade --> Sepet
    Sepet --> Stratejiler
    Sepet --> Decoratorlar
    ETicaretFacade --> BildirimSistemi
    ETicaretFacade --> KargoAdapter

