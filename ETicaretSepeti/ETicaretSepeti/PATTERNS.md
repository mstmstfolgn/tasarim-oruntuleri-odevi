
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

```mermaid
classDiagram
=======
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
```mermaid
classDiagram
    class Program {
        +Main()
    }

    class Urun {
        +string kategori
        +bool yasSiniriVarMi
    }

    Program --> Urun : "dogrudan new ile olusturur"
```

```mermaid
classDiagram


    class Urun {
        <<abstract>>
        +VergiHesapla()
    }

    class UrunDecorator {
        <<abstract>>
        #Urun _urun
    }
    class HediyePaketiDecorator
    class EkstraSigortaDecorator
    
    Urun <|-- UrunDecorator
    Urun <-- UrunDecorator : "sarmalar"
    UrunDecorator <|-- HediyePaketiDecorator
    UrunDecorator <|-- EkstraSigortaDecorator

    class IKargoServisi {
        <<interface>>
        +Deliver()
    }
    class ArasKargoAdapter
    class ArasKargoSistemi {
        +KargoGonder()
    }
    
    IKargoServisi <|.. ArasKargoAdapter
    ArasKargoAdapter --> ArasKargoSistemi : "uyarlar"

    class ETicaretFacade {
        +SiparisOlustur()
    }
    class Sepet {
        +ToplamHesapla()
    }
    
    ETicaretFacade --> Sepet : "yonetir"
    ETicaretFacade --> IKargoServisi : "kullanir"
    ETicaretFacade ..> UrunDecorator : "yaratir"
=======

    class Elektronik {
        +VergiHesapla()
    }

    class Kitap {
        +VergiHesapla()
    }

    class UrunFactory {
        +UrunOlustur() Urun
    }

    class Program {
        +Main()
    }

    Urun <|-- Elektronik
    Urun <|-- Kitap

    UrunFactory ..> Elektronik : yaratir
    UrunFactory ..> Kitap : yaratir

    Program --> UrunFactory : kullanir
```

