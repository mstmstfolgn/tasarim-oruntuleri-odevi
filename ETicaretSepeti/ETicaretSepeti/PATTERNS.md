
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
    Sepet --> IIndirimStratejisi : Strategy```
    Sepet --> IUrun : Icerir
    IUrun <|-- UrunDecorator : Decorator
    IUrun <|-- ElektronikUrun : Factory Method
