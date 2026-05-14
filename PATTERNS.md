## 2. Structural Patterns (Faz 2)

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



    ---

## 3. Behavioral Patterns (Faz 3)

A. Strategy Pattern
Nerede Kullanıldı?
`Sepet` sınıfında indirim hesaplamaları için `IIndirimStratejisi` 
arayüzü ve türevleri (`VipIndirimStratejisi`, `OgrenciIndirimStratejisi` vb.) kullanıldı.

Neden Kullanıldı?
Önceden indirim mantıkları if-else bloklarıyla `Sepet` sınıfının 
içine gömülüydü. Yeni bir indirim eklemek kodun bozulmasına ve OCP'nin ihlaline yol açardı. 
İndirim algoritmalarını sınıflara bölerek bu karmaşayı temizledik.

Ne Kazanıldı?
OCP (Açık/Kapalı Prensibi) %100 sağlandı. Yeni bir indirim türü 
(Örn: Efsane Cuma) eklemek için mevcut koda hiç dokunmadan sadece yeni bir 
strateji sınıfı oluşturmamız yeterli olacak.

B. Observer Pattern
Nerede Kullanıldı?
`ETicaretFacade` sınıfında sipariş bitiminde müşteriye bildirim 
göndermek için `IBildirimGozlemcisi` arayüzü ile kullanıldı.

Neden Kullanıldı?
Sipariş tamamlanınca Email ve SMS servislerini koda sıkı sıkıya 
bağlamak (tight coupling) yerine, esnek bir "yayıncı-abone" (publisher/subscriber) 
sistemi kurmak istedik.

Ne Kazanıldı?
Sisteme yeni bir bildirim yöntemi (Örn: WhatsApp bildirimi) 
eklemek istediğimizde sipariş veya sepet kodunu değiştirmemize gerek kalmadı. 
Sadece yeni bir gözlemci ekleyip sisteme abone ediyoruz.

---

```mermaid
classDiagram
    class Sepet {
        -IIndirimStratejisi _indirimStratejisi
        +ToplamHesapla()
        +IndirimStratejisiBelirle()
    }
    class IIndirimStratejisi {
        <<interface>>
        +IndirimUygula()
    }
    class VipIndirimStratejisi
    class OgrenciIndirimStratejisi
    
    Sepet o-- IIndirimStratejisi : "kullanir"
    IIndirimStratejisi <|.. VipIndirimStratejisi
    IIndirimStratejisi <|.. OgrenciIndirimStratejisi

    class ETicaretFacade {
        -List~IBildirimGozlemcisi~ _gozlemciler
        +SiparisOlustur()
        +GozlemciEkle()
    }
    class IBildirimGozlemcisi {
        <<interface>>
        +BildirimAl()
    }
    class EmailBildirim
    class SmsBildirim
    
    ETicaretFacade o-- IBildirimGozlemcisi : "haberdar eder"
    IBildirimGozlemcisi <|.. EmailBildirim
    IBildirimGozlemcisi <|.. SmsBildirim