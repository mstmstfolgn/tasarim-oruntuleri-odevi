

﻿## 2. Structural Patterns (Faz 2)

### A. Decorator Pattern
**Nerede Kullanıldı?** `UrunDecorator` soyut sınıfı ve ondan türeyen `HediyePaketiDecorator` 
ile `EkstraSigortaDecorator` sınıflarında kullanıldı.

**Neden Kullanıldı?** Ürünlere (kitap, elektronik vb.) çalışma zamanında (runtime) dinamik olarak 
yeni özellikler (fiyat artışı, isim değişikliği) eklememiz gerekiyordu. 
Eğer her kombinasyon için yeni bir sınıf açsaydık 
(örneğin: `HediyePaketliSigortaliElektronik`), sınıf sayımız patlardı (Class Explosion). 
Decorator ile nesneleri birbirinin içine "sarmalayarak" bu sorunu çözdük.

**Ne Kazanıldı?** - OCP (Açık/Kapalı Prensibi) sağlandı: Mevcut ürün sınıflarına hiç 
  dokunmadan onlara yeni yetenekler kazandırdık.
- Özellikler iç içe geçebilir (sarmalanabilir) hale geldi.

### B. Adapter Pattern
**Nerede Kullanıldı?** Kendi sistemimizdeki `IKargoServisi` arayüzü ile dışarıdan gelen 
(ve koduna dokunamadığımız) `ArasKargoSistemi` sınıfı arasında 
`ArasKargoAdapter` sınıfında kullanıldı.

**Neden Kullanıldı?** Dışarıdan gelen kargo sisteminin metot isimleri (`KargoGonder`) bizim 
sistemimizin beklentisiyle (`Deliver`) tamamen uyumsuzdu. 
Kodu değiştiremediğimiz bu dış yapıyı kendi sistemimize entegre etmek 
için araya bir "çevirici/priz" koyduk.

**Ne Kazanıldı?** - Uyumsuz arayüzler sorunsuz şekilde birbirine bağlandı. 
- Kargo firması değişirse, sadece yeni bir Adapter yazmamız yetecek.

### C. Facade Pattern
**Nerede Kullanıldı?** `ETicaretFacade` sınıfı oluşturularak kullanıldı.

**Neden Kullanıldı?** `Main` metodumuzda ürün yaratma, bunu decorator ile sarmalama, 
sepete ekleme ve kargoyu çağırma gibi arka arkaya çalışan 
bir sürü karmaşık adım vardı. Bu karmaşayı istemciden (Client) 
gizlemek ve basitleştirmek için tek bir giriş noktası oluşturduk.

**Ne Kazanıldı?** - `Main` (istemci) kodu inanılmaz derecede basitleşti. 
- İstemci artık alt sistemlerin karmaşasını bilmek zorunda değil.

---

### 📊 Faz 2 Mimari UML Diyagramı




﻿Tasarım Örüntüleri Dokümantasyonu
1. Creational Pattern: Factory Method (Faz 1)

Nerede Kullanıldı?

Projede UrunFactory isminde bir fabrika sınıfı oluşturuldu.
Eskiden ürünler Main içinde direkt new Elektronik(), new Kitap() şeklinde oluşturuluyordu.
Şimdi ise ürün oluşturma işlemleri UrunFactory üzerinden yapılıyor.

Neden Kullanıldı?

Eski yapıda yeni bir ürün eklemek istediğimizde ana kodun içine girip değişiklik yapmak gerekiyordu.
Bu durum kodun karışmasına ve bağımlılığın artmasına neden oluyordu.
Factory Method kullanılarak nesne oluşturma işi tek bir yerde toplandı. 
Böylece kod daha düzenli ve yönetilebilir hale geldi.

Ne Kazanıldı?
-Kodun okunabilirliği arttı.
-Ürün oluşturma işlemleri merkezi hale geldi.
-Yeni ürün eklemek kolaylaştı.
-Main metodu gereksiz nesne oluşturma yükünden kurtuldu.
-OCP prensibine daha uygun bir yapı oluştu.

UML Diyagramı Ne Anlatıyor?

İlk diyagramda Program sınıfı ürünleri doğrudan kendi oluşturuyor. Yani sistem sıkı bağlı çalışıyor.
İkinci diyagramda ise araya UrunFactory giriyor. Böylece Program hangi ürünün nasıl 
oluşturulduğunu bilmek zorunda kalmıyor.
Bu da bağımlılığı azaltıyor ve sistemi daha esnek hale getiriyor.
### 📊 Sistemin Genel Mimari Diyagramı (UML)

```mermaid
classDiagram
    class ETicaretFacade {
        +SiparisTamamla()
    }
    class Sepet {
        -List urunler
        +UrunEkle(IUrun)
        +Hesapla(IIndirimStratejisi)
    }
    class IUrun {
        <<interface>>
        +GetFiyat()
        +GetAd()
    }
    class IIndirimStratejisi {
        <<interface>>
        +IndirimUygula(fiyat)
    }
    class IBildirimGozlemci {
        <<interface>>
        +BilgiVer(mesaj)
    }

    ETicaretFacade --> Sepet : Yonetir
    ETicaretFacade --> IBildirimGozlemci : Observer
    Sepet --> IIndirimStratejisi : Strategy
    Sepet --> IUrun : Icerir
    IUrun <|-- UrunDecorator : Decorator
    IUrun <|-- ElektronikUrun : Factory Method```