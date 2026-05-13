Tasarım Örüntüleri Dokümantasyonu
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

**ÖNCEKİ HALİ (Sıkı Bağımlı):**
```mermaid
classDiagram
    class Program {
        +Main()
    }
    class Urun {
        +string kategori
        +bool yasSiniriVarMi
    }
    Program --> Urun : doğrudan new Urun() ile yaratır


    **SONRAKİ HALİ (Factory Method Uygulanmış)**

    classDiagram
    class Urun {
        <<abstract>>
        +VergiHesapla()
    }
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
    UrunFactory ..> Elektronik : yaratır
    UrunFactory ..> Kitap : yaratır
    Program --> UrunFactory : kullanır